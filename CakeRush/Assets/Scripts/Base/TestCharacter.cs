using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : UnitBase
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        DataLoad("EggCandy");
        base.Awake();
        navMashAgent.speed = moveSpeed;
        curHp = 10000;
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
