using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SM = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    private WaitUntil LoadingDelay;
    private string nextSceneName;

    public void Init()
    {
        LoadingDelay = new WaitUntil(() => GetSceneName().Equals(nextSceneName));
    }

    public string GetSceneName()
    {
        return SM.GetActiveScene().name;
    }

    public bool IsSameSceneName(Define.Scene target)
    {

        return (SM.GetActiveScene().name.Equals(target.ToString()));
    }

    public void LoadScene(Define.Scene next)
    {
        nextSceneName = next.ToString();

        SM.LoadScene(nextSceneName);

        StartCoroutine(LoadingCycle());
    }

    private IEnumerator LoadingCycle()
    {
        yield return LoadingDelay;

        GameManager.instance.OnLoadingScene();
    }
}
