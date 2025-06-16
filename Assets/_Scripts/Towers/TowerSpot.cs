using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    private void OnMouseDown()
    {
        if (!IsOccupied)
        {
            // Ví dụ hardcode type để test
            TowerFactory.CreateTower("Tower_Normal", transform.position, transform);
            IsOccupied = true;
        }
    }
}
