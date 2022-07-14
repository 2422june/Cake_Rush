using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeTowerController : BuildBase
{
    [SerializeField] private GameObject bullet;
    Collider target;
    Vector3 firePos;
    
    protected override void Awake()
    {
        bullet = Resources.Load<GameObject>("Effect/TowerBullet");
        firePos = transform.Find("FirePos").position;
        isSpawnable = false;
        DataLoad("CokeTower"); 
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Attack());
    }

    protected override void Update()
    {
        base.Update();
    }

    //[SerializeField] private List<Collider> enemies = new List<Collider>();
    [SerializeField] private Collider[] enemies;
    IEnumerator Attack()
    {
        while(true)
        {
            enemies = Physics.OverlapSphere(transform.position, attackRange, GameProgress.instance.selectableLayer);
            float minDistance = 999999f;
            foreach(Collider enemy in enemies)
            {
                if (enemy.gameObject.GetComponent<EntityBase>() is BuildBase)
                {
                    Debug.Log("Find agian");
                    continue;
                }
                Debug.Log(enemy); 
                if((enemy.transform.position - transform.position).magnitude <= minDistance)
                {
                    minDistance = (enemy.transform.position - transform.position).magnitude;
                    target = enemy;
                }
            }   
            if(target != null) // attack
            {
                yield return new WaitForSeconds(1f);
                if(Vector3.Distance(target.transform.position, transform.position) > attackRange)
                {
                    target = null;
                    continue;
                }
                GameObject newBullet = Instantiate(bullet, firePos, Quaternion.identity, transform);
                newBullet.GetComponent<TowerBulletController>().target = target.transform;
                SoundManager.instance.PlayClip(ref source, Define.GameSound.FX_CokeTower_Attack);
                Debug.Log($"{target.gameObject.name}");
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return null;
        }
    }
}