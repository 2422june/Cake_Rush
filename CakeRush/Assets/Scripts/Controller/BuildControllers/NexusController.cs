using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : BuildBase
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        isSpawnable = false;
        DataLoad("Nexus"); 

        base.Awake();
    }

    protected override void Die()
    {
        base.Die();
        if(PV.IsMine)
            Managers.instance._game.SetScene(Define.Scene.Defeat);
        else
            Managers.instance._game.SetScene(Define.Scene.Victory);
    }

}
