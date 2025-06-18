using System;
using UnityEngine;

public class TowerFire : TowerBase
{
    private void Awake()
    {
        attackStrategy = new FireBurnAttackStrategy();
    }
}
