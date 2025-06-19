using System;
using UnityEngine;

public class TowerNormal : TowerBase
{
    public override void Initialize()
    {
        base.Initialize();
        attackStrategy = new NormalAttackStrategy();
    }
}
