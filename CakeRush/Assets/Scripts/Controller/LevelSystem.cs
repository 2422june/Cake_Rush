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

    private const int maxLevel = 11;     //max index number
    public float[] maxExp { get; private set; } = new float[11];

    private void Awake()
    {
        int needMaxExp = 100;
        playerController = GetComponent<PlayerController>();

        for (int i = 0; i < 11; i++)
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
        if(curExp >= maxExp[curLevel] && maxLevel - 1> curLevel)
        {
            SetLevel();

            if(curLevel == maxLevel)
            {
                playerController.AbilltyUp();
                playerController.AbilltyUp();
                UiManager.instance.SetPlayerStat();
                return;                
            }

            curExp = 0;
            playerController.AbilltyUp();
            UiManager.instance.SetPlayerStat();


            if (playerController.lightning.level < playerController.lightning.maxSkillLevel)
                UiManager.instance.lightningLevelUp.SetActive(true);

            if (playerController.cokeShot.level < playerController.cokeShot.maxSkillLevel)
                UiManager.instance.cokeShotLevelUp.SetActive(true);

            if (playerController.shootingStar.level < playerController.shootingStar.maxSkillLevel)
                UiManager.instance.shootingStarLevelUp.SetActive(true);

            if (playerController.levelSystem.curLevel > 5 && playerController.cakeRush.isSkillable == false)
                UiManager.instance.cakeRushLevelUp.SetActive(true);

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
        
        if(skillPoint == 0)
        {
            UiManager.instance.lightningLevelUp.SetActive(false);
            UiManager.instance.cokeShotLevelUp.SetActive(false);
            UiManager.instance.shootingStarLevelUp.SetActive(false);
            UiManager.instance.cakeRushLevelUp.SetActive(false);
        }

        if (playerController.cakeRush.isSkillable == true)
            UiManager.instance.cakeRushLevelUp.SetActive(false);

        Debug.Log($"{skill.GetType()} skill level up {skill.level} / current skill point : {skillPoint}");
    }
}
