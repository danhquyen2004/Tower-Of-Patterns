public class WaveSpawnInfo {
    public string enemyType;
    public int count;
    public float spawnInterval;
    public float spawnDuration;

    public WaveSpawnInfo(string enemyType, int count, float spawnInterval, float spawnDuration) {
        this.enemyType = enemyType;
        this.count = count;
        this.spawnInterval = spawnInterval;
        this.spawnDuration = spawnDuration;
    }
}
