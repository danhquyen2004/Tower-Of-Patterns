using System;
using UnityEngine;

public class TowerNormal : TowerBase
{
    private void Awake()
    {
        attackStrategy = new NormalAttackStrategy();
    }
}
