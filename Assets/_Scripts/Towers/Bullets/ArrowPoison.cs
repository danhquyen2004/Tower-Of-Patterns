using System;
using UnityEngine;

public class ArrowPoison : ArrowBase
{
    private void Awake()
    {
        effect = new PoisonEffect();
    }
}
