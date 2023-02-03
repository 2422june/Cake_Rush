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
        Managers.instance._sound.PlayClip(ref fxSource, Define.GameSound.FX_Lobby_GameStart);
    }

    void Update()
    {
        if(Managers.instance._game.nowScene == Define.Scene.Title && UIManager.instance.isLoadingOff && fxSource.isPlaying == false)
        {
            if(bgmSource.isPlaying) 
                return;
            Managers.instance._sound.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Title_Main);
        }

        if(Managers.instance._game.nowScene == Define.Scene.Lobby)
        {

            if(Managers.instance._game.nowMatching == true)
            {
                if(Managers.instance._sound.isClipEqualSourceClip(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching)) 
                    return;
                Managers.instance._sound.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching);
            }
            else
            {
                if(Managers.instance._sound.isClipEqualSourceClip(ref bgmSource, BGM_Lobby_Main)) 
                return;

                Managers.instance._sound.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Lobby_Main);

                if(Managers.instance._game.nowMatching == true)
                {
                    if(Managers.instance._sound.isClipEqualSourceClip(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching)) 
                        return;
                    Managers.instance._sound.ChangeOrPlayBGM(ref bgmSource, Define.GameSound.BGM_Lobby_GameMatching);
                }
            }
        }

        if(Managers.instance._game.nowScene == Define.Scene.InGame && isGameStarted == false)
        {
            isGameStarted = true;
            bgmSource.Stop();
            bgmSource.clip = null;
            Managers.instance._sound.PlayClip(ref fxSource, Define.GameSound.FX_Lobby_GameStart);
        }
    }
}