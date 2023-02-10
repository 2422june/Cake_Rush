using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    enum PlayerState
    {
        Idle,
        Move,
        Attack, 
        Stun,
        Die,
        Skill_Lightning,
        Skill_Cokeshot,
        Skill_Shootingstar,
        Skill_Cakerush
    }
    
    PlayerState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            switch(_state)
            {
                case PlayerState.Stun:
                    animator.CrossFade("Stun", 0);
                    break;
                case PlayerState.Die:
                    animator.CrossFade("Die", 0);
                    break;
                case PlayerState.Idle:
                    animator.CrossFade("Idle", 0);
                    break;
                case PlayerState.Move:
                    animator.CrossFade("Move", 0);
                    break;
                case PlayerState.Attack:
                    animator.CrossFade("Attack", 0);
                    break;
                case PlayerState.Skill_Lightning:
                    animator.CrossFade("Lightning", 0, -1);
                    break;
                case PlayerState.Skill_Cokeshot:
                    animator.CrossFade("Cokeshot", 0, -1);
                    break;
                case PlayerState.Skill_Shootingstar:
                    animator.CrossFade("Shootingstar", 0, -1);
                    break;
            }
        }
    }
    Animator animator;
    PlayerStat stat;
    Skill_Lightning skill_lightning;
    Skill_Cokeshot skill_cokeshot;
    Skill_Shootingstar skill_shootingstar;
    Skill_Cakerush skill_cakerush;
    PlayerState _state;
    Vector3 destPos;
    
    int mask = (1 << (int) Define.Layer.Ground) | (1 << (int)Define.Layer.Mob) | (1 << (int)Define.Layer.Player) | (1 << (int)Define.Layer.Unit);
    bool useSkill = false;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (State == PlayerState.Die || State == PlayerState.Stun)
            return;
        
        switch(State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Move:
                UpdateMove();
                break;
            case PlayerState.Attack:
                UpdateAttack();
                break;
        }

        OnMouseClickEvent();
    }

    protected override void Init()
    {
        base.Init();
        stat = gameObject.GetOrAddComponent<PlayerStat>();
        skill_lightning = gameObject.GetOrAddComponent<Skill_Lightning>();
        skill_cokeshot = gameObject.GetOrAddComponent<Skill_Cokeshot>();
        skill_shootingstar = gameObject.GetOrAddComponent<Skill_Shootingstar>();
        skill_cakerush = gameObject.GetOrAddComponent<Skill_Cakerush>();

        navMeshAgent.speed = stat.MoveSpeed;
    }

    void OnMouseClickEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                if (hit.collider != null)
                {
                    destPos = hit.transform.position;
                    State = PlayerState.Move;

                    if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                        target = null;
                    else
                        target = hit.transform.gameObject;
                }
            }
        }
    }

    void OnKeyboardEvent()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(Input.GetMouseButtonDown(0))
            {
                State = PlayerState.Skill_Lightning;
            }
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            if (Input.GetMouseButtonDown(0))
            {
                State = PlayerState.Skill_Cokeshot;
            }
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            if (Input.GetMouseButtonDown(0))
            {
                State = PlayerState.Skill_Shootingstar;
            }
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            State = PlayerState.Skill_Cakerush;
        }
    }

    protected override void UpdateIdle()
    {

    }

    protected override void UpdateMove()
    {
        if(target != null)
        {
            float distance = (target.transform.position - transform.position).magnitude;

            if(distance < stat.AttackRange)
            {
                State = PlayerState.Attack;
                return;
            }
        }

        Vector3 dir = destPos - transform.position;

        if(dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            navMeshAgent.SetDestination(destPos);
        }
    }

    protected override void UpdateAttack()
    {

    }

    public override void OnStun(float time)
    {
        base.OnStun(time);
        State = PlayerState.Stun;
    }

    void OnHitEvent()
    {

    }

    void OnDieEvent()
    {

    }
}
