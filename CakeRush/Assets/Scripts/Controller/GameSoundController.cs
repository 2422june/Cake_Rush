using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundController : MonoBehaviour
{
    AudioSource bgmSource;
    AudioSource fxSource;
    
    void Awake()
    {
        bgmSource = transform.Find("BGMSource").gameObject.GetComponent<AudioSource>();
        fxSource = transform.Find("FXSource").gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        SoundManager.instance.ChangeOrPlayBGM(ref fxSource, Define.GameSound.BGM_Game_Main);
    }
}
