using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum OverlayFlags 
{
    None = 0b_0000_0000,
    Pause = 0b_0000_0001,
    Results = 0b_0000_0010,
    GameOver = 0b_0000_0100
}


public class UIManager : MonoBehaviour
{
    private bool wasBonusShowing = false;
    private OverlayFlags flags = OverlayFlags.None;

    public static UIManager Instance;

    public ScoreManager scoreManager;
    public StopWatch stopWatch;

    // Create an enum with the Scene names
    // For actual game change the string to the enum corresponding with the scene for the final level
    public bool isFinalResultsScreen
    {
        get { return SceneManager.GetActiveScene().name == "TestLevel"; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Return))
       {
            flags ^= OverlayFlags.Pause;
            TogglePauseMenu();
       }
       
       if (SceneManager.sceneCount > 1)
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
        if (flags <= OverlayFlags.Pause)
        {
            // Setting Time.timeScale to 0 freezes any physics and animations that use Time.deltaTime
            bool isPaused = Convert.ToBoolean(flags);
            Time.timeScale = isPaused ? 0f : 1f;
            if (isPaused)
            {
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            } else 
            {
                SceneManager.UnloadSceneAsync("PauseMenu");
            }

            if (isPaused) {
                wasBonusShowing = scoreManager.HideScore();
                stopWatch.HideStopWatch();
            } else {
                scoreManager.ShowScore(wasBonusShowing);
                stopWatch.ShowStopWatch();
            }
        }
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0f;
        stopWatch.HideStopWatch();
        flags ^= OverlayFlags.GameOver;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    public void ShowResultsScreen()
    {
        // Create a enum for all the tags in the project
        flags ^= OverlayFlags.Results;
        if (flags == OverlayFlags.Results)
        {
            stopWatch.HideStopWatch();
            SceneManager.LoadScene("ResultsScreen", LoadSceneMode.Additive);
        }
    }
    
    private IEnumerator RestartGame()
    {
        // Set timer back to 3 secs in full game
        yield return new WaitForSecondsRealtime(0f);
        Time.timeScale = 1f;
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.name != SceneManager.GetActiveScene().name) {
                SceneManager.UnloadSceneAsync(s.name);
            }
        }
        flags = OverlayFlags.None;
        SceneManager.LoadScene("TestLevel");
    }
}
