using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManagerCreater : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        if(Managers.instance._game == null)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.Instantiate("Prefabs/Systems/GameManager", Vector3.zero, Quaternion.identity);
        Destroy(gameObject);
    }
}
