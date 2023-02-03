using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : ManagerBase
{
    public static SceneManager instance;
    private WaitUntil LoadingDelay;
    private string nextSceneName;
    public string nowScene;

    public override void Init()
    {
        nowScene = Define.Scene.Loading.ToString();
        //LoadingDelay = new WaitUntil(() => GetSceneName().Equals(nextSceneName));
    }

    public string GetSceneName()
    {
        return nowScene;
    }

    public bool IsSameSceneName(Define.Scene target)
    {

        return (nowScene.Equals(target.ToString()));
    }

    public void LoadScene(Define.Scene next)
    {
        nextSceneName = next.ToString();

        //SM.LoadScene(nextSceneName);

        StartCoroutine(LoadingCycle());
    }

    private IEnumerator LoadingCycle()
    {
        yield return LoadingDelay;

        Managers.instance._game.OnLoadingScene();
    }
}
