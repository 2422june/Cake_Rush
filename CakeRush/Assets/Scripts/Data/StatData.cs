using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Stat
    {
        public int[] dropResource = new int[3];
        public float hp;
        public float maxHp;
        public float damage;
        public float defense;
        public float moveSpeed;
        public float attackSpeed;
        public float spawnTime;
        public float attackRange;
        public float returnExp;
        public float eyeSight;
    }
    public class PlayerStat : Stat
    {
        public int level;
        public float maxExp;
    }

    public class PlayerStatData : ILoader<int, PlayerStat>
    {
        List<PlayerStat> PlayerStat = new List<PlayerStat>();

        public Dictionary<int, PlayerStat> MakeDict()
        {
            Dictionary<int, PlayerStat> dict = new Dictionary<int, PlayerStat>();

            foreach (PlayerStat stat in PlayerStat)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
}
