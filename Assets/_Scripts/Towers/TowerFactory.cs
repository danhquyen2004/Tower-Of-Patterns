using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public static GameObject CreateTower(string type, Vector3 position, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>($"Towers/{type}");
        if (prefab != null)
        {
            GameObject towerObj = Instantiate(prefab, position, Quaternion.identity, parent);
            TowerBase towerBase = towerObj.GetComponent<TowerBase>();
            if (towerBase != null)
            {
                towerBase.Initialize();
            }
            else
            {
                Debug.LogError($"TowerBase component not found on {type} prefab.");
            }
            return towerObj;
        }
        Debug.LogWarning("Tower prefab not found");
        return null;
    }
}
