using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount - 1];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
        
            for(int i = 0; i < audioSources.Length; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.GetOrAddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
        }

        audioSources[(int)Define.Sound.BGM].loop = true;
    }

    public void Play(string path, Define.Sound type = Define.Sound.SFX, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(path, type, pitch);
        
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.SFX, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if(type == Define.Sound.BGM)
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.BGM];
            
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.SFX];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlaySurrond(GameObject go, string path)
    {
        if (go == null)
            return;

        AudioClip audioClip = GetOrAddAudioClip(path, Define.Sound.SFX);
    }


    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.SFX)
    {
        if (path.Contains("Sound/") == false)
            path = $"Sound/{path}";

        AudioClip audioClip = null;

        if(type == Define.Sound.BGM)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if(audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"Missing AudioClip {path}");

        return audioClip;
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }

        audioClips.Clear();
    }
}
