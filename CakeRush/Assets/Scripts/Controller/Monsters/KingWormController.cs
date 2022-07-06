using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingWormController : MobBase
{
    protected override void Awake()
    {
        DataLoad("KingKkomuli"); 
        base.Awake();
        navMashAgent.speed = moveSpeed;
        
    }

    protected override void Update()
    {
        base.Update();
    }

  
}
