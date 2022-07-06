using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Chating : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject[] textBoxs;
    [SerializeField]
    private TMP_InputField input;
    [SerializeField]
    private GameObject scrollView;
    [SerializeField]
    private TMP_Text[] texts = new TMP_Text[5];
    private int maxLenght = 5;
    private bool isChat;
    PhotonView PV;

    [SerializeField]
    private GameObject inputField;
    [SerializeField]
    private GameObject ChatingPanel;
    [SerializeField]
    private GameObject viewport;
    [SerializeField]
    private GameObject content;


    GameObject FindElement(GameObject parent, string name)
    {
        return parent.transform.Find(name).gameObject;
    }


    private void Start()
    {
        ChatingPanel = FindElement(gameObject, "ChatingPanel");
        inputField = FindElement(ChatingPanel, "InputField");
        input = inputField.GetComponent<TMP_InputField>();
        scrollView = FindElement(ChatingPanel, "ScrollView");
        viewport = FindElement(scrollView, "Viewport");
        content = FindElement(viewport, "Content");

        isChat = false;
        for (int i = 0; i < maxLenght; i++)
        {
            textBoxs[i] = FindElement(content.gameObject, $"t{5 - i}");
            texts[i] = textBoxs[i].GetComponent<TMP_Text>();
        }
        ChatingPanel.SetActive(false);
        scrollView.SetActive(false);
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isChat)
            {
                isChat = true;
                ChatingPanel.SetActive(true);
                scrollView.SetActive(true);
                input.ActivateInputField();
            }
            else
            {
                if (input.text == "")
                {
                    input.Select();
                    isChat = false;
                    ChatingPanel.SetActive(false);
                    scrollView.SetActive(false);
                    return;
                }
                else
                {
                    PV.RPC("Chat", RpcTarget.All, $"{PhotonNetwork.LocalPlayer.NickName} : {input.text}");
                    input.text = "";
                    Debug.Log("Chat");
                    input.ActivateInputField();
                }
            }
        }
    }


    [PunRPC]
    void Chat(string str)
    {
        for (int i = maxLenght - 1; i > 0; i--)
        {
            texts[i].text = texts[i - 1].text;
        }
        texts[0].text = str;
    }
}
