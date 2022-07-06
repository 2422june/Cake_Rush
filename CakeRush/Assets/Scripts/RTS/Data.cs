using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    [SerializeField]
    public struct Stat
    {
        public float damage;
        public float hp;
        public float attackSpeed;
        public float attackRange;
        public float returnExp;
        public float eyeSight;
        public int[] cost;
        public int[] dropCost;
        public float defensive;
        public float spawnTime;
        public float moveSpeed;
    }

    [System.Serializable]
    public struct StatureAbillty
    {
        public float s_hp;
        public float s_damage;
        public float s_defensive;
    }
}

