using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.AI;

//모든 캐릭터의 최상위 부모 클래스
public class CharacterBase : EntityBase
{
    public NavMeshAgent navMashAgent;
    protected Animator animator;
    [SerializeField] protected Data.StatureAbillty statureAbillty;
    protected bool isStun;
    float curStunTime;

    protected override void Awake()
    {
        isStun = false;
        navMashAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        if (isStun) return;
        if (!GameManager.instance.CompareTag(tag)) return;
        //base.Update();
    }

    public IEnumerator Stun(float stunTime)
    {
        isStun = true;
        curStunTime = stunTime;
        Debug.Log($"Stun {gameObject.name}");

        while(curStunTime >= 0)
        {
            curStunTime -= Time.deltaTime;
            yield return null;
        }

        curStunTime = 0;
        isStun = false;
    }

    public virtual void AbilltyUp()
    {
        maxHp += statureAbillty.hp;
        damage += statureAbillty.damage;
        defensive += statureAbillty.defensive;
    }

    protected void AbilltyLoad(string path)
    {
        statureAbillty = new Data.StatureAbillty();
        TextAsset dataFile = Resources.Load<TextAsset>($"Data/{path}");
        statureAbillty = JsonUtility.FromJson<Data.StatureAbillty>(dataFile.text);
    }
}