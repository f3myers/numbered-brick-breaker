using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Puntuación")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Vidas")]
    [SerializeField] private int lives = 3;

    private int score = 0;
    private bool gameOver = false;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        if (gameOver)
            return;

        score += points;

        UpdateScoreText();

        Debug.Log("Puntuación: " + score);
    }

    public void LoseLife()
    {
        if (gameOver)
            return;

        lives = Mathf.Max(0, lives - 1);

        Debug.Log("Vidas restantes: " + lives);

        if (lives == 0)
        {
            gameOver = true;
            Debug.Log("GAME OVER");
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "PUNTOS: " + score;
        }
    }
}