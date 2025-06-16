using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public Transform spawnPoint;

    private int currentWave = 0;
    public event Action<int> OnWaveStarted;
    public event Action<int> OnWaveEnded;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void StartNextWave()
    {
        currentWave++;
        StartCoroutine(SpawnWaveCoroutine(currentWave));
    }

    private IEnumerator SpawnWaveCoroutine(int waveNumber)
    {
        Debug.Log($"Wave {waveNumber} started");

        WaveBuilder builder = new WaveBuilder(waveNumber);
        List<WaveSpawnInfo> waveInfo = builder.BuildWave();

        foreach (var info in waveInfo)
        {
            for (int i = 0; i < info.count; i++)
            {
                EnemyFactory.CreateEnemy(info.enemyType, spawnPoint.position);
                yield return new WaitForSeconds(info.spawnInterval);
            }
        }

        Debug.Log($"Wave {waveNumber} ended");
    }


    private string GetRandomEnemyType(int waveNumber)
    {
        // Logic đơn giản: càng về sau càng dễ ra loại mạnh
        float r = UnityEngine.Random.value;
        if (waveNumber < 3) return "Enemy_Torch";
        if (r < 0.6f) return "Enemy_Torch";
        if (r < 0.9f) return "Enemy_TNT";
        return "Enemy_Barrel";
    }
}

