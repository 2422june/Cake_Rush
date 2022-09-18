using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : SkillBase
{
    [SerializeField] private float[] damage;

    protected override void Awake()
    {
        skillEffect = Resources.Load<GameObject>("Effect/Skill/Lightning");
        rangeViewObj = Resources.Load<GameObject>("Prefabs/rangeView/Lightning"); 
        //rangeViewMat = Resources.Load<Material>("Materials/RangeView/Lightning");
        maxSkillLevel = 2;
        base.Awake();
    }

    public void UseSkill(int skillLevel, Collider unit)
    {
        if (!skillStat[skillLevel].isCoolTime && isSkillable == true)
        {
            Debug.Log("Check");
            StartCoroutine(skillStat[skillLevel].CurrentCoolTime(UIManager.instance.lightningCooltime, UIManager.instance.lightningActive));
        }
        else
        {
            return;
        }

        Debug.Log("³«·Ú");
        
        Factor(unit.GetComponent<CharacterBase>());
    }

    private void Factor<T>(T unit) where T : CharacterBase
    {
        unit = unit as T;
        Instantiate(skillEffect, unit.transform.position, Quaternion.identity);
        unit.Hit(unit.curHp / 100 * damage[level]);
    }
}