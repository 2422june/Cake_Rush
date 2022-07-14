using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class MonsterManager : MonoBehaviour
{

    void Start()
    {
        PhotonNetwork.Instantiate("Prefabs/Monsters/ThiefRat", new Vector3(5,0,5), Quaternion.identity);
    }

    
    void Update()
    {
        
    }
}
