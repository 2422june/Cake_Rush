using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase
{

    #region elements

    public Define.Scene nowScene, nextScene;
    private bool isChangingScene;

    public float playerLevel;
    public int[] cost;
    public bool isSpawnable;
    private bool nowInGame = false;
    public bool nowMatching, nowCloseMatching, nowInRoom;

    public RTSController rtsController;

    #endregion

    #region Setting

    public override void Init()
    {

    }

    public void OnConnectTedServer()
    {
        if (Managers.instance._scene.IsSameSceneName(Define.Scene.Loading))
        {
            nowInGame = false;

            SetScene(Define.Scene.Title);
        }
        else if (Managers.instance._scene.IsSameSceneName(Define.Scene.InGame))
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
        Managers.instance._ui.OffLoading();
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
            if(Managers.instance._scene.GetSceneName().Equals("InGame"))
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

        Managers.instance._ui.StartLoading();
    }

    public void OnFadeOut()
    {
        switch (nextScene)
        {
            case Define.Scene.Title:
                break;

            case Define.Scene.Lobby:
                Managers.instance._server.JoinLobby();
                break;

            case Define.Scene.InGame:
                break;

            case Define.Scene.Defeat:
                break;

            case Define.Scene.Victory:
                break;
        }

        Managers.instance._server.OnFadeOuting(nowScene);
        Managers.instance._scene.LoadScene(nextScene);
    }

    public void OnLoadingScene()
    {
        nowScene = nextScene;

        switch (nowScene)
        {
            case Define.Scene.Title:
                break;

            case Define.Scene.Lobby:
                if (Managers.instance._scene.GetSceneName().Equals("InGame"))
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

        Managers.instance._ui.ShowUI(nowScene);

        Managers.instance._ui.OffLoading();
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
                Managers.instance._server.DisconnectServer();
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

    /*#region find Function
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
    #endregion*/
