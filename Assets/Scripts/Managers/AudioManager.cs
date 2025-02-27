using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Clip
{
    Ambient_Rain,

    Music_Title,
    Music_Game,

    SoundFX_GetItem,
    SoundFX_CreateItem,
    SoundFX_Rain,
    SoundFX_Shooting,
    SoundFX_Start,
    SoundFX_TypingSound,
}

public class AudioManager
{
    private static readonly float MASTER_VOLUME = 0.2f;
    private static readonly float[] VOLUMES =
    {
        1.0f * MASTER_VOLUME,   // Ambient
        0.5f * MASTER_VOLUME,   // Music
        0.2f * MASTER_VOLUME,   // MusicFX
        0.6f * MASTER_VOLUME,   // SoundFX
    };

    public enum Type
    {
        Ambient,
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
            audioSources[i] = audioSource;
        }
    }

    public void Play(Clip key, float volumeScale = 1.0f)
    {
        Type type;
        try
        {
            type = GetType(key.ToString());
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
            case Type.Ambient:
                Play_Ambient(audioSource, clip, volumeScale);
                break;
            case Type.Music:
                Play_Music(audioSource, clip, volumeScale);
                break;
            case Type.MusicFX:
                Debug.LogWarning($"Failed to Play({key})");
                break;
            case Type.SoundFX:
                Play_SoundFX(audioSource, clip, volumeScale);
                break;
        }
    }

    private void Play_Ambient(AudioSource audioSource, AudioClip clip, float volumeScale)
    {
        audioSource.clip = clip;
        audioSource.DOFade(VOLUMES[(int)Type.Ambient] * volumeScale, 1.0f).From(0.0f);
        audioSource.Play();
    }

    private void Play_Music(AudioSource audioSource, AudioClip clip, float volumeScale)
    {
        if (audioSource.clip != null)
        {
            Play_MusicFX(audioSource.clip, audioSource.time, audioSource.volume);
        }

        audioSource.clip = clip;
        audioSource.DOFade(VOLUMES[(int)Type.Music] * volumeScale, 2.0f).From(0.0f);
        audioSource.Play();
    }

    private void Play_MusicFX(AudioClip clip, float time, float volume)
    {
        AudioSource audioSource = audioSources[(int)Type.MusicFX];
        audioSource.clip = clip;
        audioSource.time = time;
        audioSource.volume = volume;
        audioSource.DOFade(0.0f, 1.0f).OnComplete(() => audioSource.Stop());
        audioSource.Play();
    }

    private void Play_SoundFX(AudioSource audioSource, AudioClip clip, float volumeScale)
    {
        if (soundClips.Add(clip) == false)
        {
            return;
        }

        audioSource.PlayOneShot(clip, volumeScale);
        DOVirtual.DelayedCall(0.1f, () => soundClips.Remove(clip));
    }

    public void Stop_Ambient()
    {
        AudioSource audioSource = audioSources[(int)Type.Ambient];
        audioSource.DOFade(0.0f, 1.0f).OnComplete(() => audioSource.Stop());
    }

    private Type GetType(string key)
    {
        string type = key[..key.IndexOf('_')];
        return (Type)Enum.Parse(typeof(Type), type);
    }
}