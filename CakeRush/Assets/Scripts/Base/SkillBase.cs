using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillStat
{
    public float coolTime;          //Cool time   
    public float currentCoolTime;   //Current cool time
    public bool isCoolTime;         //Is the current skill available?

    public IEnumerator CurrentCoolTime()
    {
        isCoolTime = true;

        currentCoolTime = coolTime;

        while (currentCoolTime >= 0)
        {
            currentCoolTime -= Time.deltaTime;

            yield return null;
        }

        isCoolTime = false;
        currentCoolTime = 0;
    }
}

public class SkillBase : MonoBehaviour
{
    [SerializeField] protected GameObject skillEffect;
    public GameObject rangeViewObj;
    public SkillStat[] skillStat;
    public bool isSkillable { get; set; } = false; 
    public bool isSkillUsed { get; set; }
    public int maxSkillLevel { get; protected set; } = 2;
    public float range { get; set; }
    public int level { get; protected set; }

    protected virtual void Awake()
    {
        GameObject temp = Instantiate(rangeViewObj);
        temp.transform.position = transform.position;
        temp.SetActive(false);
        temp.transform.parent = transform;
        temp.transform.localScale = rangeViewObj.transform.localScale;
        rangeViewObj = temp;
        level = 0;
    }

    public virtual void UseSkill(int skillLevel, Vector3 point)
    {
        if (!skillStat[skillLevel].isCoolTime && isSkillable == true)
        {
            Debug.Log("Check");
            StartCoroutine(skillStat[skillLevel].CurrentCoolTime());
        }
        else
        {
            return;
        }
    }

    public virtual void LevelUp()
    {
        level++;
    }
}
