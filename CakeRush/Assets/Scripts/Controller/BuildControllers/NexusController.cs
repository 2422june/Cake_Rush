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

}
