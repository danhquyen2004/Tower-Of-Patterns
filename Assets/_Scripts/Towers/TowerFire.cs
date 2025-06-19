using System;
using UnityEngine;

public class TowerFire : TowerBase
{
    public override void Initialize()
    {
        base.Initialize();
        attackStrategy = new FireBurnAttackStrategy();
    }
}
