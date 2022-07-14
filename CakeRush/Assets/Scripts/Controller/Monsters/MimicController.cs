using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicController : MobBase
{
    protected override void Awake()
    {
        DataLoad("JorypongCandy"); 
        base.Awake();
        navMashAgent.speed = moveSpeed;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

}
