using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] public GameObject cookieHouseObj;
    [SerializeField] public GameObject sugarMinerObj;
    [SerializeField] public GameObject chocolateMinerObj;

    [SerializeField] public CookieHouseController cookieHosueController;
    [SerializeField] public SugarMinerController sugarMinerController;
    [SerializeField] public ChocolateMinerController chocolateMinerController;

    public string cookieHouseName;
    public string sugerMinerName;
    public string chocolateMinerName;

    public string[] BuildPrefabs = new string[3];
    [SerializeField] public bool isBuildMode;

    void Awake()
    {
        isBuildMode = false;
        if(cookieHouseObj == null) cookieHouseObj = Resources.Load<GameObject>("Prefabs/Houses/CookieHouse");
        if(sugarMinerObj == null) sugarMinerObj = Resources.Load<GameObject>("Prefabs/Houses/SugarMiner");
        if(chocolateMinerObj == null) chocolateMinerObj = Resources.Load<GameObject>("Prefabs/Houses/ChocolateMiner");
        
        BuildPrefabs[0] = "Prefabs/Houses/CookieHouse";
        BuildPrefabs[1] = "Prefabs/Houses/SugarMiner";
        BuildPrefabs[2] = "Prefabs/Houses/ChocolateMiner";

        if(cookieHouseObj != null) 
        {
            cookieHouseName = cookieHouseObj.name;
            cookieHosueController = cookieHouseObj.GetComponent<CookieHouseController>();
        }
        if(sugarMinerObj != null)
        {
            sugerMinerName = sugarMinerObj.name;
            sugarMinerController = sugarMinerObj.GetComponent<SugarMinerController>();
        } 
        if(chocolateMinerObj != null)
        {
            chocolateMinerName = chocolateMinerObj.name;    
            chocolateMinerController = chocolateMinerObj.GetComponent<ChocolateMinerController>();
        } 
    }
}