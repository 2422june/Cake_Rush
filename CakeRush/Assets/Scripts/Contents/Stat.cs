using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float defense;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float spawnTime;
    [SerializeField] protected float returnExp;
    [SerializeField] protected float eyeSight;
    [SerializeField] protected int[] dropResource = new int[3];
    

    public float Hp { get { return hp; } set { hp = value; } }
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public float Defense { get { return defense; } set { defense = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float SpawnTime { get { return spawnTime; } set { spawnTime = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float ReturnExp { get { return returnExp; } set { returnExp = value; } }
    public float EyeSight { get { return eyeSight; } set { eyeSight = value; } }
    public int[] DropResource { get { return DropResource; } set { DropResource = value; } }
    
    public abstract void Init();
}
