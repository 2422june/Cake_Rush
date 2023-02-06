using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : ManagerBase
{

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject go;

        go = Instantiate<GameObject>(Resources.Load<GameObject>($"Prefabs/{path}"));
        if (parent != null)
        {
            go.transform.SetParent(parent);
        }
        return go;
    }
}
