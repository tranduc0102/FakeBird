using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    protected override void Awake()
    {
        base.KeepAlive(false);
        base.Awake();
    }
}
