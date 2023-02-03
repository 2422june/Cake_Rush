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

    public LayerMask groundLayer;
    public LayerMask selectableLayer;
    public RTSController rtsController;
    private GameObject _camera;
    private PhotonView PV;

    public bool team1Ready;
    public bool team2Ready;
    private bool startProcess;

    private int timeI;
    private float timeF;

    public void Init()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        selectableLayer = 1 << LayerMask.NameToLayer("Selectable");
    }

    public void NowArriveInGame()
    {
        PV = GetComponent<PhotonView>();

        inGameStart = false;
        team1Ready = false;
        team2Ready = false;

        startProcess = false;

        tag = Managers.instance._game.tag;

        rtsController = Managers.instance._game.rtsController;
        _camera = GameObject.Find("MainCamera");

        SetingMap();
        Managers.instance._ui.ShowInGameStaticPanel();
        Managers.instance._ui.ShowInGameDynamicPanel(UIManager.inGameUIs.main);


        if (tag == "Team_1")
        {
            PV.RPC("Team1Ready", RpcTarget.All);
            _camera.GetComponent<CameraController>().SetUpsideDown(false);
        }
        else
        {
            PV.RPC("Team2Ready", RpcTarget.All);
            _camera.GetComponent<CameraController>().SetUpsideDown(true);
        }

        PV.RPC("CountDownStarter", RpcTarget.All);

        return;
    }

    WaitForSeconds one = new WaitForSeconds(1);

    [PunRPC]
    private void CountDownStarter()
    {
        StartCoroutine(InGameCountDown());
    }

    private IEnumerator InGameCountDown()
    {
        while (true)
        {
            yield return null;
            if (team1Ready && team2Ready)
            {
                Managers.instance._ui.Notice("5", 0.8f);
                yield return one;
                Managers.instance._ui.Notice("4", 0.8f);
                yield return one;
                Managers.instance._ui.Notice("3", 0.8f);
                yield return one;
                Managers.instance._ui.Notice("2", 0.8f);
                yield return one;
                Managers.instance._ui.Notice("1", 0.8f);
                yield return one;
                Managers.instance._ui.Notice("0", 0.8f);
                yield return one;
                Managers.instance._ui.Notice("GameStart!!", 1f);
                inGameStart = true;
                break;
            }
        }
        yield return null;

        PV.RPC("ProcessStarter", RpcTarget.All);
    }

    [PunRPC]
    private void ProcessStarter()
    {
        rtsController.ChangeCost(0, 0, 0);
        timeI = 0;
        timeF = 0;
        startProcess = true;
    }


    private void Update()
    {
        if (startProcess)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Managers.instance._ui.OnClickOption();
            }

            if(timeF - timeI >= 1)
            {
                timeI = Managers.instance._ui.FlowTime(timeI);
            }

            timeF += Time.deltaTime;

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
        GameObject go;
        //if (false)//Managers.instance._game.DevelopMode)
        //{
        //    go = PN.Instantiate("Prefabs/Units/Player", (Vector3.right + Vector3.forward) * 9, Quaternion.identity);
        //    rtsController.unitList.Add(go.GetComponent<PlayerController>());
        //    go = PN.Instantiate("Prefabs/Houses/A_Nexus", (Vector3.right * 24.5f) + (Vector3.forward * 24.5f), Quaternion.identity);
        //    rtsController.buildList.Add(go.GetComponent<NexusController>());
        //    go = PN.Instantiate("Prefabs/Houses/B_Nexus", (Vector3.right * 274.6f) + (Vector3.forward * 274.6f), Quaternion.identity);
        //    rtsController.buildList.Add(go.GetComponent<NexusController>());

        //    go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 60) + (Vector3.forward * 281.8f), Quaternion.Euler(Vector3.right * -90));
        //    rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
        //    go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 165) + (Vector3.forward * 185), Quaternion.Euler(Vector3.right * -90));
        //    rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
        //    go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 282) + (Vector3.forward * 59.5f), Quaternion.Euler(Vector3.right * -90));
        //    rtsController.buildList.Add(go.GetComponent<CokeTowerController>());

        //    go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 19) + (Vector3.forward * 238), Quaternion.Euler(Vector3.right * -90));
        //    rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
        //    go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 135) + (Vector3.forward * 115), Quaternion.Euler(Vector3.right * -90));
        //    rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
        //    go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 240) + (Vector3.forward * 18), Quaternion.Euler(Vector3.right * -90));
        //    rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
        //    return;
        //}
        
        if (tag == "Team_1")
        {
            go = PN.Instantiate("Prefabs/Units/Player", (Vector3.right + Vector3.forward) * 9, Quaternion.identity);
            rtsController.unitList.Add(go.GetComponent<PlayerController>());
            go = PN.Instantiate("Prefabs/Houses/A_Nexus", (Vector3.right * 24.5f) + (Vector3.forward * 24.5f), Quaternion.identity);
            rtsController.buildList.Add(go.GetComponent<NexusController>());

            go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 19) + (Vector3.forward * 238), Quaternion.Euler(Vector3.right * -90));
            rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
            go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 135) + (Vector3.forward * 115), Quaternion.Euler(Vector3.right * -90));
            rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
            go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 240) + (Vector3.forward * 18), Quaternion.Euler(Vector3.right * -90));
            rtsController.buildList.Add(go.GetComponent<CokeTowerController>());

            _camera.transform.localPosition = ((Vector3.right * 4) + (Vector3.up * 5) + (Vector3.forward)) * 10;
        }
        else
        {
            go = PN.Instantiate("Prefabs/Units/Player", (((Vector3.right + Vector3.forward) * 300) - (Vector3.right + Vector3.forward) * 9), Quaternion.identity);
            rtsController.unitList.Add(go.GetComponent<PlayerController>());
            go = PN.Instantiate("Prefabs/Houses/B_Nexus", (Vector3.right * 274.6f) + (Vector3.forward * 274.6f), Quaternion.identity);
            rtsController.buildList.Add(go.GetComponent<NexusController>());

            go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 60) + (Vector3.forward * 281.8f), Quaternion.Euler(Vector3.right * -90));
            rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
            go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 165) + (Vector3.forward * 185), Quaternion.Euler(Vector3.right * -90));
            rtsController.buildList.Add(go.GetComponent<CokeTowerController>());
            go = PN.Instantiate("Prefabs/Houses/CokeTower", (Vector3.right * 282) + (Vector3.forward * 59.5f), Quaternion.Euler(Vector3.right * -90));
            rtsController.buildList.Add(go.GetComponent<CokeTowerController>());

            _camera.transform.localPosition = ((Vector3.right * 26) + (Vector3.up * 5) + (Vector3.forward * 30)) * 10;
            _camera.transform.rotation = Quaternion.Euler(((Vector3.right * 7) + (Vector3.up * 18)) * 10);
        }

    }

    public void FinalGame()
    {

    }

    public void GameOver()
    {
        
    }
}
