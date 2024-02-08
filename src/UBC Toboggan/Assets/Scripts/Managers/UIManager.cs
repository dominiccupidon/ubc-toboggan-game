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

    public scoreManager scoreManager;
    public soundManager soundManager;
    public boostBarManager boostBarManager;
    public StopWatch stopWatch;

    // Create an enum with the Scene names
    // For actual game change the string to the enum corresponding with the scene for the final level
    public bool isFinalResultsScreen
    {
        get { return SceneManager.GetActiveScene().name == "TestLevel"; }
    }

    public bool isControllerConnected
    {
        get { return Input.GetJoystickNames().Length > 0; }
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
        SceneManager.activeSceneChanged += PrepareForNextLevel;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire3"))
       {
            TogglePauseMenu();
       }
       
       if (SceneManager.sceneCount > 1 || flags == OverlayFlags.GameOver)
       {
           if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Fire1"))
           {
                QuitGame();
           } else if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
           {
                StartCoroutine(RestartGame());
           }
        }
    }

    void TogglePauseMenu()
    {
        if (flags <= OverlayFlags.Pause)
        {
            flags ^= OverlayFlags.Pause;
            bool isPaused = Convert.ToBoolean(flags);
            // Setting Time.timeScale to 0 freezes any physics and animations that use Time.deltaTime
            Time.timeScale = isPaused ? 0f : 1f;
            if (isPaused)
            {
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
                wasBonusShowing = scoreManager.HideScore();
                soundManager.pauseEffects();
                stopWatch.HideStopWatch();
                boostBarManager.ToggleBar(false);
            } else 
            {
                SceneManager.UnloadSceneAsync("PauseMenu");
                scoreManager.ShowScore(wasBonusShowing);
                soundManager.playEffects();
                stopWatch.ShowStopWatch();
                boostBarManager.ToggleBar(true);
            }
        }
    }

    public void ShowGameOverScreen()
    {
        flags ^= OverlayFlags.GameOver;
        if (flags == OverlayFlags.GameOver) {
            stopWatch.HideStopWatch();
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ShowResultsScreen()
    {
        // Create a enum for all the tags in the project
        Debug.Log(flags);
        if (flags == OverlayFlags.Pause) {
            SceneManager.UnloadSceneAsync("PauseMenu");
            flags = OverlayFlags.None;
        }
        flags ^= OverlayFlags.Results;
        if (flags == OverlayFlags.Results)
        {
            stopWatch.HideStopWatch();
            boostBarManager.ToggleBar(false);
            soundManager.pauseEffects();
            scoreManager.HideScore();
            SceneManager.LoadScene("ResultsScreen", LoadSceneMode.Additive);
        }
    }

    private void PrepareForNextLevel(Scene current, Scene next)
    {
        flags = next.name == "GameOver" ? OverlayFlags.GameOver : OverlayFlags.None;
    }

    void  QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScreen");
        Destroy(gameObject);
    }
    
    private IEnumerator RestartGame()
    {
        // Set timer back to 3 secs in full game
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.name != SceneManager.GetActiveScene().name) {
                SceneManager.UnloadSceneAsync(s.name);
            }
        }
        flags = OverlayFlags.None;
        SceneManager.LoadScene("Farm");
    }
}
