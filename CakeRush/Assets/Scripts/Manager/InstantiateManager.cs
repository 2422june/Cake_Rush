using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : ManagerBase
{

    public T Instantiate<T>(string path) where T : Object
    {
        T go = Instantiate<T>(Resources.Load<T>($"Prefabs/{path}"));
        Debug.Log(go);
        return go;
    }

    public GameObject ResourcesLoad(string path, Transform parent)
    {
        GameObject obj = Instantiate<GameObject>($"Prefabs/{path}");
        obj.transform.SetParent(parent);
        return obj;
    }
}
