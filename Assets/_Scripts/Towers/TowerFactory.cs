using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public static GameObject CreateTower(string type, Vector3 position, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>($"Towers/{type}");
        if (prefab != null)
        {
            GameObject towerObj = Instantiate(prefab, position, Quaternion.identity, parent);
            TowerBase tower = towerObj.GetComponent<TowerBase>();

            switch (type)
            {
                case "Tower_Normal":
                    tower.attackStrategy = new NormalAttackStrategy();
                    break;
                case "Tower_AoE":
                    tower.attackStrategy = new AoEAttackStrategy();
                    break;
                default:
                    Debug.LogWarning("Tower type not recognized");
                    break;
            }

            return towerObj;
        }
        Debug.LogWarning("Tower prefab not found");
        return null;
    }
}
