using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using PN = Photon.Pun.PhotonNetwork;

public class ServerManager : MonoBehaviourPunCallbacks
{
    #region singleton

    public static ServerManager instance;

    public void Init()
    {
        PV = GetComponent<PhotonView>();
    }

    #endregion

    #region elements

    public PhotonView PV;


    #endregion

    #region Scene Functions

    public void OnFadeOuting(Define.Scene nextScene)
    {
        switch (nextScene)
        {
            case Define.Scene.Title:
                break;

            case Define.Scene.Lobby:
                    PN.LeaveRoom();
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


    #region Player Info Function

    public bool isNullableNickName()
    {
        return (PN.NickName == "");
    }

    #endregion

    #region DisConnect

    public void DisconnectServer()
    {
        if (PN.InLobby)
            PN.LeaveLobby();

        if (PN.InRoom)
            PN.LeaveRoom();

        if (PN.IsConnected)
            PN.Disconnect();
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
                Managers.instance._game.GameQuit();
                break;

            case DisconnectCause.DisconnectByOperationLimit:
                Debug.Log("DisconnectByOperationLimit");
                break;

            default:
                Debug.Log("what?");
                break;
        }
    }


    #endregion

    #region Functons

    public void ServerConnect()
    {
        if (!PN.IsConnected)
        {
            PN.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Managers.instance._game.OnConnectTedServer();
    }

    public void JoinLobby()
    {
        if (PN.IsConnected)
        {
            PN.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Managers.instance._game.OnJoinedLobby();
    }

    public override void OnCreatedRoom()
    {
        instance.tag = "Team_1";
    }

    public override void OnJoinedRoom()
    {
        Invoke("NoticeJoinedRoom", 1f);

        if (Managers.instance._game.DevelopMode)
        {
            UIManager.instance.SetStartTextInLobby("매칭 시작");
            UIManager.instance.Notice("매칭이 성공했습니다.", 1);
            /*nowMatching = false;
            SetScene("inGame");*/
            return;
        }

        if (PN.CurrentRoom.MaxPlayers == PN.CurrentRoom.PlayerCount)
        {
            instance.tag = "Team_2";
            UIManager.instance.SetStartTextInLobby("매칭 시작");
            UIManager.instance.Notice("매칭이 성공했습니다.", 1);
            /*nowMatching = false;
            SetScene("inGame");*/
        }
    }

    private void NoticeJoinedRoom()
    {
        UIManager.instance.Notice("방에 입장했습니다.", 1);
        //nowInRoom = true;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (Managers.instance._game.DevelopMode)
        {
            UIManager.instance.SetStartTextInLobby("매칭 시작");
            UIManager.instance.Notice("매칭이 성공했습니다.", 1);
            //nowMatching = false;
            //SetScene("inGame");
            return;
        }

        if (PN.CurrentRoom.MaxPlayers == PN.CurrentRoom.PlayerCount)
        {
            UIManager.instance.SetStartTextInLobby("매칭 시작");
            UIManager.instance.Notice("매칭이 성공했습니다.", 1);
            //nowMatching = false;
            //SetScene("inGame");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (Managers.instance._game.DevelopMode)
            PN.CreateRoom($"{Random.Range(0, 100)}", new Photon.Realtime.RoomOptions { MaxPlayers = 1 },
                null, null);

        else
            PN.CreateRoom($"{Random.Range(0, 100)}", new Photon.Realtime.RoomOptions { MaxPlayers = 2 },
                null, null);
    }

    #endregion


    #region button Function
    public void OnClickStartInLobby()
    {
        /*PN.JoinRandomOrCreateRoom(
            null, 2, Photon.Realtime.MatchmakingMode.FillRoom,
            null, null, $"{Random.Range(0, 100)}", new Photon.Realtime.RoomOptions { MaxPlayers = 2 });*/

        PN.JoinRandomRoom();
    }
    public void LeaveRoom()
    {
        //nowInRoom = false;
        UIManager.instance.Notice("방에서 나갔습니다.", 1);
        PN.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Invoke("EndExitingMatching", 2f);
        return;
    }

    private void EndExitingMatching()
    {
        UIManager.instance.Notice("매칭을 취소했습니다.", 1);
        UIManager.instance.SetStartTextInLobby("매칭 시작");
        //nowCloseMatching = false;
        //nowMatching = false;
    }

    public void OnClickExit()
    {
        if (PN.IsConnected)
            PN.Disconnect();
    }

    public void SetNickName(string name)
    {
        PN.NickName = name;
    }
    #endregion
}
