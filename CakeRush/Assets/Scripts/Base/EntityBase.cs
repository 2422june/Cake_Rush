using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//Character and Building GameObject's Base Class
public class EntityBase : MonoBehaviourPunCallbacks
{
    #region  element
    public float damage { get; set; }
    public float maxHp { get; set; }
    public float curHp { get; set; }
    public float attackSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float spawnTime { get; set; }
    public float returnExp { get; set; }
    [SerializeField] protected float attackRange;
    protected float eyeSight;
    public int[] cost;
    [SerializeField] protected int[] dropCost = new int[3];
    protected float defensive;

    protected Data.Stat stat;
    [SerializeField]
    protected RTSController rtsController;
    public GameObject Marker;

    protected AudioSource source;

    public int Sugar {get {return cost[1];} protected set{cost[1] = value;} }
    public int Chocolate {get {return cost[1];} protected set{cost[1] = value;} }
    public int Wheat {get {return cost[1];} protected set{cost[1] = value;} }

    public bool isSelected;
    public bool isActive;

    public int team;
    protected PhotonView PV;

    #endregion

    protected virtual void Awake()
    {
        Marker = transform.Find("Marker").gameObject;
        Marker.transform.localPosition = Vector3.zero;
        Marker.SetActive(false);
        PV = GetComponent<PhotonView>();

        source = GetComponent<AudioSource>();
        if (source == null)
            source = GetComponent<AudioSource>();

        tag = GameManager.instance.tag;

        cost = new int[3];

        Init();
    }

    protected void Start()
    {
        rtsController = GameManager.instance.rtsController;
    }

    protected virtual void Init()
    {
        maxHp = stat.hp;
        curHp = stat.hp;
        damage = stat.damage;
        attackRange = stat.attackRange;
        attackSpeed = stat.attackSpeed;
        returnExp = stat.returnExp;
        eyeSight = stat.eyeSight;
        cost = stat.cost;
        dropCost = stat.dropCost;
        defensive = stat.defensive;
        spawnTime = stat.spawnTime;
        moveSpeed = stat.moveSpeed;
    }

    #region function
    public virtual void Hit(float hitDamage)
    {
        curHp -= hitDamage;

        Debug.Log($"Current {gameObject.name} HP : {curHp}");
        if(curHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"Die(), at {gameObject.name})");
    }

    protected void DataLoad (string fileName)
    {
        stat = new Data.Stat();
        TextAsset dataFile = Resources.Load<TextAsset>($"Data/{fileName}");
        stat = JsonUtility.FromJson<Data.Stat>(dataFile.text);
    }
    public void Select()
    {

        isSelected = true;
		Marker.SetActive(true);

        OnSelect();
    }

    private void OnSelect()
    {
        //Send Stat Data
    }
    
	public void Deselect()
    {
        isSelected = false;
        Marker.SetActive(false);
	}
    
    protected virtual void Update()
    {
    }

    protected virtual void Respawn()
    {

    }
    
    #endregion
}
