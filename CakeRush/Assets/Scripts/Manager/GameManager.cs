using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

using PN = Photon.Pun.PhotonNetwork;
public enum Scene
{
    noting, title, lobby, inGame, victory, defeat
};

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Singleton

    public static GameManager instance;

    #endregion

    #region element

    public PhotonView PV;

    public Scene nowScene;


    public float playerLevel;
    public RTSController rtsController;
    public int[] cost;
    public bool isSpawnable;
    private bool nowInGame;
    public bool nowMatching;

    private UiManager UIManager;

    [SerializeField]
    private GameObject UIManagerOBJ;

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PV = GetComponent<PhotonView>();
            DontDestroyOnLoad(instance);
        }


        if (SceneManager.GetActiveScene().name.Contains("Title"))
        {
            nowInGame = false;

            UIManagerOBJ = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UIManager"), transform);
            UIManager = UIManagerOBJ.GetComponent<UiManager>();

            UIManager.Init();

            SetScene("title");
        }
        else
        {
            //developer version
            rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
        }
    }


    #region Scene or Server

    public override void OnCreatedRoom()
    {
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
        SetScene("loby");
    }

    public override void OnJoinedRoom()
    {

        UIManager.NoticeInLoby(PN.CurrentRoom.Name);
        if (PN.CurrentRoom.MaxPlayers == PN.CurrentRoom.PlayerCount)
        {
            UIManager.SetStartTextInLoby("매칭 시작");
            UIManager.NoticeInLoby("매칭이 성공했습니다.");
            nowMatching = false;
            SetScene("inGame");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PN.CurrentRoom.MaxPlayers == PN.CurrentRoom.PlayerCount)
        {
            UIManager.SetStartTextInLoby("매칭 시작");
            UIManager.NoticeInLoby("매칭이 성공했습니다.");
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

        if(PN.CountOfRooms > 0)
        {
            Debug.Log("있다.");
            PN.JoinRandomRoom();
        }
        else
        {
            Debug.Log("없다.");
            PN.CreateRoom($"{Random.Range(0, 100)}", new Photon.Realtime.RoomOptions { MaxPlayers = 2 },
                null, null);
        }
    }
    public void LeaveRoom()
    {
        PN.LeaveRoom();
    }
    public void OnClickExit()
    {
        if (PN.IsConnected)
            PN.Disconnect();
    }
    public void OnClickInfo()
    {
    }
    public void OnClickMaker()
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
            nowScene = Scene.title;
        }
        if (targetScene.Equals("loby"))
        {
            nowScene = Scene.lobby;
        }
        if (targetScene.Equals("inGame"))
        {
            nowScene = Scene.inGame;
            SceneManager.LoadScene("InGame");
            nowInGame = true;
            StartCoroutine(InGameProcess());
        }
        if (targetScene.Equals("victory"))
        {
            nowScene = Scene.victory;
        }
        if (targetScene.Equals("defeat"))
        {
            nowScene = Scene.defeat;
        }

        UIManager.ShowUI(nowScene);
    }

    private IEnumerator InGameProcess()
    {
        while(true)
        {
            if(SceneManager.GetActiveScene().name == "InGame")
            {
                rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
                PN.Instantiate("Prefabs/Units/Player", Vector3.zero, Quaternion.identity);

                yield return null;
                break;
            }

            //rtsController = GameObject.Find("RTSManager").GetComponent<RTSController>();
            //PN.Instantiate("Prefabs/Units/Player", Vector3.zero, Quaternion.identity);

            //yield return null;
            //break;
            yield return null;
        }
    }

    public bool isNullableNickName()
    {
        return (PN.NickName == "");
    }
}
