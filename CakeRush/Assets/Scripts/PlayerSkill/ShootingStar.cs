using UnityEngine;

public class ShootingStar : SkillBase
{
    public float stunTime { get; set; }
    [SerializeField] private float[] angleRange;
    [SerializeField] private Transform skillPos;
    [SerializeField] private GameObject slashEffect;
    private Renderer _renderer;
    private Material rangeViewMat;

    protected override void Awake()
    {
        skillEffect = Resources.Load<GameObject>("Effect/Skill/ShootingStar");
        rangeViewObj = Resources.Load<GameObject>("Prefabs/RangeView/ShootingStar");
        slashEffect = Resources.Load<GameObject>("Effect/Skill/ShootingStar_Slash");
        _renderer = rangeViewObj.GetComponent<Renderer>();
        rangeViewMat = _renderer.sharedMaterial;
        base.Awake();
        rangeViewMat.SetFloat("_Angle", angleRange[level]);
        maxSkillLevel = 2;

        
    }
    public override void UseSkill(int skillLevel, Vector3 point)
    {
        if (!skillStat[skillLevel].isCoolTime && isSkillable == true)
        {
            StartCoroutine(skillStat[skillLevel].CurrentCoolTime(Managers.instance._ui.shotingStarCooltime, Managers.instance._ui.shootingStarActive));
        }
        else
        {
            return;
        }
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5.0f, GameProgress.instance.selectableLayer);
        point.y -= 90;
        GameObject temp = Instantiate(slashEffect, skillPos.position, Quaternion.Euler(-105, 270, 0));
        Destroy(temp, 1f);
        for (int i = (int)-angleRange[skillLevel] / 2; i <= (int)angleRange[skillLevel] / 2; i += 10)
        {
            GameObject go = Instantiate(skillEffect, skillPos.position, Quaternion.Euler(0, point.y - i, 0));
            Destroy(go, 2);
        }

        if (colliders.Length < 2)
        {
            return;
        }

        SectorColision(colliders);
    }

    private void SectorColision(Collider[] colliders)
    {
        Vector3 dirction;
        float dotValue;

        dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange[level] / 2));

        for (int i = 0; i < colliders.Length; i++)
        {
            dirction = colliders[i].transform.position - transform.position;

            if (Vector3.Dot(dirction.normalized, transform.forward) > dotValue && colliders[i].GetType() != typeof(PlayerController))
            {
                StunEntity(colliders[i].gameObject.GetComponent<CharacterBase>());
            }
        }
    }


    private void StunEntity<T>(T unit) where T : CharacterBase
    {
        unit = unit as T;

        StartCoroutine(unit.Stun(stunTime));
    }

    public override void LevelUp()
    {
        base.LevelUp();
        rangeViewMat.SetFloat("_Angle", angleRange[level]);
    }
}
