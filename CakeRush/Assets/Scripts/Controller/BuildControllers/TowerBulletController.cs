using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletController : MonoBehaviour
{
    float damage;
    public Transform target;
    [SerializeField] GameObject hitEffect;
    AudioSource source;
    void Awake()
    {
        damage = transform.parent.gameObject.GetComponent<CokeTowerController>().damage;
        hitEffect = transform.Find("TowerBullet_Hit").gameObject;
    }



    void Update()
    {
        transform.position += (target.position - transform.position).normalized * Time.deltaTime * 40f;
        if(Vector3.Distance(target.position, transform.position) <= 1f)
        {
            target.gameObject.GetComponent<EntityBase>().Hit(damage);
            hitEffect.GetComponent<ParticleSystem>().Play();
            hitEffect.transform.parent = transform.parent.parent;
            Managers.instance._sound.PlayClip(ref source, Define.GameSound.FX_CokeTower_Hit);
            Destroy(gameObject);
        }
    }
}