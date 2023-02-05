using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : ManagerBase
{
    private WaitUntil _loadingDelay;
    private int _loadValue;

    private string _nextSceneName;
    private string _nowScene;

    private Dictionary<string, GameObject> _scenes = new Dictionary<string, GameObject>();
    private string[] _sceneNames = { "Loading", "Title" };

    public override void Init()
    {
        GameObject newScene;
        foreach (string name in _sceneNames)
        {
            newScene = Managers.instance._instantiate.Instantiate<GameObject>($"Scenes/{name}");
            newScene.SetActive(false);
            _scenes.Add(name, newScene);
        }

        LoadScene(Define.Scene.Loading);
    }

    public void SetLoadingValue(int value)
    {
        _loadValue = value;
    }

    public string GetSceneName()
    {
        return _nowScene;
    }

    public bool IsSameSceneName(Define.Scene target)
    {

        return (_nowScene.Equals(target.ToString()));
    }

    public void LoadScene(Define.Scene next)
    {
        _nextSceneName = next.ToString();

        StartCoroutine(LoadingCycle());
    }

    public void OnLoadScene()
    {
        if(_scenes[_nowScene])
            _scenes[_nowScene].SetActive(false);

        _nowScene = _nextSceneName;
        _scenes[_nowScene].SetActive(true);
    }

    private IEnumerator LoadingCycle()
    {
        yield return _loadingDelay;

        Debug.Log("On Loading");
    }
}
