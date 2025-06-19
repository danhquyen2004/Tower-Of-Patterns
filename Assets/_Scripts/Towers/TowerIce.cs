using UnityEngine;

public class TowerIce : TowerBase
{
    public override void Initialize()
    {
        base.Initialize();
        attackStrategy = new IceSlowAttackStrategy();
    }
}
