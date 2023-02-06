using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : ManagerBase
{
    [SerializeField]
    private Slider _loadSlider;
    private int _loadValue;

    private bool _isFadeOuting;
    private bool _isFadeIning;

    private string _nextSceneName;
    private string _nowScene;

    private Dictionary<string, GameObject> _scenes = new Dictionary<string, GameObject>();
    private string[] _sceneNames = { "Loading", "Title", "Lobby" };

    public override void Init()
    {
        GameObject newScene;
        foreach (string name in _sceneNames)
        {
            newScene = Managers.instance._instantiate.Instantiate($"Scenes/{name}");
            newScene.SetActive(false);
            _scenes.Add(name, newScene);
        }

        _scenes["Loading"].SetActive(true);
        _loadSlider = Managers.instance._ui.Find<Slider>("LoadingSlider", _scenes["Loading"].transform);

        LoadScene(Define.Scene.Loading);
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
        _loadValue = 0;
        _loadSlider.value = _loadValue;
        FadeOut();
    }

    public void SetLoadingValue(int value)
    {
        _loadValue = value;
        if(_isFadeOuting) { return; }

        _loadSlider.value = _loadValue;

        if (_loadValue >= 100)
        {
            _loadValue = 0;
            SwapScene();
            OnLoadScene();
        }
    }

    void SwapScene()
    {
        if (_scenes[_nowScene])
            _scenes[_nowScene].SetActive(false);

        _nowScene = _nextSceneName;
        _scenes[_nowScene].SetActive(true);
    }

    void OnLoadScene()
    {

        FadeIn();
    }

    void FadeIn()
    {
        _isFadeIning = true;
    }

    void OnFadeIn()
    {
        _isFadeIning = false;
    }

    void FadeOut()
    {
        _isFadeOuting = true;
    }

    void OnFadeOut()
    {
        _isFadeOuting = false;
        _loadSlider.value = _loadValue;
    }
}
