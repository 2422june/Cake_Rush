using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySoundController : MonoBehaviour
{
    AudioSource bgmSource;
    AudioSource fxSource;

    Define.GameSound BGM_Lobby_Main;
    Define.GameSound BGM_Title_Main;

    bool isGameStarted;

    void Awake()
    {
        BGM_Lobby_Main = Define.GameSound.BGM_Lobby_Main;
        BGM_Title_Main = Define.GameSound.BGM_Title_Main;
        
        bgmSource = transform.Find("BGMSource").gameObject.GetComponent<AudioSource>();
        fxSource = transform.Find("FXSource").gameObject.GetComponent<AudioSource>();
        bgmSource.loop = true;
    }
    void Start()
    {
        SoundManager.instance.PlayClip(ref fxSource, Define.GameSound.FX_Lobby_GameStart);
    }

    void Update()
    {
        if(GameManager.instance.nowScene == Define.Scene.title && UiManager.instance.isExitLoading && fxSource.isPlaying == false)
        {
            if(bgmSource.isPlaying) 
                return;
            SoundManager.instance.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Title_Main);
        }

        if(GameManager.instance.nowScene == Define.Scene.lobby)
        {

            if(GameManager.instance.nowMatching == true)
            {
                if(SoundManager.instance.isClipEqualSourceClip(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching)) 
                    return; 
                SoundManager.instance.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching);
            }
            else
            {
                if(SoundManager.instance.isClipEqualSourceClip(ref bgmSource, BGM_Lobby_Main)) 
                return; 

                SoundManager.instance.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Lobby_Main);

                if(GameManager.instance.nowMatching == true)
                {
                    if(SoundManager.instance.isClipEqualSourceClip(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching)) 
                        return; 
                    SoundManager.instance.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching);
                }
            }
        }

        if(GameManager.instance.nowScene == Define.Scene.inGame && isGameStarted == false)
        {
            isGameStarted = true;
            bgmSource.Stop();
            bgmSource.clip = null;
            SoundManager.instance.PlayClip(ref fxSource, Define.GameSound.FX_Lobby_GameStart);
        }
    }
}