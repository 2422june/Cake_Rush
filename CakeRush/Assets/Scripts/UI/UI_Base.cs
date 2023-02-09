using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i < objects.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = gameObject.FindChild<GameObject>(names[i], true);
            else
                objects[i] = gameObject.FindChild<T>(names[i], true);

            if (objects[i] == null)
                Debug.Log($"Not found this object {name}");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if(_objects.TryGetValue(typeof(T), out objects) == false)
        {
            Debug.Log($"Faild get object");
            return null;
        }

        return objects[idx] as T;
    }

    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    protected TMP_InputField GetInputFild(int idx) { return Get<TMP_InputField>(idx); }
    protected Slider GetSlider(int idx) { return Get<Slider>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
}
