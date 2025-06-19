using UnityEngine;

public class TowerPoison : TowerBase
{
    public override void Initialize()
    {
        base.Initialize();
        attackStrategy = new PoisonAttackStrategy();
    }
}
