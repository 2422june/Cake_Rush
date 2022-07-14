using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CookieHouseController : BuildBase
{
    //[SerializeField] private GameObject[] units = new GameObject[4];
    Vector3 collectionPos;
    [SerializeField] bool isWorking;
    [SerializeField] Transform unitSpawnPos;
    string[] unitPrefab = new string[4];
    protected override void Awake()
    {
        collectionPos = new Vector3(transform.position.x, 0f, transform.position.z);
        isSpawnable = true;
        DataLoad("CookieHouse");
        unitPrefab[0] = "Prefabs/Units/Unit_Egg";
        unitPrefab[1] = "Prefabs/Units/Unit_SlimeJelly";
        unitPrefab[2] = "Prefabs/Units/Unit_Stone";
        base.Awake();
    }

    protected override void Start()
    {
<<<<<<< HEAD
        unitSpawnPos = transform.Find("UnitSpawnPos");
=======
>>>>>>> BiN_
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(isSelected && isActive)
        {
            SpwanUnit();
            CollectionUnit();
        }
    }

    void CollectionUnit()
    {
        if ( Input.GetMouseButtonDown(1))
		{
			Ray	ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// When there is an object hitting the ray (= clicking on the unit)
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameProgress.instance.groundLayer))
			{
				collectionPos = hit.point;
			}
        }
    }

    void SpwanUnit()
    {
        if(isWorking == true)
        {            
            return;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Spawn(0));
            return;
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Spawn(1));
            return;
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Spawn(2));
            return;
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(Spawn(3));
        }
    }

    IEnumerator Spawn(int n)
    { 
        isWorking = true;
        GameObject newUnit = PhotonNetwork.Instantiate(unitPrefab[n], unitSpawnPos.position, Quaternion.identity);
       // newUnit.tag = "Me_Unit";
        EntityBase unitBase = newUnit.GetComponent<EntityBase>();
        newUnit.SetActive(false);
        
        float curTime = 0.0f;
        for(int i = 0; i < 3; i++)
        {
            if(rtsController.cost[i] <= unitBase.cost[i])
            {
                Debug.Log("cost isNotEnough to SpwanUnit");
                yield break;
            }
        }
        
        for(int i = 0; i < 3; i++)
            rtsController.cost[i] -= unitBase.cost[i];
        while(curTime <= unitBase.spawnTime)
        {
            curTime += Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.N))
            {
                for(int i = 0; i < 3; i++)
                    rtsController.cost[i] += unitBase.cost[i];
                Debug.Log("Cenceled SpwanUnit");
                yield break;
            }
            yield return null;
        }
        //yield return new WaitForSeconds(unit.spawnTime);
        Debug.Log($"prevCost: {rtsController.cost[0]} {rtsController.cost[1]} {rtsController.cost[2]}");

        for(int i = 0; n < 3; n++)
        {
            rtsController.cost[i] -= unitBase.cost[i];
        }
        
        Debug.Log($"curCost: {rtsController.cost[0]} {rtsController.cost[1]} {rtsController.cost[2]}");

        yield return new WaitUntil(() => rtsController.unitList.Count < rtsController.maxUnit + 1);
        newUnit.SetActive(true);
        newUnit.GetComponent<UnitBase>().Move(collectionPos);
        rtsController.unitList.Add(newUnit.GetComponent<UnitBase>());  
        isWorking = false;  
    }
}
