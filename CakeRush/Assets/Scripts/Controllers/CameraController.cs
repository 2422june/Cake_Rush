using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target = null;
    [SerializeField] Vector3 _offset = new Vector3(0, 15, -12);

    private void Start()
    {
        target = Managers.Game.GetPlayer.gameObject;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            target = Managers.Game.GetPlayer.gameObject;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 offset = target.transform.position + _offset;
        transform.position = offset;
    }
}
