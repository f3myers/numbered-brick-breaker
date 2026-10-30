
using UnityEngine;

public class Brick : MonoBehaviour
{
    [Header("Resistencia del bloque")]
    [SerializeField] private int health = 3;

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
            Destroy(gameObject);
        }
    }
}
