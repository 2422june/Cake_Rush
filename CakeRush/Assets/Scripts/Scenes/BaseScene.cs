using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        GameObject eventSystem = FindObjectOfType<EventSystem>().gameObject;

        if (eventSystem == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public virtual void Clear()
    {

    }
}
