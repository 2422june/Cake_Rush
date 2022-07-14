using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillStat
{
    public float coolTime;          //Cool time   
    public float currentCoolTime;   //Current cool time
    public bool isCoolTime;         //Is the current skill available?

    public IEnumerator CurrentCoolTime(TMPro.TMP_Text skillCoolTime, GameObject go)
    {
        string currentCoolTimeText;
        isCoolTime = true;

        currentCoolTime = coolTime;
        go.SetActive(true);

        while (true)
        {
            if (currentCoolTime <= 0.01) break;

            currentCoolTimeText = string.Format("{0:0.0}", currentCoolTime);
            skillCoolTime.text = $"{currentCoolTimeText}";
            currentCoolTime -= Time.deltaTime;
                
            yield return null;
        }

        isCoolTime = false;
        skillCoolTime.text = "";
        go.SetActive(false);
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

    }

    public virtual void LevelUp()
    {
        level++;
    }
}
