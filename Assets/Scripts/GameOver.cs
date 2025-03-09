using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text scoreText;
    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AddScore(int points)
    {
        score += points;
    }
}
