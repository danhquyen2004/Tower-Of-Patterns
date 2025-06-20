

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    public enum GameState { BUILD, WAVE, WAIT_END, GAME_OVER }
    public static WaveManager Instance { get; private set; }
    public Transform spawnPoint;
    public float waveDuration;

    public GameState currentState = GameState.BUILD;
    private int currentWave = 0;
    [SerializeField] private WaveUI waveUI;
    private List<EnemyBase> enemies = new List<EnemyBase>();
    
    private Coroutine timerCoroutine;
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
        waveUI.SetTimeCountText(GetSpawnDuration(this.currentWave));
    }

    private void Update()
    {
        waveUI.SetButtonEnable(currentState == GameState.BUILD);
        waveUI.SetWaveText(currentWave);
    }

    // Gọi từ UI khi player bấm nút bắt đầu wave
    public void StartWave()
    {
        StartCoroutine(WaveRoutine());
        timerCoroutine = StartCoroutine(WaveTimerCoroutine());
    }

    IEnumerator WaveRoutine()
    {
        currentState = GameState.WAVE;
        currentWave++;
        WaveBuilder builder = new WaveBuilder(currentWave);
        List<WaveSpawnInfo> waveInfo = builder.BuildWave();

        float elapsed = 0f;
        bool timeUp = false;
        while (!timeUp)
        {
            foreach (var info in waveInfo)
            {
                waveDuration = info.spawnDuration;
                for (int i = 0; i < info.count; i++)
                {
                    if(currentState != GameState.WAVE) 
                    {
                        timeUp = true; 
                        break; 
                    }
                    if (elapsed >= waveDuration) { timeUp = true; break; }
                    GameObject enemyObj = EnemyFactory.CreateEnemy(info.enemyType, spawnPoint.position);
                    enemies.Add(enemyObj.GetComponent<EnemyBase>());
                    yield return new WaitForSeconds(info.spawnInterval);
                    elapsed += info.spawnInterval;
                }
                if (timeUp) break;
            }
        }
        
        EndWave();
    }

    public void GameOver()
    {
        EndWave();
        currentState = GameState.GAME_OVER;
    }
    IEnumerator WaveTimerCoroutine()
    {
        float timeLeft = waveDuration;
        while (timeLeft > 0f)
        {
            waveUI.SetTimeCountText(timeLeft);
            yield return null; 
            timeLeft -= Time.deltaTime;
        }
        waveUI.SetTimeCountText(0); 
    }
    private float GetSpawnDuration(int currentWave) {
        if (currentWave < 2) return 20f;
        if (currentWave < 4) return 40f;
        return 60f;
    }
    public void RestartGame()
    {
        StopCoroutine(timerCoroutine);
        waveUI.SetTimeCountText(0); 
        currentWave = 0;
        enemies.Clear();
        EnterBuildPhase();
    }

    private void EndWave()
    {
        foreach (var enemy in enemies)
        {
            if(enemy.IsDead) continue; 
            enemy.Die();
        }
        enemies.Clear();
        // Kết thúc wave
        currentState = GameState.WAIT_END;
        
        EnterBuildPhase();
    }
}