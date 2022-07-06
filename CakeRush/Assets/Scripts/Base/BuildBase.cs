using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// this is a Build Component class that is spwanable. 
public class BuildBase : EntityBase
{
    public GameObject buildEffect;
    public bool isSpawnable;

    public bool isOnSelectable;

    NavMeshObstacle obstacle;
    Renderer render;
    [SerializeField] Material originMat;

    protected override void Awake()
    {
        base.Awake();
        if(isSpawnable)
        {
            obstacle = gameObject.GetComponent<NavMeshObstacle>();
            obstacle.carving = true;
            obstacle.enabled = false;
            render = gameObject.GetComponentInChildren<Renderer>();
            originMat = render.material;
            render.material = Resources.Load<Material>("Materials/Blueprint");
            curHp = 0f;
            buildEffect = transform.Find("BuildEffect").gameObject;
        }
    }
    
    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isSelected)
        {
            BuildCancel();
        }
        base.Update();
    }
    
    public IEnumerator Build()
    {   
        buildEffect.SetActive(true);
        obstacle.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        gameObject.tag = "Build";
        render.material = originMat;
        gameObject.isStatic = true;
        Debug.Log("Start Coroutine Build()");
        while (curHp < maxHp )
        {
            curHp += Time.deltaTime * spawnTime;
            //Debug.Log(curHp);
            if(Input.GetKeyDown(KeyCode.N) && isSelected)
            {
                for(int i = 0; i < 3; i++)
                    rtsController.cost[i] += cost[i]/2;
                Debug.Log("Cenceled Build");
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
        curHp = maxHp;
        Debug.Log("Build() Completed");
        isActive = true;
        buildEffect.SetActive(false);
    }

    protected void BuildCancel()
    {
        // summon effect
        // give player: returnCost / 2
        for(int i = 0; i < 2; i++)
        {
            Debug.Log($"{GameManager.instance.cost[i]} -> {GameManager.instance.cost[i] + cost[i]/2}");
            GameManager.instance.cost[i] += cost[i]/2;
        }
        Debug.Log("Build Cancel()");
        Destroy(gameObject);
    }

    public void SelectBuilding(BuildBase newBuild)
	{
        
	}

	public void DeselectBuilding(BuildBase newBuild)
	{
        
	}

    private void OnTriggerStay(Collider other) 
    {
        if(isSpawnable == true)
        {
            if(isActive == false && other.gameObject.layer == LayerMask.NameToLayer("Selectable")
                || other.gameObject.CompareTag("Ground_Stone"))
            {
                isOnSelectable = true;
                render.material.SetColor("_OutlineColor", Color.red);
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(isSpawnable == true)
        {
            isOnSelectable = false;
            render.material.SetColor("_OutlineColor", Color.green);
        }
    }
}