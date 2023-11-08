using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    private int activeScene;
    private static int lastCompletedLevel = 1;

    [SerializeField] private Image FadeOut;
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionAsset player;

    private void OnTriggerEnter(Collider other)
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        //Open endscreen
        if (activeScene == 9)
        {
            StartCoroutine(EndGame());
            return;
        }
        
        //load next level on player collision
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNextLevel();

            if (activeScene + 2 > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", activeScene + 1);
            }
        }
    }

    /// <summary>
    /// coroutine to load endscreen after fade out
    /// </summary>
    /// <returns></returns>
    IEnumerator EndGame()
    {
        animator.SetBool("Fade", true);
        //player.Disable();
        yield return new WaitUntil(() => FadeOut.color.a == 1);

        SceneManager.LoadSceneAsync("EndScreen");
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// loads next level 
    /// </summary>
    public void LoadNextLevel()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(activeScene + 1);
        
        Target.amountTargetsHasBeenHit = 0;
    }
}