using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    [Header("Delete Progress")] [SerializeField]
    private GameObject deleteProgressWindow;


    // Start is called before the first frame update
    void Start()
    {
        //first level input
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        //activate buttons for each level completed
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 2 > levelAt)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    /// <summary>
    /// loads the next level
    /// </summary>
    /// <param name="_levelIndex"></param>
    public void LoadLevel(int _levelIndex)
    {
        SceneManager.LoadSceneAsync(_levelIndex);
    }

    /// <summary>
    /// opens delete Progress window
    /// </summary>
    public void DeleteProgress()
    {
        deleteProgressWindow.SetActive(true);
    }

    /// <summary>
    /// resets players completed levels
    /// </summary>
    public void DeleteProgressButton()
    {
        PlayerPrefs.DeleteKey("levelAt");
        //PlayerPrefs.DeleteAll();

        SceneManager.LoadSceneAsync("MainMenu");
    }

    /// <summary>
    /// closes delete progress window
    /// </summary>
    public void CancelDeleteButton()
    {
        deleteProgressWindow.SetActive(false);
    }

    /// <summary>
    /// opens main menu
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}