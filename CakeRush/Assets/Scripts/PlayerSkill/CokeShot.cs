using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeShot : SkillBase
{
    public float radius { get; set; }

    private const float skillHoldTime = 4f;
    [SerializeField] private float[] delay;
    private float currentHoldTime;
    private WaitForSeconds delayTime;
    private PlayerController playerController;

    [SerializeField] private float[] damage;

    protected override void Awake()
    {
        delayTime = new WaitForSeconds(delay[level]);
        skillEffect = Resources.Load<GameObject>("Effect/Skill/CokeShot");
        rangeViewObj = Resources.Load<GameObject>("Prefabs/RangeView/CokeShot");
        playerController = GetComponent<PlayerController>();
        maxSkillLevel = 2;
        base.Awake();
    }

    public override void UseSkill(int skillLevel, Vector3 point)
    {
        if (!skillStat[skillLevel].isCoolTime && isSkillable == true)
        {
            StartCoroutine(skillStat[skillLevel].CurrentCoolTime(UiManager.instance.cokeShotCooltime, UiManager.instance.cokeShotActive));
        }
        else
        {
            return;
        }

        StartCoroutine(SkillActive(point));
    }

    private IEnumerator SkillActive(Vector3 point)
    {
        yield return new WaitForSeconds(0.5f);
        currentHoldTime = skillHoldTime;
        point.y = 1f;
        GameObject temp = Instantiate(skillEffect, point, Quaternion.Euler(0, 1, 0));

        while (currentHoldTime >= 0)
        {
            Collider[] overlapShpere = Physics.OverlapSphere(point, radius, GameProgress.instance.selectableLayer);
            
            Factor(overlapShpere);
            currentHoldTime -= delay[level];

            yield return delayTime;
        }

        Destroy(temp);
    }

    private void Factor (Collider[] colliders)
    {
        if(colliders.Length < 1)
        {
            return;
        }

        for(int i = 0; i < colliders.Length; i++)
        {
            Debug.Log($"{colliders[i].name} Coke Shot");
            colliders[i].GetComponent<CharacterBase>().Hit(damage[level]);
            
            if(colliders[i].GetComponent<CharacterBase>().curHp <= 0)
            {
                playerController.levelSystem.GetExp(colliders[i].GetComponent<CharacterBase>().returnExp);
            }
        }
    }
}

