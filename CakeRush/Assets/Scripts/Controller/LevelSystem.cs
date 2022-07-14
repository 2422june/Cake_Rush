using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSystem : MonoBehaviour
{
    private PlayerController playerController;
    public float curExp { get; private set; }
    public int curLevel { get; private set; }
    public int skillPoint { get; private set; } = 1;

    private const int maxLevel = 10;     //max index number
    [SerializeField] public float[] maxExp;

    private void Awake()
    {
        int needMaxExp = 100;
        playerController = GetComponent<PlayerController>();

        for(int i = 0; i < maxLevel; i++)
        {
            maxExp[i] = needMaxExp;
            needMaxExp += 100;
        }
    }

    public int SetLevel()
    {
        skillPoint++;
        return curLevel++;
    }

    private void LevelUp()
    {
        if(curExp >= maxExp[curLevel] && maxLevel > curLevel)
        {
            curExp = 0;
            SetLevel();
            playerController.AbilltyUp();
            UiManager.instance.SetPlayerStat();
        }
    }

    public void GetExp(float returnExp)
    {
        curExp += returnExp;
        LevelUp();

        UiManager.instance.SetPlayerExp();
    }

    public void SkillLevelUp <T> (T skill) where T : SkillBase
    {
        if (skill.isSkillable == false)
        {
            skill.isSkillable = true;
        }
        else
        {
            if (skill.maxSkillLevel > skill.level)
            {
                skill.LevelUp();
                skill.skillStat[skill.level].currentCoolTime = skill.skillStat[skill.level - 1].currentCoolTime;
            }
        }

        skillPoint--;
        
        Debug.Log($"{skill.GetType()} skill level up {skill.level} / current skill point : {skillPoint}");
    }
}
