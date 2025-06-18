using UnityEngine;

public class TowerPoison : TowerBase
{
    private void Awake()
    {
        attackStrategy = new PoisonAttackStrategy();
    }
}
