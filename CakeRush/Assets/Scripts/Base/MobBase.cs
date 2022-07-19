using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;

//중립 몬스터의 부모 클래스
public class MobBase : CharacterBase
{
    [SerializeField] protected Vector3 targetPos;
    [SerializeField] protected EntityBase targetEntity;

    [SerializeField] protected Define.MobState state;
    protected Vector3 originPos;

    [SerializeField] bool isResetting = false;
    [SerializeField] bool isFighting = false;
    [SerializeField] bool isAttackable = true;
    protected override void Awake()
    {
        base.Awake();
        originPos = transform.position;
        state = Define.MobState.idle;
        navMashAgent.speed = moveSpeed;
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

        switch(state)
        {
            case Define.MobState.idle:
                Idle();
                break;
            case Define.MobState.attack:
                Attack();
                break;
            case Define.MobState.move:
                Move();
                break;
            case Define.MobState.retargeting:
                Retarget();
                break;
            case Define.MobState.reset:
                Reset();
                break;
            default:
                break;
        }
    }

    void Idle()
    {
        curHp = maxHp;
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        targetEntity.Hit(damage);
        if(targetEntity.curHp < 0)
        {
            state = Define.MobState.retargeting;
        }
        StartCoroutine(AttackCoolDown());
        state = Define.MobState.move;
    }

    void Move()
    {
        animator.SetBool("isMove", true);
        navMashAgent.SetDestination(targetPos);
        if(Vector3.Distance(transform.position, targetPos) < attackRange)
        {
            state = Define.MobState.attack;
            return;
        }

        if(Vector3.Distance(transform.position, originPos) > eyeSight)
        {
            state = Define.MobState.reset;
        }
    }

    public virtual void Hit(float hitDamage, Vector3 targetPos)
    {
        PV.RPC("OnHit", RpcTarget.All, hitDamage, targetPos);
        //OnHit(hitDamage, target);
    }

    [PunRPC]
    protected void OnHit(float hitDamage, Vector3 targetPos)
    {
        Debug.Log("OnHit");
        base.Hit(hitDamage);
        if (isResetting == false && isFighting == false)
        {
            this.targetPos = targetPos;
            state = Define.MobState.move;
        }
    }
    
    void Reset()
    {
        animator.SetBool("isMove", true);
        navMashAgent.SetDestination(originPos);
        if(Vector3.Distance(transform.position, originPos) < 4)
        {
            animator.SetBool("isMove", false);
            state = Define.MobState.idle;
        }
    }

    IEnumerator AttackCoolDown()
    {
        isAttackable = false;
        yield return new WaitForSeconds(attackSpeed);   
        isAttackable = true;
    }

    void Retarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, eyeSight, GameProgress.instance.selectableLayer);
        
        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.GetComponent<EntityBase>().curHp < 0) continue;
            targetEntity = collider.gameObject.GetComponent<EntityBase>();
            targetPos = collider.transform.position;
            break;
        }
        state = Define.MobState.move;
    }
}