using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] int level = 1;
    [SerializeField] int maxLevel;
    [SerializeField] float currentExp;
    [SerializeField] float maxExp;
    
    public int Level { get { return level; } set { level = value; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value; } }
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }
    
    Dictionary<int, Data.PlayerStat> stat = new Dictionary<int, Data.PlayerStat>();

    public override void Init()
    {
        stat = Managers.Data.PlayerStat;
        maxLevel = stat.Count;
        level = 1;

        SetStat();
    }

    protected virtual void SetStat()
    {
        hp = stat[level].hp;
        maxHp = stat[level].maxHp;
        damage = stat[level].damage;
        attackSpeed = stat[level].attackSpeed;
        attackRange = stat[level].attackRange;
        defense = stat[level].defense;
        moveSpeed = stat[level].moveSpeed;
        eyeSight = stat[level].eyeSight;
        dropResource = stat[level].dropResource;
        spawnTime = stat[level].spawnTime;
        returnExp = stat[level].returnExp;
        MaxExp = stat[level].maxExp;
    }
}
