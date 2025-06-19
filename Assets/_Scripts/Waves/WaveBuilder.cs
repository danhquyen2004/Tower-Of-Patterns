using System.Collections.Generic;
using UnityEngine;

public class WaveBuilder {
    private int waveNumber;

    public WaveBuilder(int waveNumber) {
        this.waveNumber = waveNumber;
    }

    public List<WaveSpawnInfo> BuildWave() {
        List<WaveSpawnInfo> wave = new List<WaveSpawnInfo>();

        // Luật: càng cao càng nhiều enemy
        int baseCount = 3 + waveNumber;
        float spawnInterval = Mathf.Max(0.5f, 2f - (waveNumber * 0.1f));

        // Tính toán tỉ lệ
        int torchCount = Mathf.RoundToInt(baseCount * GetTorchRatio());
        int barrelCount = baseCount - torchCount ;

        if (torchCount > 0)
            wave.Add(new WaveSpawnInfo("Torch", torchCount, spawnInterval));
        if (barrelCount > 0)
            wave.Add(new WaveSpawnInfo("Barrel", barrelCount, spawnInterval));

        return wave;
    }

    private float GetTorchRatio() {
        if (waveNumber < 3) return 1f;
        if (waveNumber < 10) return 0.7f;
        if (waveNumber < 20) return 0.5f;
        return 0.4f;
    }
}
