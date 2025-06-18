using UnityEngine;

public class TowerIce : TowerBase
{
    private void Awake()
    {
        attackStrategy = new IceSlowAttackStrategy();
    }
}
