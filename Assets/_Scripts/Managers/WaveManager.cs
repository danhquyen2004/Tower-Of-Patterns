

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    public enum GameState { BUILD, WAVE, WAIT_END, GAME_OVER }
    public static WaveManager Instance { get; private set; }
    public Transform spawnPoint;
    public float buildTime = 20f;
    public float waveDuration = 60f;

    public GameState currentState = GameState.BUILD;
    private int currentWave = 0;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        EnterBuildPhase();
    }

    public void EnterBuildPhase()
    {
        currentState = GameState.BUILD;
    }

    // Gọi từ UI khi player bấm nút bắt đầu wave
    public void StartWave()
    {
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        currentState = GameState.WAVE;
        currentWave++;
        float elapsed = 0f;
        WaveBuilder builder = new WaveBuilder(currentWave);
        List<WaveSpawnInfo> waveInfo = builder.BuildWave();

        // Bắt đầu spawn quái trong 1 phút
        while (elapsed < waveDuration)
        {
            foreach (var info in waveInfo)
            {
                for (int i = 0; i < info.count; i++)
                {
                    EnemyFactory.CreateEnemy(info.enemyType, spawnPoint.position);
                    yield return new WaitForSeconds(info.spawnInterval);
                }
            }
            elapsed += waveDuration; // hoặc cộng dồn từng lần spawn
        }

        // Kết thúc wave
        currentState = GameState.WAIT_END;
        EnterBuildPhase();
    }

    public void GameOver()
    {
        currentState = GameState.GAME_OVER;
        // Hiện UI thua, tổng kết điểm, v.v.
    }
}