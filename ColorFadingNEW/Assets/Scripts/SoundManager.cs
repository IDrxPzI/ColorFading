using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("SoundClips")] [SerializeField]
    private SoundClips[] sfxSounds, musicSounds;

    [Header("AudioSources")] [SerializeField]
    private AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Background_Music");
    }

    /// <summary>
    /// start the background music
    /// </summary>
    /// <param name="_name"></param>
    public void PlayMusic(string _name)
    {
        SoundClips s = Array.Find(musicSounds, x => x.name == _name);

        musicSource.clip = s.clip;
        musicSource.Play();
    }

    /// <summary>
    /// plays the soundeffekt given through the name
    /// </summary>
    /// <param name="_name"></param>
    public void PlaySFX(string _name)
    {
        SoundClips s = Array.Find(sfxSounds, x => x.name == _name);

        sfxSource.PlayOneShot(s.clip);
    }

    /// <summary>
    /// sets volume of the music and saves it for the settings
    /// </summary>
    /// <param name="_volume"></param>
    public void MusicVolume(float _volume)
    {
        musicSource.volume = _volume;
        PlayerPrefs.SetFloat("MusicVolume", musicSource.volume);
    }

    /// <summary>
    /// sets volume of the sfx and saves it for the settings
    /// </summary>
    /// <param name="_volume"></param>
    public void SFXVolume(float _volume)
    {
        sfxSource.volume = _volume;
        PlayerPrefs.SetFloat("SFXVolume", sfxSource.volume);
    }
}

/// <summary>
/// audioclip to be played when name called
/// </summary>
[Serializable]
public struct SoundClips
{
    public string name;
    public AudioClip clip;
}