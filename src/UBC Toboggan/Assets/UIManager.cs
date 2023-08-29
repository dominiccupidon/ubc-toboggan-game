using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool isFinalResultsScreen = false;
    public GameObject timer;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject resultsScreen;

    // Start is called before the first frame update
    void Start()
    {
       pauseMenu.SetActive(false);
       gameOverScreen.SetActive(false);
       resultsScreen.SetActive(false);
       // Create an enum with the Scene names
       // For actual game change the string to the enum corresponding with the scene for the final level
       isFinalResultsScreen = SceneManager.GetActiveScene().name == "TestLevel";
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            isPaused = !isPaused;
            TogglePauseMenu();
       }
       
       if (gameOverScreen.activeInHierarchy || pauseMenu.activeInHierarchy)
       {
           if (Input.GetKeyDown(KeyCode.Q))
           {
                QuitGame();
           } else if (Input.GetKeyDown(KeyCode.R))
           {
                StartCoroutine(RestartGame());
           }
        }
    }

    void  QuitGame()
    {    
        Application.Quit();
        // Code below will only run in the Unity Editor, not in the actual game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }


    void TogglePauseMenu()
    {
        if (!resultsScreen.activeInHierarchy)
        {
            // Setting Time.timeScale to 0 freezes any physics and animations that use Time.deltaTime
            Time.timeScale = isPaused ? 0f : 1f;
            pauseMenu.SetActive(isPaused);
            timer.SetActive(!isPaused);
        }
    }

    public void ShowGameOverScreen()
    {
        if (!resultsScreen.activeInHierarchy)
        {    
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
            timer.SetActive(false);
        }
    }

    public void ShowResultsScreen()
    {
        // Create a enum for all the tags in the project
        resultsScreen.SetActive(true);
        GameObject restartOrQuitPrompt = GameObject.Find("Prompt - Q,R");
        restartOrQuitPrompt.SetActive(isFinalResultsScreen);
        timer.SetActive(false);
    }
    
    private IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("TestLevel");
    }
}
