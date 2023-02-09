using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return Object.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type)
    {
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        char[] letters = name.ToCharArray();
        letters[0] = char.ToUpper(letters[0]);

        return new string(letters);
    }
}
