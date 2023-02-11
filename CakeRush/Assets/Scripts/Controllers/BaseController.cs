using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class BaseController : MonoBehaviourPunCallbacks
{
    public Define.Creature Type { get { return _type; } }
    [SerializeField] protected Define.Creature _type;
    protected NavMeshAgent navMeshAgent;
    protected GameObject target;
    
    protected virtual void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void UpdateIdle()
    {

    }

    protected virtual void UpdateMove()
    {

    }

    protected virtual void UpdateAttack() 
    {

    }

    protected virtual void UpdateDie()
    {

    }

    public virtual void OnStun(float time)
    {
        StartCoroutine(CoStun(time));
    }

    protected IEnumerator CoStun(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
