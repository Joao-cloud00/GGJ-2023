using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNative : Tower
{
    public TowerNative()
    {
        price = 100;
    }

    private void Awake()
    {
        SetColliderSize(3);
    }
}
