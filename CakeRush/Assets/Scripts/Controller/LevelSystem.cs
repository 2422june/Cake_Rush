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

    private const int maxLevel = 9;     //max index number
    [SerializeField] private float []maxExp;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
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
