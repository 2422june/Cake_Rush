using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자가 조종할 유닛의 부모클래스
public class UnitBase : CharacterBase
{
    public enum CharacterState
    {
        Idle,
        Move,
        Attack,
        Stun,
        Die,    
    }
    public CharacterState state;

    protected Transform targetTransform = null;
    public CakeRush cakeRush;
    public Camera teamCamera;
    public GameObject rangeViewObj;

    protected override void Awake()
    {
        base.Awake();


        if (PV.IsMine)
            tag = "Me_Unit";
        else
            tag = "Other_Unit";

        teamCamera = Camera.main;
        state = CharacterState.Idle;
        navMashAgent.speed = moveSpeed;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (!PV.IsMine) return;
        Idle();
        //Stop();
    }
    
    public void SelectUnit()
	{
		Marker.SetActive(true);
	}

	public void DeselectUnit()
	{
        Marker.SetActive(false);
	}

    protected virtual void Attack(Transform target)
    {        
        navMashAgent.isStopped = true;

        animator.SetBool("Move", false);
        animator.SetBool("Attack", true);
    }

    void Idle()
    {
        if(Input.GetMouseButtonDown(1) && Marker.activeSelf && isStun == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Stop();
                // attack cancel
                if(state == CharacterState.Attack)
                {
                    StopAllCoroutines();
                }
                // attack
                /*if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Selectable") && (hit.transform != this.transform))
                {
                    targetTransform = hit.transform; 
                    Debug.Log("Move To Attack");
                    StartCoroutine(OutToAttakRange(hit.transform));
                }
                // move
                else
                {
                    
                    state = CharacterState.Move;
                    Debug.Log("Move");
                    Move(hit.point);
                }*/

                if((hit.transform != this.transform))
                {
                    if(hit.transform.CompareTag("Monster") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Selectable"))
                    {
                        targetTransform = hit.transform; 
                        StartCoroutine(OutToAttakRange(hit.transform));
                    }
                    else
                    {
                        //move
                        state = CharacterState.Move;
                        Move(hit.point);
                    }
                }
                Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
           }
        }
    }
    protected IEnumerator Arrive()
    {
        while(true)
        {
            if(!navMashAgent.pathPending)
            {
               if(navMashAgent.remainingDistance <= navMashAgent.stoppingDistance)
               {
                   if(!navMashAgent.hasPath || navMashAgent.velocity.sqrMagnitude == 0)
                    {
                        Debug.Log("Arrive");
                        animator.SetBool("Move", false);
                        state = CharacterState.Idle;
                        break;
                   }
               }
            }

            yield return null;
        }
    }    
    public virtual void Move(Vector3 destination)
    {
        state = CharacterState.Move;

        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);

        navMashAgent.isStopped = false; 
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), 90);
        navMashAgent.SetDestination(destination);
        StartCoroutine(Arrive());
    }

    protected virtual void Stop()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            targetTransform = null;
            navMashAgent.SetDestination(transform.position);
            navMashAgent.isStopped = true;
            navMashAgent.isStopped = false; 
            StopAllCoroutines();
            state = CharacterState.Idle;
        }
    }


    public virtual IEnumerator OutToAttakRange(Transform target)
    {
        state = CharacterState.Attack;

        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);

        while(attackRange < (target.position - transform.position).sqrMagnitude)
        {
            Move(target.position);
            yield return null;
        }

        //Move(transform.position);
        StartCoroutine(BasicAttack(target));
    
    }   

	//Default Attack on Entities
	protected virtual IEnumerator BasicAttack(Transform target)
	{
        state = CharacterState.Attack;

        animator.SetBool("Move", false);
        animator.SetBool("Attack", true);

		while((target.position - transform.position).sqrMagnitude < attackRange)
		{
            // Vector3 dir = target.position - transform.position;
			// Quaternion quat = Quaternion.LookRotation(dir);
			// transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
            transform.LookAt(target);
            Attack(target);
            
			yield return new WaitForSeconds(attackSpeed);
		}
        
        StartCoroutine(OutToAttakRange(target));
	}
}