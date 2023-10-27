using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool isFinalResultsScreen = false;
    private bool isFlipBonusShowing = false;
    private bool isAirTimeBonusShowing = false;
    public GameObject timer;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject resultsScreen;
    public GameObject finalScore;
    public GameObject flipBonus;
    public GameObject airTimeBonus;
    private GameObject controlsButton;
    private GameObject audioButton;

    // Start is called before the first frame update
    void Start()
    {
       controlsButton = pauseMenu.transform.GetChild(1).gameObject;
       audioButton = pauseMenu.transform.GetChild(2).gameObject;
       controlsButton.SetActive(false);
       audioButton.SetActive(false);
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
       if (Input.GetKeyDown(KeyCode.Return))
       {
            isPaused = !isPaused;
            TogglePauseMenu();
       }
       
       if (gameOverScreen.activeInHierarchy || pauseMenu.activeInHierarchy || resultsScreen.activeInHierarchy)
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

            if (isPaused) {
                isFlipBonusShowing = flipBonus.activeInHierarchy;
                isAirTimeBonusShowing = airTimeBonus.activeInHierarchy;
                finalScore.SetActive(false);
                flipBonus.SetActive(false);
                airTimeBonus.SetActive(false);
            } else {
                finalScore.SetActive(true);
                Debug.Log(isFlipBonusShowing);
                Debug.Log(isAirTimeBonusShowing);
                flipBonus.SetActive(isFlipBonusShowing);
                airTimeBonus.SetActive(isAirTimeBonusShowing);
            }
        }
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        timer.SetActive(false);
    }

    public void ShowResultsScreen()
    {
        // Create a enum for all the tags in the project
        if (!gameOverScreen.activeInHierarchy)
        {
            resultsScreen.SetActive(true);
            GameObject restartOrQuitPrompt = GameObject.Find("Prompt - Q,R");
            restartOrQuitPrompt.SetActive(isFinalResultsScreen);
            timer.SetActive(false);
        }
    }
    
    private IEnumerator RestartGame()
    {
        // Set timer back to 3 secs in full game
        yield return new WaitForSecondsRealtime(0f);
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("TestLevel");
    }
}
