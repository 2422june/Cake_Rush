using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheifRatController : MobBase
{
    protected override void Awake()
    {
        DataLoad("TheifRat");
        base.Awake();        
        navMashAgent.speed = moveSpeed;
    }
    void Start()
    {
        
    }

}
