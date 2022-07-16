using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Sound
{
    public Define.GameSound sound;
    public AudioClip clip;
}

public class SoundManager : MonoSingleton<SoundManager>
{
    public Sound[] GameSounds;

    void Awake()
    {
        GameSounds = new Sound[Enum.GetValues(typeof(Define.GameSound)).Length];
        for(int i = 0; i < GameSounds.Length; i++)
        {
            GameSounds[i].sound = (Define.GameSound)Enum.Parse(typeof(Define.GameSound), i.ToString());
            AudioClip newClip = Resources.Load<AudioClip>($"Sounds/{Enum.GetName(typeof(Define.GameSound), i)}");
            if(newClip != null)
                GameSounds[i].clip = newClip;
            else
                Debug.Log("AudioClip is not loaded, " + GameSounds[i].sound.ToString());
        }
        DontDestroyOnLoad(gameObject);
    }

    AudioClip ReturnClip(Define.GameSound sound)
    {
        AudioClip returnClip = GameSounds[(int)sound].clip;
        if(returnClip != null)
        {
            return returnClip;
        }
        return null;
    }

    public void ChangeOrPlayBGM(ref AudioSource source, Define.GameSound sound)
    {
        source.clip = ReturnClip(sound);
        source.Play();
    }

    public void PlayClip(ref AudioSource source, Define.GameSound sound)
    {
        source.PlayOneShot(ReturnClip(sound));
    }

    public bool isClipEqualSourceClip(ref AudioSource source, Define.GameSound sound)
    {
        if(source.clip == SoundManager.instance.GameSounds[(int)sound].clip)
            return true;
        else 
            return false;
    }
}
