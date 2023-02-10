using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] int level;
    [SerializeField] int maxLevel;
    [SerializeField] float currentExp;
    [SerializeField] float maxExp;
    
    public int Level { get { return level; } set { level = value; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value; } }
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }

    protected override void Init()
    {
           
    }
}
