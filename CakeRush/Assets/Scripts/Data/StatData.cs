using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class Stat
    {
        public float hp;
        public float maxHp;
        public float damage;
        public float attackSpeed;
        public float attackRange;
        public float defense;
        public float moveSpeed;
        public float eyeSight;
        public int[] dropResource;
        public float spawnTime;
        public float returnExp;
    }

    [System.Serializable]
    public class PlayerStat : Stat
    {
        public int level;
        public float maxExp;
    }

    [System.Serializable]
    public class PlayerStatData : ILoader<int, PlayerStat>
    {
        public List<PlayerStat> stats = new List<PlayerStat>();

        public Dictionary<int, PlayerStat> MakeDict()
        {
            Dictionary<int, PlayerStat> dict = new Dictionary<int, PlayerStat>();

            foreach (PlayerStat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
}
