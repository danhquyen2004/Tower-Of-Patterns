public class WaveSpawnInfo {
    public string enemyType;
    public int count;
    public float spawnInterval;

    public WaveSpawnInfo(string enemyType, int count, float spawnInterval) {
        this.enemyType = enemyType;
        this.count = count;
        this.spawnInterval = spawnInterval;
    }
}
