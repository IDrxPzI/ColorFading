using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] private GameObject credits;
    private int levelToLoad;

    /// <summary>
    /// starts the first level of the game
    /// </summary>
    public void StartGame()
    {
        Target.amountTargetsHasBeenHit = 0;

        SceneManager.LoadSceneAsync("Level1");
        Debug.Log("loaded level1 ");
    }

    /// <summary>
    /// opens level selection menu
    /// </summary>
    public void OpenLevelSelection()
    {
        SceneManager.LoadSceneAsync("LevelSelection");
    }

    /// <summary>
    /// closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    

    /// <summary>
    /// opens options menu
    /// </summary>
    public void OpenOptionsMenu()
    {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }

    /// <summary>
    /// opens credits window
    /// </summary>
    public void OpenCredits()
    {
        credits.SetActive(true);
    }

    /// <summary>
    /// closes credit window
    /// </summary>
    public void CloseCredits()
    {
        credits.SetActive(false);
    }   

    /// <summary>
    /// opens the main menu
    /// </summary>
    public void OpenMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Debug.Log("Back to menu");

        Cursor.lockState = CursorLockMode.None;
    }
}