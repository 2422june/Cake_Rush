using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PN = Photon.Pun.PhotonNetwork;

//게임의 승패 관리, 인벤트 생성 등 인게임 진행 담당
public class GameProgress : MonoBehaviourPunCallbacks
{
    public static GameProgress instance;
    public bool inGameStart;
    public bool isGameOver;

    private float inGameTime = 0;

    public LayerMask groundLayer;
    public LayerMask selectableLayer;
    public RTSController rtsController;
    private GameObject camera;
    private PhotonView PV;

    public bool team1Ready;
    public bool team2Ready;

    private UiManager UIManager;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        selectableLayer = 1 << LayerMask.NameToLayer("Selectable");
    }

    public void NowArriveInGame()
    {
        inGameStart = false;
        team1Ready = false;
        team2Ready = false;

        tag = GameManager.instance.tag;

        rtsController = GameManager.instance.rtsController;
        UIManager = GameManager.instance.UIManager;
        camera = GameObject.Find("MainCamera");

        SetingMap();
        UIManager.ShowInGamePanel(UiManager.inGameUIs.main);


        if (tag == "Team_1")
            PV.RPC("Team1Ready", RpcTarget.All);
        else
            PV.RPC("Team2Ready", RpcTarget.All);

        StartCoroutine(InGameCountDown());

        return;
    }

    WaitForSeconds one = new WaitForSeconds(1);

    [PunRPC]
    private IEnumerator InGameCountDown()
    {
        while (true)
        {
            yield return null;
            if (team1Ready && team2Ready)
            {
                UIManager.NoticeInLoby("5", 0.8f);
                yield return one;
                UIManager.NoticeInLoby("4", 0.8f);
                yield return one;
                UIManager.NoticeInLoby("3", 0.8f);
                yield return one;
                UIManager.NoticeInLoby("2", 0.8f);
                yield return one;
                UIManager.NoticeInLoby("1", 0.8f);
                yield return one;
                UIManager.NoticeInLoby("0", 0.8f);
                yield return one;
                UIManager.NoticeInLoby("GameStart!!", 1f);
                inGameStart = true;
                break;
            }
        }
        yield return null;
        StartCoroutine(InGameProcess());
    }

    private IEnumerator InGameProcess()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.OnClickOption();
            }
            yield return null;
        }
    }

    [PunRPC]
    private void Team1Ready()
    {
        team1Ready = true;
    }

    [PunRPC]
    private void Team2Ready()
    {
        team2Ready = true;
    }

    private void SetingMap()
    {
        if (tag == "Team_1")
        {
            PN.Instantiate("Prefabs/Units/Player", Vector3.zero, Quaternion.identity);
            PN.Instantiate("Prefabs/Houses/A_Nexus", (Vector3.right * 24.5f) + (Vector3.forward * 24.5f), Quaternion.identity);
            camera.transform.localPosition = ((Vector3.right * 4) + (Vector3.up * 5) + (Vector3.forward)) * 10;
        }
        else
        {
            PN.Instantiate("Prefabs/Units/Player", ((Vector3.right + Vector3.forward) * 300), Quaternion.identity);
            PN.Instantiate("Prefabs/Houses/B_Nexus", (Vector3.right * 274.6f) + (Vector3.forward * 274.6f), Quaternion.identity);
            camera.transform.localPosition = ((Vector3.right * 26) + (Vector3.up * 5) + (Vector3.forward * 27)) * 10;
        }
    }

    public void FinalGame()
    {

    }

    public void GameOver()
    {
        
    }
}
