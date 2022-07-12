using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSnackController : UnitBase
{

    protected override void Awake()
    {
        DataLoad("StoneSnack");
        base.Awake();
        navMashAgent.speed = moveSpeed; 
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack(Transform target)
    {        
        base.Attack(target);
        this.Attack();
    }

    protected void Attack()
    {        
        animator.SetBool("Move", false);
        animator.SetBool("Attack", true);
        SoundManager.instance.PlayClip(ref source, Define.GameSound.FX_Unit_Attack);
    }

    protected override void Stop()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            targetTransform = null;
            navMashAgent.SetDestination(transform.position);
            navMashAgent.isStopped = true;
            navMashAgent.isStopped = false; 
            StopAllCoroutines();
            state = CharacterState.Idle;
            animator.SetBool("Move", false);
            animator.SetBool("Attack", false);
        }
    }
}
