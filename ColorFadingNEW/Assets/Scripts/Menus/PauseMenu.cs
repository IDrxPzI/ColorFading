using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private InputActionAsset player;

    /// <summary>
    /// opens pausemenu
    /// </summary>
    public void OpenMenu()
    {
        if (PlayerMovement.openMenu)
        {
            Canvas.SetActive(true);
            player.Disable();
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// closes pause menu
    /// </summary>
    public void CloseMenu()
    {
        Canvas.SetActive(false);
        player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        //pausemenu doesnt work if removed
        if (PlayerMovement.openMenu)
        {
            OpenMenu();
        }
    }

    /// <summary>
    /// opens optionsmenu
    /// </summary>
    public void OpenOptionsMenu()
    {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }

    /// <summary>
    /// opens main menu
    /// </summary>
    public void OpenMainMenu()
    {
        Target.amountTargetsHasBeenHit = 0;

        SceneManager.LoadSceneAsync("MainMenu");
        Debug.Log("Back to menu");
    }
}