using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public bool isOccupied = false;
    public TowerBase currentTower;

    private void OnMouseDown()
    {
        UITowerManager.Instance.ShowPopup(this);
    }

    public void BuildTower(string type)
    {
        if (isOccupied) return;
        GameObject obj = TowerFactory.CreateTower(type, transform.position, transform);
        currentTower = obj.GetComponent<TowerBase>();
        GoldManager.Instance.SpendGold(currentTower.price);
        isOccupied = true;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void UpgradeTower()
    {
        if (currentTower != null) currentTower.Upgrade();
    }

    public void DestroyTower()
    {
        if (currentTower != null)
        {
            Destroy(currentTower.gameObject);
            currentTower = null;
            isOccupied = false;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
