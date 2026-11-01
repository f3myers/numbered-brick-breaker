
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Puntuación")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Vidas")]
    [SerializeField] private int lives = 3;

    [Header("Pantalla Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;

    private int score = 0;
    private bool gameOver = false;

    private void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

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
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;

        Debug.Log("GAME OVER");
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "PUNTOS: " + score;
        }
    }
}