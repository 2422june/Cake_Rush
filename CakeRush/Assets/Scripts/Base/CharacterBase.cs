using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//모든 캐릭터의 최상위 부모 클래스
public class CharacterBase : EntityBase
{
    public NavMeshAgent navMashAgent;
    protected Animator animator;
    protected Data.StatureAbillty statureAbillty;
    protected bool isStun;
    float curStunTime;

    protected override void Awake()
    {
        isStun = false;
        navMashAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        base.Awake();
    }
    
    protected override void Update()
    {
        if (isStun) return;
        if (team != rtsController.team) return;
        base.Update();
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

    protected void AbilltyUp()
    {
        maxHp += statureAbillty.s_hp;
        damage += statureAbillty.s_damage;
        defensive += statureAbillty.s_defensive;
    }

    protected void AbilltyLoad(string data)
    {
        statureAbillty = new Data.StatureAbillty();
        TextAsset dataFile = Resources.Load<TextAsset>($"Data/{data}");
        statureAbillty = JsonUtility.FromJson<Data.StatureAbillty>(dataFile.text);
    }
}