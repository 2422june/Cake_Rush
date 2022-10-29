using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
using PN = Photon.Pun.PhotonNetwork;

public class GameManager : MonoBehaviourPunCallbacks
=======
public class GameManager : MonoBehaviour
>>>>>>> ReMake
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

<<<<<<< HEAD
    #region element

    public PhotonView PV;

    public Define.Scene nowScene;
=======
    #region elements
>>>>>>> ReMake

    public Define.Scene nowScene, nextScene;
    private bool isChangingScene;

    public float playerLevel;
    public int[] cost;
    public bool isSpawnable;
    private bool nowInGame;
    public bool nowMatching, nowCloseMatching, nowInRoom;
<<<<<<< HEAD
    public UiManager UIManager;

    [SerializeField]
    private GameObject UIManagerOBJ;
=======
>>>>>>> ReMake
    public RTSController rtsController;

    [Space(10)]
    [Header("ONLY DEVELOPER")]
    public bool DevelopMode;

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
<<<<<<< HEAD
    #endregion

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        PV = GetComponent<PhotonView>();
    //        GameProgress.instance = GetComponent<GameProgress>();
    //        DontDestroyOnLoad(instance);
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    /*if (instance == null)
    //    {
    //        GameObject GM = Resources.Load<GameObject>("Prefabs/Systems/GameManager");
    //        instance = GM.GetComponent<GameManager>();
    //        Destroy(this.gameObject);
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        PV = GetComponent<PhotonView>();
    //        GameProgress.instance = GetComponent<GameProgress>();
    //        DontDestroyOnLoad(instance);
    //    }*/


    //    if (SceneManager.GetActiveScene().name.Contains("Title"))
    //    {
    //        nowInGame = false;

    //        UIManagerOBJ = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UIManager"), transform);
    //        UIManager = UIManagerOBJ.GetComponent<UiManager>();

    //        UIManager.Init();

    //        SetScene("title");
    //    }
    //    else if (SceneManager.GetActiveScene().name.Contains("InGame"))
    //    {
    //        rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
    //    }
    //}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PV = GetComponent<PhotonView>();
            GameProgress.instance = GetComponent<GameProgress>();
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name.Contains("Title"))
        {
            nowInGame = false;

            UIManagerOBJ = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UIManager"), transform);
            UIManager = UIManagerOBJ.GetComponent<UiManager>();

            UIManager.Init();

            SetScene("title");
        }
        else if (SceneManager.GetActiveScene().name.Contains("InGame"))
        {
            rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
        }
    }


    #region Scene or Server

    public override void OnCreatedRoom()
    {
        instance.tag = "Team_1";
    }

    public override void OnConnectedToMaster()
    {
        UIManager.OnConnectedToMaster();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        switch (cause)
        {
            case DisconnectCause.None:
                Debug.Log("None");
                break;

            case DisconnectCause.DisconnectByDisconnectMessage:
                Debug.Log("DisconnectByDisconnectMessage");
                break;

            case DisconnectCause.OperationNotAllowedInCurrentState:
                Debug.Log("ExceptionOnConnect");
                break;

            case DisconnectCause.DisconnectByClientLogic:
                Debug.Log("플레이어가 직접 끈 경우");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
                break;

            case DisconnectCause.DisconnectByOperationLimit:
                Debug.Log("DisconnectByOperationLimit");
                break;

            default:
                Debug.Log("what?");
                break;
        }
    }

    public override void OnJoinedLobby()
    {
        SetScene("lobby");
    }

    public override void OnJoinedRoom()
    {
        Invoke("JoinedRoom", 1f);

        if (DevelopMode)
        {
            UIManager.SetStartTextInLobby("매칭 시작");
            UIManager.NoticeInLobby("매칭이 성공했습니다.", 1);
            nowMatching = false;
            SetScene("inGame");
            return;
        }

        if (PN.CurrentRoom.MaxPlayers == PN.CurrentRoom.PlayerCount)
        {
            instance.tag = "Team_2";
            UIManager.SetStartTextInLobby("매칭 시작");
            UIManager.NoticeInLobby("매칭이 성공했습니다.", 1);
            nowMatching = false;
            SetScene("inGame");
        }
    }

    private void JoinedRoom()
    {
        UIManager.NoticeInLobby("방에 입장했습니다.", 1);
        nowInRoom = true;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(DevelopMode)
        {
            UIManager.SetStartTextInLobby("매칭 시작");
            UIManager.NoticeInLobby("매칭이 성공했습니다.", 1);
            nowMatching = false;
            SetScene("inGame");
            return;
        }

        if (PN.CurrentRoom.MaxPlayers == PN.CurrentRoom.PlayerCount)
        {
            UIManager.SetStartTextInLobby("매칭 시작");
            UIManager.NoticeInLobby("매칭이 성공했습니다.", 1);
            nowMatching = false;
            SetScene("inGame");
        }
    }
    #endregion


    #region button
    public  void OnClickStartInTitle()
    {
        if (PN.IsConnected)
        {
            PN.JoinLobby();
        }
    }
    public void OnClickStartInLobby()
    {
        /*PN.JoinRandomOrCreateRoom(
            null, 2, Photon.Realtime.MatchmakingMode.FillRoom,
            null, null, $"{Random.Range(0, 100)}", new Photon.Realtime.RoomOptions { MaxPlayers = 2 });*/

        PN.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if(DevelopMode)
            PN.CreateRoom($"{Random.Range(0, 100)}", new Photon.Realtime.RoomOptions { MaxPlayers = 1 },
                null, null);

        else
            PN.CreateRoom($"{Random.Range(0, 100)}", new Photon.Realtime.RoomOptions { MaxPlayers = 2 },
                null, null);
    }
    public void LeaveRoom()
    {
        nowInRoom = false;
        UIManager.NoticeInLobby("방에서 나갔습니다.", 1);
        PN.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Invoke("EndExitingMatching", 2f);
        return;
    }

    private void EndExitingMatching()
    {
        UIManager.NoticeInLobby("매칭을 취소했습니다.", 1);
        UIManager.SetStartTextInLobby("매칭 시작");
        nowCloseMatching = false;
        nowMatching = false;
    }

    public void OnClickExit()
    {
        if (PN.IsConnected)
            PN.Disconnect();
    }
    public void OnClickInfo()
    {
    }
    public void SetNickName(string name)
    {
        PN.NickName = name;
    }
    #endregion


    public virtual void SetScene(string targetScene)
    {
        if (targetScene.Equals("title"))
        {
            if (!PN.IsConnected)
            {
                PN.ConnectUsingSettings();
            }
            nowScene = Define.Scene.title;
        }
        if (targetScene.Equals("lobby"))
        {
            if(SceneManager.GetActiveScene().name == "InGame")
            {
                for (int i = 0; i < rtsController.unitList.Count; i++)
                {
                    Destroy(rtsController.unitList[i].gameObject);
                }
                rtsController.unitList.Clear();
                for (int i = 0; i < rtsController.buildList.Count; i++)
                {
                    Destroy(rtsController.buildList[i].gameObject);
                }
                rtsController.buildList.Clear();
                nowInGame = false;
                PN.LeaveRoom();
                Destroy(this.gameObject);
                SceneManager.LoadScene("TitleLobby");
            }
            nowScene = Define.Scene.lobby;
        }
        if (targetScene.Equals("inGame"))
        {
            nowScene = Define.Scene.inGame;
            Invoke("EnterInGame", 2f);
        }
        if (targetScene.Equals("victory"))
        {
            nowScene = Define.Scene.victory;
        }
        if (targetScene.Equals("defeat"))
        {
            nowScene = Define.Scene.defeat;
        }

        UIManager.ShowUI(nowScene);
    }

    private void EnterInGame()
    {
        SceneManager.LoadScene("InGame");
        nowInGame = true;
        nowInRoom = false;
        StartCoroutine(InGameSeting());
    }

    private IEnumerator InGameSeting()
    {
        while(true)
        {
            if(SceneManager.GetActiveScene().name.Equals("InGame"))
            {
                rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
                GameProgress.instance.NowArriveInGame();
                break;
            }

            yield return null;
        }
    }

    public bool isNullableNickName()
    {
        return (PN.NickName == "");
    }
}
=======
    #endregion*/
>>>>>>> ReMake
