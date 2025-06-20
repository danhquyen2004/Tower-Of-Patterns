using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button replayButton;
    
    private void Start()
    {
        replayButton.onClick.AddListener(OnReplayButtonClicked);
    }
    public void ShowGameOver(int score)
    {
        gameOverPanel.SetActive(true);
        scoreText.text = $"Score: {score}";
    }

    private void OnReplayButtonClicked()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.RestartGame();
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }
    
}
