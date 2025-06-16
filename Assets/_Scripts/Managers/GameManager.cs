using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int baseHealth = 100;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }
    private void Start()
    {
        GoldManager.Instance.OnGoldChanged += UIManager.Instance.UpdateGold;
        WaveManager.Instance.OnWaveStarted += UIManager.Instance.UpdateWave;
    }
    public void TakeBaseDamage(int amount)
    {
        baseHealth -= amount;
        Debug.Log($"Base damaged! Current health: {baseHealth}");
        if (baseHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Gọi UI, stop wave, v.v
    }
}
