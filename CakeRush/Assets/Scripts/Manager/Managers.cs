using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers instance;

    public GameManager          _game    = null;
    public UIManager            _ui      = null;
    public SceneManager         _scene   = null;
    public SoundManager         _sound   = null;
    public ServerManager        _server = null;
    public InstantiateManager   _instantiate = null;


    T FindManager<T>() where T : MonoBehaviour
    {
        T instance = GetComponent<T>();
        if (instance == null)
        {
            instance = gameObject.AddComponent<T>();
        }

        return instance;
    }

    T Init<T>() where T : ManagerBase
    {
        T instance = FindManager<T>();

        instance.Init();
        return instance;
    }

    ServerManager Init()
    {
        ServerManager instance = FindManager<ServerManager>();

        instance.Init();
        return instance;
    }

    void SetupManagers()
    {
        _instantiate = Init<InstantiateManager>();
        _scene = Init<SceneManager>();
        //_server = Init();
        //_game = Init<GameManager>();
        //_ui = Init<UIManager>();
        //_sound = Init<SoundManager>();

    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            SetupManagers();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
