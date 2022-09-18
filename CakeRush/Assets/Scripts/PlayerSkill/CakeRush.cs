using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CakeRush : SkillBase
{
    private const float skillFactor = 40f;
    private float addSpeed;
    private float addDamage;

    protected override void Awake()
    {
        skillEffect = Resources.Load<GameObject>("Effect/Skill/CakeRush");
        maxSkillLevel = 0;
    }

    public IEnumerator UnitCakeRush(Transform unit)
    {
        UnitBase unitBase = unit.GetComponent<UnitBase>();

        addSpeed = unitBase.moveSpeed + (unitBase.moveSpeed / 100 * skillFactor);
        addDamage = unitBase.damage + (unitBase.damage / 100 * skillFactor);

        float t_moveSpeed = unitBase.moveSpeed;         //Move speed temp
        float t_damage = unitBase.damage;     //Attack speed temp

        unitBase.moveSpeed = addSpeed;
        unitBase.damage = addDamage;
        unitBase.navMashAgent.speed = unitBase.moveSpeed;
        GameObject go = Instantiate(skillEffect, unit.transform.position, Quaternion.Euler(-90, 0, 0));
        go.transform.parent = unit;

        yield return new WaitForSeconds(15f);

        unitBase.moveSpeed = t_moveSpeed;
        unitBase.damage = t_damage;
        unitBase.navMashAgent.speed = unitBase.moveSpeed;
        UIManager.instance.SetPlayerStat();
    }

    public void UseSkill(int skillLevel)
    {
        if(!skillStat[skillLevel].isCoolTime && isSkillable == true)
        {
            for(int i = 0; i < GameManager.instance.rtsController.unitList.Count; i++)
            {
                GameManager.instance.rtsController.unitList[i].cakeRush.StartCoroutine(UnitCakeRush(GameManager.instance.rtsController.unitList[i].transform));
            }

            StartCoroutine(skillStat[skillLevel].CurrentCoolTime(UIManager.instance.cakeRushCooltime, UIManager.instance.cakeRushActive));
            UIManager.instance.SetPlayerStat();
        }
        else
        {
            return;
        }
    }
}
