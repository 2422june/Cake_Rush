using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : ManagerBase
{
    public GameObject ResourcesLoad(string path)
    {
        return Resources.Load<GameObject>($"Prefabs/{path}");
    }

    public GameObject ResourcesLoad(string path, Transform parent)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefabs/{path}");
        obj.transform.SetParent(parent);
        return obj;
    }
}
