
using UnityEngine;
using TMPro;

public class Brick : MonoBehaviour
{
    [Header("Resistencia del bloque")]
    [SerializeField] private int health = 3;

    private int startingHealth;

    [Header("Texto de resistencia")]
    [SerializeField] private TextMeshPro healthText;

    private void Start()
    {
        startingHealth = health;
        UpdateHealthText();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar si el objeto que golpeó es la pelota.
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("El bloque recibió un impacto. Resistencia restante: " + health);

        if (health <= 0)
        {
            // Calcular los puntos según la resistencia inicial.
            int points = startingHealth * 10;

            // Buscar el administrador del juego.
            GameManager gameManager = FindFirstObjectByType<GameManager>();

            if (gameManager != null)
            {
                gameManager.AddScore(points);
            }

            Destroy(gameObject);
        }
        else
        {
            UpdateHealthText();
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }
}
