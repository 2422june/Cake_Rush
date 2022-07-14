using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager> //GameManager
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
    private GameObject victoryPanel;
    private GameObject defeatPanel;
    private GameObject characterInfoPanel;

    //inGame
    private GameObject playerPanel;
    private GameObject statPanel;
    public GameObject buildPanel { get; set; }
    private GameObject defaultPanel;
    private GameObject playerUnitSlot;
    private GameObject downUnitSlot;
    private Button statButton;
    private Button buildButton;

    private Slider loadingBar;
    public bool isExitLoading;

    private Button startInTitle;
    private Button optionInTitle;
    private Button exitInTitle;

    private Button startInLobby;
    private TMP_Text startTextInLobby;
    private Button optionInLobby;
    private TMP_InputField nameInputInLobby;
    private Button exitInLobby;
    private TMP_Text noticeText;
    private Button infoInLobby;
     
    private bool callNotice;
    private float noticeTime;
    private float noticeTimer;

    private GameObject skillCokeShot;
    private GameObject skillCakeRush;
    private GameObject skillShotingStar;
    private GameObject skillLightning;

    public GameObject lightningLevelUp;
    public GameObject cokeShotLevelUp;
    public GameObject shootingStarLevelUp;
    public GameObject cakeRushLevelUp;

    public TMP_Text lightningCooltime;
    public TMP_Text shotingStarCooltime;
    public TMP_Text cakeRushCooltime;
    public TMP_Text cokeShotCooltime;

    public GameObject lightningActive;
    public GameObject cokeShotActive;
    public GameObject shootingStarActive;
    public GameObject cakeRushActive;
    
    private TMP_Text timeTxt;

    private TMP_Text chocolateTxt;
    private TMP_Text sugarTxt;
    private TMP_Text doughTxt;
    
    private TMP_Text playerDamage;
    private TMP_Text playerHealth;
    private TMP_Text playerSpeed;
    private TMP_Text playerDefense;
    private TMP_Text playerAttacSpeed;
    private TMP_Text playerAttackrange;
    private TMP_Text playerLevel;
    private Button nextInVictory;

    private Button nextInDefeat;

    public Slider hpBar;
    private Slider expBar;
    private TMP_Text exp;
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
    public void FindPlayer() => player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();

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

        characterInfoPanel = SetGameObj(playerPanel, "CharacterInfoPanel");

        victoryPanel     = FindElement("VictoryPanel");
        defeatPanel      = FindElement("DefeatPanel");

        startInTitle     = SetAny<Button>(titlePanel, "StartButton");
        exitInTitle      = SetAny<Button>(titlePanel, "ExitButton");

        startInLobby     = SetAny<Button>(lobbyPanel, "StartButton");
        startTextInLobby = SetText(startInLobby.gameObject, "Text");
        optionInLobby    = SetAny<Button>(lobbyPanel, "OptionButton");
        matchingPanel    = SetGameObj(lobbyPanel, "MatchingPanel");
        nameInputInLobby  = SetAny<TMP_InputField>(matchingPanel, "NameInput");
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

        nextInDefeat     = SetAny<Button>(defeatPanel, "Next");
        nextInVictory    = SetAny<Button>(victoryPanel, "Next");

        skillCokeShot    = SetGameObj(characterInfoPanel, "CokeShot");
        skillCakeRush    = SetGameObj(characterInfoPanel, "CakeRush");
        skillShotingStar = SetGameObj(characterInfoPanel, "ShotingStar");
        skillLightning   = SetGameObj(characterInfoPanel, "Lightning");

        cokeShotActive   = SetGameObj(skillCokeShot, "SkillActive");
        cakeRushActive   = SetGameObj(skillCakeRush, "SkillActive");
        shootingStarActive = SetGameObj(skillShotingStar, "SkillActive");
        lightningActive  = SetGameObj(skillLightning, "SkillActive");

        lightningCooltime = SetText(skillLightning, "Cooltime");
        cakeRushCooltime = SetText(skillCakeRush, "Cooltime");
        cokeShotCooltime = SetText(skillCokeShot, "Cooltime");
        shotingStarCooltime = SetText(skillShotingStar, "Cooltime");

        hpBar = SetAny<Slider>(characterInfoPanel, "HPBar");
        expBar = SetAny<Slider>(characterInfoPanel, "EXPBar");
                                                          
        playerHealth = SetText(hpBar.gameObject, "HP");
        exp = SetText(expBar.gameObject, "EXP");

        playerDamage = SetText(statPanel, "Damage");
        playerAttackrange = SetText(statPanel, "AttackRange");
        playerAttacSpeed = SetText(statPanel, "AttackSpeed");
        playerDefense = SetText(statPanel, "Defense");
        playerSpeed = SetText(statPanel, "MoveSpeed");
        playerLevel = SetText(statPanel, "Level");
        loadingPanel.transform.SetAsLastSibling();

        startInTitle.onClick.AddListener(OnClickStartInTitle);
        exitInTitle.onClick.AddListener(OnClickExit);

        startInLobby.onClick.AddListener(OnClickStartInLobby);
        exitInLobby.onClick.AddListener(OnClickExit);
        optionInLobby.onClick.AddListener(OnClickOption);
        infoInLobby.onClick.AddListener(OnClickInfo);
        nameInputInLobby.onEndEdit.AddListener(OnClickNameSubmit);

        statButton.onClick.AddListener(OnClickStat);
        buildButton.onClick.AddListener(OnClickBuild);

        nextInDefeat.onClick.AddListener(EndGame);
        nextInVictory.onClick.AddListener(EndGame);

        lightningLevelUp = SetGameObj(skillLightning, "LevelUp");
        cokeShotLevelUp = SetGameObj(skillCokeShot, "LevelUp");
        shootingStarLevelUp = SetGameObj(skillShotingStar, "LevelUp");
        cakeRushLevelUp = SetGameObj(skillCakeRush, "LevelUp");
        
        lightningLevelUp.SetActive(true);
        cokeShotLevelUp.SetActive(true);
        shootingStarLevelUp.SetActive(true);
        cakeRushLevelUp.SetActive(false);

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

    public void SetPlayerStat()
    {
        playerDamage.text = $"{player.damage}";
        playerAttackrange.text = $"{player.attackRange}";
        playerAttacSpeed.text = $"{player.attackSpeed}";
        playerSpeed.text = $"{player.moveSpeed}";
        playerDefense.text = $"{player.defensive}";
        playerLevel.text = $"{player.levelSystem.curLevel + 1}";
        SetPlayerHp();
    }

    public void SetPlayerHp()
    {
        playerHealth.text = $"{player.curHp} / {player.maxHp}";
        hpBar.value = player.curHp / player.maxHp;
    }
    public void SetPlayerExp()
    {
        if(player.levelSystem.curLevel < 10)
        {
            expBar.value = player.levelSystem.curExp / player.levelSystem.maxExp[player.levelSystem.curLevel];
            exp.text = $"{player.levelSystem.curExp} / {player.levelSystem.maxExp[player.levelSystem.curLevel]}";
        }
        else
        {
            expBar.value = 1;
            exp.text = "1000 / 1000";
            Debug.Log("Check");
        }

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

    #region Loading && Notice
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
    #endregion

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
            NoticeInLobby("매칭을 취소하는 중 입니다.", 1);
        }
        else
        {
            if (GameManager.instance.nowMatching)
            {
                if(GameManager.instance.nowInRoom)
                {
                    GameManager.instance.nowCloseMatching = true;
                    NoticeInLobby("매칭을 취소합니다.", 1);
                    SetStartTextInLobby("매칭 취소중");
                    GameManager.instance.LeaveRoom();
                }
                else
                {
                    NoticeInLobby("방에 입장하는 중 입니다.", 1);
                }
            }
            else
            {
                if (GameManager.instance.isNullableNickName())
                {
                    NoticeInLobby("닉네임을 입력해 주세요.", 1);
                }
                else if(!GameManager.instance.nowInRoom)
                {
                    GameManager.instance.OnClickStartInLobby();
                    GameManager.instance.nowMatching = true;
                    NoticeInLobby("매칭을 시작했습니다.", 1);
                    SetStartTextInLobby("매칭 취소");
                }
                else
                {
                    NoticeInLobby("방에서 나가는 중 입니다.", 1);
                }
            }
        }
    }

    public void SetStartTextInLobby(string text)
    {
        startTextInLobby.text = text;
    }

    public void NoticeInLobby(string text, float time)
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
        NoticeInLobby($"닉네임을 '{text}'로 지정했습니다.", 1);
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

    private void EndGame()
    {
        GameManager.instance.SetScene("lobby");
    }

    #endregion
    public void ShowUI(Define.Scene nowScene)
    {
        titlePanel.SetActive(nowScene == Define.Scene.title);
        lobbyPanel.SetActive(nowScene == Define.Scene.lobby);
        victoryPanel.SetActive(nowScene == Define.Scene.victory);
        defeatPanel.SetActive(nowScene == Define.Scene.defeat);
    }
}