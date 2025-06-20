using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score;
    public GameOverUI gameOverPanel;
    public CastleController castleController;
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void RestartGame()
    {
        castleController.Restart();
        WaveManager.Instance.RestartGame();
        GoldManager.Instance.ResetGold();
        score = 0;
    }

    public void GameOver()
    {
        WaveManager.Instance.GameOver();
        gameOverPanel.ShowGameOver(score);
    }
}
