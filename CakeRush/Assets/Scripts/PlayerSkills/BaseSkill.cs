using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public float CurrentCooltime { get; protected set; }
    public float Cooltime { get; protected set; }
    public GameObject ViewRange { get; set; }

    protected float radius;
    protected bool isSkillable;   
    
    protected virtual void Init()
    {
        GameObject root = GameObject.Find($"{gameObject.name}_Root");

        if(root == null)
        {
            root = new GameObject { name = $"{gameObject.name}_Root" };
            root.transform.parent = Managers.Game.GetPlayer.transform;
        }

        ViewRange = root.FindChild("ViewRange", true);
        ViewRange.gameObject.SetActive(false);
    }

    public virtual void UseSkill()
    {
        CurrentCooltime = Cooltime;
        isSkillable = false;
    }
}
