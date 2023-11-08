using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, sfxSlider;

    /// <summary>
    /// sets the volume to the players last settings
    /// </summary>
    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
    
    /// <summary>
    /// sets volume of music
    /// </summary>
    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(musicSlider.value);
    }

    /// <summary>
    /// sets volume of sfx
    /// </summary>
    public void SFXVolume()
    {
        SoundManager.Instance.SFXVolume(sfxSlider.value);
    }

    /// <summary>
    /// closes optionsmenu
    /// </summary>
    public void BackButton()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }
}