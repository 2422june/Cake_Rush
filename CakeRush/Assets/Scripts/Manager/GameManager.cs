using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Init();

            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region elements

    public Define.Scene nowScene, nextScene;
    private bool isChangingScene;

    public float playerLevel;
    public int[] cost;
    public bool isSpawnable;
    private bool nowInGame;
    public bool nowMatching, nowCloseMatching, nowInRoom;
    public RTSController rtsController;

    [Space(10)]
    [Header("ONLY DEVELOPER")]
    public bool DevelopMode;

    #endregion

    #region find Function
    protected virtual GameObject FindElement(string folder, string path)
    {
        return Instantiate(Resources.Load<GameObject>($"Prefabs/${folder}/{path}"));
    }
    protected virtual GameObject FindElement(string path)
    {
        return Instantiate(Resources.Load<GameObject>($"Prefabs/UI/{path}"));
    }

    protected virtual GameObject FindElement(GameObject parent, string name)
    {
        return parent.transform.Find(name).gameObject;
    }
    #endregion

    #region Setting

    private void Init()
    {
        nowScene = Define.Scene.AwakeLoad;

        ServerManager.instance = GetComponent<ServerManager>();
        ServerManager.instance.Init();

        SceneManager.instance = GetComponent<SceneManager>();
        SceneManager.instance.Init();

        UIManager.instance = GetComponent<UIManager>();
        UIManager.instance.Init();

        GameProgress.instance = GetComponent<GameProgress>();
        GameProgress.instance.Init();

        ServerManager.instance.ServerConnect();
    }

    public void OnConnectTedServer()
    {
        if (SceneManager.instance.IsSameSceneName(Define.Scene.AwakeLoad))
        {
            nowInGame = false;

            SetScene(Define.Scene.Title);
        }
        else if (SceneManager.instance.IsSameSceneName(Define.Scene.InGame))
        {
            rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
        }
        
    }

    #endregion

    #region Functions

    public void GameQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
                Application.Quit();
    }

    public void OnJoinedLobby()
    {
        UIManager.instance.OffLoading();
    }

    private void EnterInGame()
    {
        nowInGame = true;
        nowInRoom = false;
        StartCoroutine(InGameSeting());
    }

    private IEnumerator InGameSeting()
    {
        while(true)
        {
            if(SceneManager.instance.GetSceneName().Equals("InGame"))
            {
                rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
                GameProgress.instance.NowArriveInGame();
                break;
            }

            yield return null;
        }
    }

    #endregion

    #region Button Function

    public void OnClickStartInTitle()
    {
        SetScene(Define.Scene.Lobby);
    }

    #endregion

    #region Change Scene

    public void SetScene(Define.Scene next)
    {
        if (isChangingScene)
            return;

        isChangingScene = true;
        nextScene = next;

        UIManager.instance.StartLoading();
    }

    public void OnFadeOut()
    {
        switch (nextScene)
        {
            case Define.Scene.Title:
                break;

            case Define.Scene.Lobby:
                ServerManager.instance.JoinLobby();
                break;

            case Define.Scene.InGame:
                break;

            case Define.Scene.Defeat:
                break;

            case Define.Scene.Victory:
                break;
        }

        ServerManager.instance.OnFadeOuting(nowScene);
        SceneManager.instance.LoadScene(nextScene);
    }

    public void OnLoadingScene()
    {
        nowScene = nextScene;

        switch (nowScene)
        {
            case Define.Scene.Title:
                break;

            case Define.Scene.Lobby:
                if (SceneManager.instance.GetSceneName().Equals("InGame"))
                {
                    ClearBuildList();
                    ClearUnitList();
                    rtsController.buildList.Clear();
                    nowInGame = false;
                }
                break;

            case Define.Scene.InGame:
                rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
                break;

            case Define.Scene.Defeat:
                break;

            case Define.Scene.Victory:
                break;
        }

        UIManager.instance.ShowUI(nowScene);

        UIManager.instance.OffLoading();
    }

    public void OnFadeIn()
    {
        switch (nowScene)
        {
            case Define.Scene.Title:
                break;

            case Define.Scene.Lobby:
                break;

            case Define.Scene.InGame:
                break;

            case Define.Scene.Defeat:
                break;

            case Define.Scene.Victory:
                break;
        }

        isChangingScene = false;
    }

    public void OnCompletlyLoading()
    {
        switch (nowScene)
        {
            case Define.Scene.Title:
            case Define.Scene.Lobby:
                ServerManager.instance.DisconnectServer();
                break;

            case Define.Scene.InGame:
                break;

            case Define.Scene.Defeat:
                break;

            case Define.Scene.Victory:
                break;
        }
    }

    #endregion

    #region RTS Controller

    public void ClearBuildList()
    {
        for (int i = 0; i < rtsController.buildList.Count; i++)
        {
            Destroy(rtsController.buildList[i].gameObject);
        }
        rtsController.buildList.Clear();
    }
    public void ClearUnitList()
    {
        for (int i = 0; i < rtsController.unitList.Count; i++)
        {
            Destroy(rtsController.unitList[i].gameObject);
        }
        rtsController.unitList.Clear();
    }


    #endregion

}
