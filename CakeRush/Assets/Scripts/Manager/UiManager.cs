﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManagerBase
{
    public override void Init()
    {
        /*sceneUICanvas = GetComponentInChildren<Canvas>();
        canvasOBJ = sceneUICanvas.gameObject;

        loadingPanel = FindElement("LoadingPanel");
        loadingBar = SetAny<Slider>(loadingPanel, "LoadingSlider");

        StartCoroutine(Notice());
        StartCoroutine(LoadingCycle());
        StartFakeLoading();

        titlePanel = FindElement("TitlePanel");
        lobbyPanel = FindElement("LobbyPanel");
        noticePanel = FindElement("NoticePanel");
        lobbyOptionPanel = SetGameObj(lobbyPanel, "OptionMenus");
        noticeText = SetText(noticePanel, "Text");
        timePanel = FindElement("TimePanel");
        defaultPanel = FindElement("DefaultPanel");

        playerPanel = FindElement("PlayerPanel");
        playerUnitSlot = FindElement("UnitListPanel");
        downUnitSlot = FindElement("UnitListPanelDown");

        characterInfoPanel = SetGameObj(playerPanel, "CharacterInfoPanel");

        victoryPanel = FindElement("VictoryPanel");
        defeatPanel = FindElement("DefeatPanel");

        startInTitle = SetAny<Button>(titlePanel, "StartButton");
        exitInTitle = SetAny<Button>(titlePanel, "ExitButton");

        startInLobby = SetAny<Button>(lobbyPanel, "StartButton");
        startTextInLobby = SetText(startInLobby.gameObject, "Text");
        optionInLobby = SetAny<Button>(lobbyPanel, "OptionButton");
        matchingPanel = SetGameObj(lobbyPanel, "MatchingPanel");
        nameInputInLobby = SetAny<TMP_InputField>(matchingPanel, "NameInput");
        exitInLobby = SetAny<Button>(lobbyOptionPanel, "ExitButton");
        infoInLobby = SetAny<Button>(lobbyOptionPanel, "InfoButton");


        statPanel = SetGameObj(playerPanel, "AbilltyPanel");
        buildPanel = SetGameObj(playerPanel, "BuildingPanel");
        buildButton = SetAny<Button>(playerPanel, "BuildButton");
        statButton = SetAny<Button>(playerPanel, "StatButton");
        timeTxt = SetText(timePanel, "time");

        resourcePanel = FindElement("ResourcePanel");
        chocolateTxt = SetText(resourcePanel, "Chocolate");
        sugarTxt = SetText(resourcePanel, "Sugar");
        doughTxt = SetText(resourcePanel, "Dough");

        nextInDefeat = SetAny<Button>(defeatPanel, "Next");
        nextInVictory = SetAny<Button>(victoryPanel, "Next");

        skillCokeShot = SetGameObj(characterInfoPanel, "CokeShot");
        skillCakeRush = SetGameObj(characterInfoPanel, "CakeRush");
        skillShotingStar = SetGameObj(characterInfoPanel, "ShotingStar");
        skillLightning = SetGameObj(characterInfoPanel, "Lightning");

        cokeShotActive = SetGameObj(skillCokeShot, "SkillActive");
        cakeRushActive = SetGameObj(skillCakeRush, "SkillActive");
        shootingStarActive = SetGameObj(skillShotingStar, "SkillActive");
        lightningActive = SetGameObj(skillLightning, "SkillActive");

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
        //skillLightning.onClick.AddListener(OnClickLightning);*/
    }

    #region object Elements

    private Image infoImage;
    private Image mapImage;

    private Text playTimeText;
    private Text unitSizeText;
    private Text eventText;
    private Text curCost;

    private PlayerController player;

    private Canvas sceneUICanvas;
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
    #endregion

    #region value elements

    private bool callNotice;
    private float noticeTime;
    private float noticeTimer;

    private Button nextInDefeat;

    public Slider hpBar;
    private Slider expBar;
    private TMP_Text exp;
    #endregion

    #region Find Functions
    public T Find<T>(string name, Transform parent) where T : class
    {
        Transform canvas = parent.Find("Canvas");
        return canvas.Find(name).GetComponent<T>();
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


    #endregion

    #region Player Info

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
        if (player.levelSystem.curLevel < 10)
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

    #endregion


    #region Notice

    private IEnumerator Notice()
    {
        while (true)
        {
            if (callNotice)
            {
                callNotice = false;
                noticePanel.SetActive(true);
                while (noticeTime >= noticeTimer)
                {
                    if (callNotice)
                        break;
                    noticeTimer += Time.deltaTime;
                    yield return null;
                }
                noticePanel.SetActive(false);
            }
            yield return null;
        }
    }

    public void Notice(string text, float time)
    {
        noticePanel.transform.localPosition = Vector3.zero;
        noticeText.text = text;
        noticeTime = time;
        noticeTimer = 0;
        callNotice = true;
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

    #region Function

    public void SetStartTextInLobby(string text)
    {
        startTextInLobby.text = text;
    }
    public void ShowUI(Define.Scene nowScene)
    {
        titlePanel.SetActive(nowScene == Define.Scene.Title);
        lobbyPanel.SetActive(nowScene == Define.Scene.Lobby);
        victoryPanel.SetActive(nowScene == Define.Scene.Victory);
        defeatPanel.SetActive(nowScene == Define.Scene.Defeat);
    }

    #endregion

    #region button

    public void OnClickStartInTitle()
    {
        Managers.instance._game.OnClickStartInTitle();
    }

    public void OnClickStartInLobby()
    {
        if (Managers.instance._game.nowCloseMatching)
        {
            Notice("��Ī�� ����ϴ� �� �Դϴ�.", 1);
        }
        else
        {
            if (Managers.instance._game.nowMatching)
            {
                if (Managers.instance._game.nowInRoom)
                {
                    Managers.instance._game.nowCloseMatching = true;
                    Notice("��Ī�� ����մϴ�.", 1);
                    SetStartTextInLobby("��Ī �����");
                }
                else
                {
                    Notice("�濡 �����ϴ� �� �Դϴ�.", 1);
                }
            }
            else
            {
                if (Managers.instance._server.isNullableNickName())
                {
                    Notice("�г����� �Է��� �ּ���.", 1);
                }
                else if (!Managers.instance._game.nowInRoom)
                {
                    //GameManager.instance.OnClickStartInLobby();
                    Managers.instance._game.nowMatching = true;
                    Notice("��Ī�� �����߽��ϴ�.", 1);
                    SetStartTextInLobby("��Ī ���");
                }
                else
                {
                    Notice("�濡�� ������ �� �Դϴ�.", 1);
                }
            }
        }
    }

    public void OnClickExit()
    {
        
    }

    public void OnClickOption()
    {
        lobbyOptionPanel.SetActive(!lobbyOptionPanel.activeSelf);
    }

    public void OnClickInfo()
    {
        Notice($"��� �غ���..", 1);
    }

    public void OnClickNameSubmit(string text)
    {
        Managers.instance._server.SetNickName(text);
        Notice($"�г����� '{text}'�� �����߽��ϴ�.", 1);
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
        sugarTxt.text = $"{sug}";
        doughTxt.text = $"{dou}";
    }

    private void OnClickBuild()
    {
        buildPanel.SetActive(!buildPanel.activeSelf);
    }

    private void OnClickStat()
    {
        statPanel.SetActive(!statPanel.activeSelf);
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
        Managers.instance._game.SetScene(Define.Scene.Lobby);
    }

    #endregion

}