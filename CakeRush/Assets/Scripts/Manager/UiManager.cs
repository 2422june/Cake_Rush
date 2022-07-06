using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//using Photon.Pun;
//using Photon.Realtime;
//using PN = Photon.Pun.PhotonNetwork;

public class UiManager : MonoBehaviour //GameManager
{

    #region elements

    private Image infoImage;
    private Image mapImage;

    private Text playTimeText;
    private Text unitSizeText;
    private Text eventText;
    private Text curCost;

    private PlayerController player;

    private Canvas sceneUICanvas;
    private GameObject canvasOBJ;
    private GameObject titlePanel;
    private GameObject lobbyPanel;
    private GameObject lobbyOptionPanel;
    private GameObject matchingPanel;
    private GameObject loaddingPanel;
    private GameObject noticePanel;

    private Button startInTitle;
    private Button optionInTitle;
    private Button exitInTitle;

    private Button startInLobby;
    private TMP_Text startTextInLobby;
    private Button optionInLobby;
    private TMP_InputField nameInputInLoby;
    private Button exitInLobby;
    private TMP_Text noticeText;
    private Button infoInLobby;

    private Button skillCokeShot;
    private Button skillCakeRush;
    private Button skillShotingStar;
    private Button skillLightning;

    #endregion

    protected GameObject FindElement(string path)
    {
        return Instantiate(Resources.Load<GameObject>($"Prefabs/UI/{path}"), canvasOBJ.transform);
    }

    protected virtual GameObject SetGameObj(GameObject parent, string name)
    {
        return parent.transform.Find(name).gameObject;
    }

    protected virtual TMP_Text SetText(GameObject parent, string name)
    {
        return parent.transform.Find(name).GetComponent<TMP_Text>();
    }

    protected virtual T SetAny<T>(GameObject parent, string name) where T : Selectable
    {
        return parent.transform.Find(name).GetComponent<T>();
    }

    public void Init()
    {
        DontDestroyOnLoad(this);

        sceneUICanvas  = GetComponentInChildren<Canvas>();
        canvasOBJ      = sceneUICanvas.gameObject;

        loaddingPanel  = FindElement("LoadingPanel");

        titlePanel       = FindElement("TitlePanel2");
        lobbyPanel       = FindElement("LobbyPanel3");
        noticePanel      = FindElement("NoticePanel");
        lobbyOptionPanel = SetGameObj(lobbyPanel, "OptionMenus");
        noticeText       = SetText(noticePanel, "Text");

        startInTitle  = SetAny<Button>(titlePanel, "StartButton");
        exitInTitle   = SetAny<Button>(titlePanel, "ExitButton");

        startInLobby     = SetAny<Button>(lobbyPanel, "StartButton");
        startTextInLobby = SetText(startInLobby.gameObject, "Text");
        optionInLobby    = SetAny<Button>(lobbyPanel, "OptionButton");
        matchingPanel    = SetGameObj(lobbyPanel, "MatchingPanel");
        nameInputInLoby  = SetAny<TMP_InputField>(matchingPanel, "NameInput");
        exitInLobby      = SetAny<Button>(lobbyOptionPanel, "ExitButton");
        infoInLobby      = SetAny<Button>(lobbyOptionPanel, "InfoButton");


        //skillCokeShot    = SetButton(commandPanel, "CokeShot");
        //skillCakeRush    = SetButton(commandPanel, "CakeRush");
        //skillShotingStar = SetButton(commandPanel, "ShotingStar");
        //skillLightning   = SetButton(commandPanel, "Lightning");



        loaddingPanel.transform.SetAsLastSibling();


        startInTitle.onClick.AddListener(OnClickStartInTitle);
        exitInTitle.onClick.AddListener(OnClickExit);

        startInLobby.onClick.AddListener(OnClickStartInLobby);
        exitInLobby.onClick.AddListener(OnClickExit);
        optionInLobby.onClick.AddListener(OnClickOption);
        infoInLobby.onClick.AddListener(OnClickInfo);
        nameInputInLoby.onEndEdit.AddListener(OnClickNameSubmit);

        //skillCakeRush.onClick.AddListener(OnClickCakeRush);
        //skillShotingStar.onClick.AddListener(OnClickShotingStar);
        //skillCokeShot.onClick.AddListener(OnClickCokeShot);
        //skillLightning.onClick.AddListener(OnClickLightning);
    }

    #region skill
    public void OnClickShotingStar()
    {

    }

    public void OnClickLightning()
    {

    }

    public void OnClickCokeShot()
    {

    }

    public void OnClickCakeRush()
    {

    }
    #endregion


    #region server
    public void OnConnectedToMaster()
    {
        loaddingPanel.SetActive(false);
    }

    #endregion

    #region button
    public void OnClickStartInTitle()
    {
        GameManager.instance.OnClickStartInTitle();
    }
    public void OnClickStartInLobby()
    {
        if(GameManager.instance.nowMatching)
        {
            GameManager.instance.LeaveRoom();
            GameManager.instance.nowMatching = false;
            NoticeInLoby("매칭을 취소했습니다.");
            SetStartTextInLoby("매칭 시작");
        }
        else
        {
            if (GameManager.instance.isNullableNickName())
            {
                NoticeInLoby("닉네임을 입력해 주세요.");
            }
            else
            {
                GameManager.instance.OnClickStartInLobby();
                GameManager.instance.nowMatching = true;
                NoticeInLoby("매칭을 시작했습니다.");
                SetStartTextInLoby("매칭 취소");
            }
        }
    }

    public void SetStartTextInLoby(string text)
    {
        startTextInLobby.text = text;
    }

    public void NoticeInLoby(string text)
    {
        noticePanel.transform.localPosition = Vector3.zero;
        noticeText.text = text;
        noticePanel.SetActive(true);
        Invoke("UnNoticeInLoby", 2f);
    }
    private void UnNoticeInLoby()
    {
        noticePanel.SetActive(false);
        noticeText.text = "";
    }

    public void OnClickExit()
    {
        GameManager.instance.OnClickExit();
    }
    public void OnClickOption()
    {
        lobbyOptionPanel.SetActive(!lobbyOptionPanel.active);
    }
    public void OnClickInfo()
    {
        GameManager.instance.OnClickInfo();
    }
    public void OnClickMaker()
    {
        GameManager.instance.OnClickMaker();
    }
    public void OnClickNameSubmit(string text)
    {
        GameManager.instance.SetNickName(text);
        NoticeInLoby($"닉네임을 '{text}'로 지정했습니다.");
    }
    #endregion

    public void ShowUI(Scene nowScene)
    {
        titlePanel.SetActive(nowScene == Scene.title);
        lobbyPanel.SetActive(nowScene == Scene.lobby);

    }


    // If select entity, on UI
    void SetUI()
    {

    }

    // Event notice method. use Fade in/out
    void Notice()
    {

    }
}