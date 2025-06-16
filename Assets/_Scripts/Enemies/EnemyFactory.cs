using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    public static GameObject CreateEnemy(string type, Vector3 spawnPos) {
        GameObject prefab = Resources.Load<GameObject>($"Enemies/{type}");
        if (prefab != null) {
            GameObject enemyObj = Instantiate(prefab, spawnPos, Quaternion.identity);
            EnemyBase enemy = enemyObj.GetComponent<EnemyBase>();

            switch (type) {
                case "Enemy_Torch":
                    enemy.attackStrategy = new MeleeAttackStrategy();
                    break;
                case "Enemy_TNT":
                    enemy.attackStrategy = new RangedAttackStrategy();
                    break;
                case "Enemy_Barrel":
                    enemy.attackStrategy = new MeleeAttackStrategy(); // Tạm dùng chung
                    break;
                default:
                    Debug.LogWarning("Enemy type not recognized");
                    break;
            }

            return enemyObj;
        }

        Debug.LogWarning("Enemy prefab not found");
        return null;
    }
}
