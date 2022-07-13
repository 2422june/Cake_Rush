using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour //GameManager
{

    #region elements

    private WaitForSeconds one = new WaitForSeconds(1);

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
    private GameObject loadingPanel;
    private GameObject noticePanel;
    private GameObject timePanel;
    private GameObject resourcePanel;

    //inGame
    private GameObject playerPanel;
    private GameObject statPanel;
    private GameObject buildPanel;
    private GameObject defaultPanel;
    private GameObject playerUnitSlot;
    private GameObject downUnitSlot;
    private Button statButton;
    private Button buildButton;

    private Slider loadingBar;
    private bool isExitLoading;

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

    private bool callNotice;
    private float noticeTime;
    private float noticeTimer;

    private Button skillCokeShot;
    private Button skillCakeRush;
    private Button skillShotingStar;
    private Button skillLightning;

    private TMP_Text timeTxt;

    private TMP_Text chocolateTxt;
    private TMP_Text sugarTxt;
    private TMP_Text doughTxt;

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

        loadingPanel  = FindElement("LoadingPanel");
        loadingBar    = SetAny<Slider>(loadingPanel, "LoadingSlider");
        //StartCoroutine(Loading());
        StartCoroutine(LoadingAndNotice());

        titlePanel       = FindElement("TitlePanel");
        lobbyPanel       = FindElement("LobbyPanel");
        noticePanel      = FindElement("NoticePanel");
        lobbyOptionPanel = SetGameObj(lobbyPanel, "OptionMenus");
        noticeText       = SetText(noticePanel, "Text");
        timePanel        = FindElement("TimePanel");
        defaultPanel     = FindElement("DefaultPanel");

        playerPanel      = FindElement("PlayerPanel");
        playerUnitSlot   = FindElement("UnitListPanel");
        downUnitSlot     = FindElement("UnitListPanelDown");

        startInTitle     = SetAny<Button>(titlePanel, "StartButton");
        exitInTitle      = SetAny<Button>(titlePanel, "ExitButton");

        startInLobby     = SetAny<Button>(lobbyPanel, "StartButton");
        startTextInLobby = SetText(startInLobby.gameObject, "Text");
        optionInLobby    = SetAny<Button>(lobbyPanel, "OptionButton");
        matchingPanel    = SetGameObj(lobbyPanel, "MatchingPanel");
        nameInputInLoby  = SetAny<TMP_InputField>(matchingPanel, "NameInput");
        exitInLobby      = SetAny<Button>(lobbyOptionPanel, "ExitButton");
        infoInLobby      = SetAny<Button>(lobbyOptionPanel, "InfoButton");


        statPanel        = SetGameObj(playerPanel, "AbilltyPanel");
        buildPanel       = SetGameObj(playerPanel, "BuildingPanel");
        buildButton      = SetAny<Button>(playerPanel, "BuildButton");
        statButton       = SetAny<Button>(playerPanel, "StatButton");
        timeTxt          = SetText(timePanel, "time");

        resourcePanel    = FindElement("ResourcePanel");
        chocolateTxt     = SetText(resourcePanel, "Chocolate");
        sugarTxt         = SetText(resourcePanel, "Sugar");
        doughTxt         = SetText(resourcePanel, "Dough");
        //skillCokeShot    = SetAny<Button>(playerPanel, "CokeShot");
        //skillCakeRush    = SetAny<Button>(playerPanel, "CakeRush");
        //skillShotingStar = SetAny<Button>(playerPanel, "ShotingStar");
        //skillLightning   = SetAny<Button>(playerPanel, "Lightning");

        loadingPanel.transform.SetAsLastSibling();

        startInTitle.onClick.AddListener(OnClickStartInTitle);
        exitInTitle.onClick.AddListener(OnClickExit);

        startInLobby.onClick.AddListener(OnClickStartInLobby);
        exitInLobby.onClick.AddListener(OnClickExit);
        optionInLobby.onClick.AddListener(OnClickOption);
        infoInLobby.onClick.AddListener(OnClickInfo);
        nameInputInLoby.onEndEdit.AddListener(OnClickNameSubmit);

        statButton.onClick.AddListener(OnClickStat);
        buildButton.onClick.AddListener(OnClickBuild);

        //skillCakeRush.onClick.AddListener(OnClickCakeRush);
        //skillShotingStar.onClick.AddListener(OnClickShotingStar);
        //skillCokeShot.onClick.AddListener(OnClickCokeShot);
        //skillLightning.onClick.AddListener(OnClickLightning);
    }

    public void ShowLoading()
    {
        loadingBar.value = 0;
        isExitLoading = false;
    }

    //private IEnumerator Loading()
    //{
    //    while(true)
    //    {
    //        if(loadingBar.value == 0)
    //        {
    //            loadingPanel.SetActive(true);

    //            while (loadingBar.value < 80)
    //            {
    //                loadingBar.value += 35 * Time.deltaTime;
    //                yield return null;
    //            }

    //            loadingBar.value = 80;
    //        }
    //        else if(isExitLoading && loadingBar.value >= 80)
    //        {
    //            isExitLoading = false;
    //            while (loadingBar.value < 100)
    //            {
    //                loadingBar.value += 40 * Time.deltaTime;
    //                yield return null;
    //            }

    //            loadingBar.value = 100;
    //            loadingPanel.SetActive(false);
    //        }

    //        yield return null;
    //    }
    //}

    private IEnumerator LoadingAndNotice()
    {
        while (true)
        {
            if(callNotice)
            {
                callNotice = false;
                noticePanel.SetActive(true);
                while(noticeTime >= noticeTimer)
                {
                    if (callNotice)
                        break;
                    noticeTimer += Time.deltaTime;
                    yield return null;
                }
                noticePanel.SetActive(false);
            }

            if (loadingBar.value == 0)
            {
                loadingPanel.SetActive(true);

                while (loadingBar.value < 80)
                {
                    loadingBar.value += 35 * Time.deltaTime;
                    yield return null;
                }

                loadingBar.value = 80;
            }
            else if (isExitLoading && loadingBar.value >= 80)
            {
                isExitLoading = false;
                while (loadingBar.value < 100)
                {
                    loadingBar.value += 40 * Time.deltaTime;
                    yield return null;
                }

                loadingBar.value = 100;
                loadingPanel.SetActive(false);
            }

            yield return null;
        }
    }

    public void ExitLoading()
    {
        isExitLoading = true;
        //loadingBar.value = 70;
    }

    #region skill
    private void OnClickShotingStar()
    {

    }

    private void OnClickLightning()
    {

    }

    private void OnClickCokeShot()
    {

    }

    private void OnClickCakeRush()
    {

    }
    #endregion


    #region server
    public void OnConnectedToMaster()
    {
        ExitLoading();
    }

    #endregion

    #region button
    public void OnClickStartInTitle()
    {
        GameManager.instance.OnClickStartInTitle();
    }
    public void OnClickStartInLobby()
    {
        if(GameManager.instance.nowCloseMatching)
        {
            NoticeInLoby("매칭을 취소하는 중 입니다.", 1);
        }
        else
        {
            if (GameManager.instance.nowMatching)
            {
                if(GameManager.instance.nowInRoom)
                {
                    GameManager.instance.nowCloseMatching = true;
                    NoticeInLoby("매칭을 취소합니다.", 1);
                    SetStartTextInLoby("매칭 취소중");
                    GameManager.instance.LeaveRoom();
                }
                else
                {
                    NoticeInLoby("방에 입장하는 중 입니다.", 1);
                }
            }
            else
            {
                if (GameManager.instance.isNullableNickName())
                {
                    NoticeInLoby("닉네임을 입력해 주세요.", 1);
                }
                else if(!GameManager.instance.nowInRoom)
                {
                    GameManager.instance.OnClickStartInLobby();
                    GameManager.instance.nowMatching = true;
                    NoticeInLoby("매칭을 시작했습니다.", 1);
                    SetStartTextInLoby("매칭 취소");
                }
                else
                {
                    NoticeInLoby("방에서 나가는 중 입니다.", 1);
                }
            }
        }
    }

    public void SetStartTextInLoby(string text)
    {
        startTextInLobby.text = text;
    }

    public void NoticeInLoby(string text, float time)
    {
        noticePanel.transform.localPosition = Vector3.zero;
        noticeText.text = text;
        noticeTime = time;
        noticeTimer = 0;
        callNotice = true;
    }

    //private void UnNoticeInLoby()
    //{
    //    noticePanel.SetActive(false);
    //    noticeText.text = "";
    //}

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
    public void OnClickNameSubmit(string text)
    {
        GameManager.instance.SetNickName(text);
        NoticeInLoby($"닉네임을 '{text}'로 지정했습니다.", 1);
    }
    #endregion

    #region inGame
    public enum inGameUIs
    {
        main, player, other
    }

    public void ChangeCost(int cho, int sug, int dou)
    {
        chocolateTxt.text = $"{cho}";
        sugarTxt.text     = $"{sug}";
        doughTxt.text     = $"{dou}";
    }

    private void OnClickBuild()
    {
        buildPanel.SetActive(!buildPanel.active);
    }

    private void OnClickStat()
    {
        statPanel.SetActive(!statPanel.active);
    }

    public void ShowInGameStaticPanel()
    {
        timePanel.SetActive(true);
        timeTxt.text = "00:00";
        resourcePanel.SetActive(true);
    }

    public int FlowTime(int time)
    {
        if ((time / 60) < 10)
            timeTxt.text = $"0{time / 60}:";
        else
            timeTxt.text = $"{time / 60}:";

        if ((time % 60) < 10)
            timeTxt.text += $"0{time % 60}";
        else
            timeTxt.text += $"{time % 60}";

        return (time + 1);
    }

    public void ShowInGameDynamicPanel(inGameUIs target)
    {
        playerPanel.SetActive(false);
        playerUnitSlot.SetActive(false);
        downUnitSlot.SetActive(false);
        defaultPanel.SetActive(false);

        switch (target)
        {
            case inGameUIs.main:
                downUnitSlot.SetActive(true);
                break;

            case inGameUIs.player:
                playerPanel.SetActive(true);
                playerUnitSlot.SetActive(true);
                break;

            case inGameUIs.other:
                defaultPanel.SetActive(true);
                break;
        }
    }

    #endregion
    public void ShowUI(Scene nowScene)
    {
        titlePanel.SetActive(nowScene == Scene.title);
        lobbyPanel.SetActive(nowScene == Scene.lobby);

    }
}