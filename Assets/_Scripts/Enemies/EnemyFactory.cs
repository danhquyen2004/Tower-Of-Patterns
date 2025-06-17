using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static GameObject CreateEnemy(string type, Vector3 spawnPos)
    {
        GameObject prefab = Resources.Load<GameObject>($"Enemies/{type}");
        if (prefab != null)
        {
            return GameObject.Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        return null;
    }

}
