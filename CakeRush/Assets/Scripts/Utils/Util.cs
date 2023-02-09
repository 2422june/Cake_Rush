using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        if (go == null)
            return null;

        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;


        if(recursive)
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);

                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();

                    if (component != null)
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = false)
    {
        if (go == null)
            return null;

        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }
}
