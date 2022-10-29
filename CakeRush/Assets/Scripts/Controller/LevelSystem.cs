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
    public float[] maxExp { get; private set; } = new float[10];

    private void Awake()
    {
        int needMaxExp = 100;
        playerController = GetComponent<PlayerController>();

        for (int i = 0; i < maxLevel - 1; i++)
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
        if (maxLevel <= curLevel + 1) return;
        
        if(curExp >= maxExp[curLevel])
        {
            if(curLevel + 1 == maxLevel)
            {
                SetLevel();
                playerController.AbilltyUp();
                playerController.AbilltyUp();
                UIManager.instance.SetPlayerStat();
                return;                
            }

            SetLevel();

            curExp = 0;
            playerController.AbilltyUp();
            UIManager.instance.SetPlayerStat();


            if (playerController.lightning.level < playerController.lightning.maxSkillLevel)
                UIManager.instance.lightningLevelUp.SetActive(true);

            if (playerController.cokeShot.level < playerController.cokeShot.maxSkillLevel)
                UIManager.instance.cokeShotLevelUp.SetActive(true);

            if (playerController.shootingStar.level < playerController.shootingStar.maxSkillLevel)
                UIManager.instance.shootingStarLevelUp.SetActive(true);

            if (playerController.levelSystem.curLevel > 5 && playerController.cakeRush.isSkillable == false)
                UIManager.instance.cakeRushLevelUp.SetActive(true);

        }
    }

    public void GetExp(float returnExp)
    {
        if(curLevel < maxLevel)
        {
            curExp += returnExp;
            LevelUp();
            UIManager.instance.SetPlayerExp();
        }

        Debug.Log(curLevel);
    }

    public void SkillLevelUp <T> (T skill) where T : SkillBase
    {
        if (skill.isSkillable == false)
        {
            skill.isSkillable = true;
            skillPoint--;
        }
        else
        {
            if (skill.maxSkillLevel > skill.level)
            {
                skill.LevelUp();
                skill.skillStat[skill.level].currentCoolTime = skill.skillStat[skill.level - 1].currentCoolTime;
                skillPoint--;
            }
        }

        if(skillPoint == 0)
        {
            UIManager.instance.lightningLevelUp.SetActive(false);
            UIManager.instance.cokeShotLevelUp.SetActive(false);
            UIManager.instance.shootingStarLevelUp.SetActive(false);
            UIManager.instance.cakeRushLevelUp.SetActive(false);
        }

        if (playerController.cakeRush.isSkillable == true)
            UIManager.instance.cakeRushLevelUp.SetActive(false);

        Debug.Log($"{skill.GetType()} skill level up {skill.level} / current skill point : {skillPoint}");
    }
}
