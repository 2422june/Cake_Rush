using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateMinerController : BuildBase
{

    private int ChocolatePerSec = 3;
    
    protected override void Awake()
    {
        isSpawnable = true;
        DataLoad("ChocolateMiner"); 
        base.Awake();
    }

    void Start()
    {
        StartCoroutine(MineChocolate());
    }

    protected override void Update()
    {
        base.Update();

    }

    IEnumerator MineChocolate()
    {
        yield return new WaitUntil(()=> isActive == true);
        while(true)
        {
            rtsController.cost[1] += ChocolatePerSec;   
            yield return new WaitForSeconds(1f);    
            Debug.Log(rtsController.cost[2]);
            yield return null;
        }
    }
}
