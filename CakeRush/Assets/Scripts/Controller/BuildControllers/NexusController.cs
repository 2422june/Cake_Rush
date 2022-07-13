using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : BuildBase
{

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
            GameManager.instance.SetScene("defeat");
        else
            GameManager.instance.SetScene("victory");
    }

}
