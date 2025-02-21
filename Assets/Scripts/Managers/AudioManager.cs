using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private static readonly float[] VOLUMES =
    {
        0.2f,   // Music
        0.2f,   // MusicFX
        0.5f,   // SoundFX
    };

    public enum Type
    {
        Music,
        MusicFX,
        SoundFX,
        Count
    }

    private readonly Transform transform = new GameObject(nameof(AudioManager), typeof(AudioListener)).transform;
    private readonly AudioSource[] audioSources = new AudioSource[(int)Type.Count];
    private readonly HashSet<AudioClip> soundClips = new();

    public void Initialize()
    {
        transform.SetParent(Managers.Instance.transform);

        var names = Enum.GetNames(typeof(Type));
        for (int i = 0; i < audioSources.Length; i++)
        {
            Transform child = new GameObject(names[i]).transform;
            child.SetParent(transform);

            AudioSource audioSource = child.gameObject.AddComponent<AudioSource>();
            audioSource.loop = (Type)i != Type.SoundFX;
            audioSource.playOnAwake = false;
            audioSource.volume = VOLUMES[i];
        }
    }

    public void Play(string key)
    {
        Type type;
        try
        {
            type = GetType(key);
        }
        catch
        {
            Debug.LogWarning($"Failed to GetType({key})");
            return;
        }

        string path = $"{Define.AUDIO}/{key}";
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogWarning($"Failed to Load<AudioClip>({key})");
            return;
        }

        AudioSource audioSource = audioSources[(int)type];
        switch (type)
        {
            case Type.Music:
                Play_Music(audioSource, clip);
                break;
            case Type.MusicFX:
                Debug.LogWarning($"Failed to Play({key})");
                break;
            case Type.SoundFX:
                Play_SoundFX(audioSource, clip);
                break;
        }
    }

    private void Play_Music(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource.clip != null)
        {
            Play_MusicFX(audioSource.clip, audioSource.time, audioSource.volume);
        }

        audioSource.clip = clip;
        audioSource.DOFade(VOLUMES[(int)Type.Music], 2.0f).From(0.0f);
        audioSource.Play();
    }

    private void Play_MusicFX(AudioClip clip, float time, float volume)
    {
        AudioSource audioSource = audioSources[(int)Type.MusicFX];
        audioSource.clip = clip;
        audioSource.time = time;
        audioSource.volume = volume;
        audioSource.DOFade(0.0f, 1.0f);
        audioSource.Play();
    }

    private void Play_SoundFX(AudioSource audioSource, AudioClip clip)
    {
        if (soundClips.Add(clip) == false)
        {
            return;
        }

        audioSource.PlayOneShot(clip);
        DOVirtual.DelayedCall(0.1f, () => soundClips.Remove(clip));
    }

    private Type GetType(string key)
    {
        string type = key[..key.IndexOf('_')];
        return (Type)Enum.Parse(typeof(Type), type);
    }
}