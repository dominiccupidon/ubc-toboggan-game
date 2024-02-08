using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Constants;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject skipPrompt;

    TMP_Text title;
    IEnumerator coroutine;
    GameObject controlPanel;
    Image mainMenuBg;
    bool isUsingController;

    void Start()
    {
        title = GameObject.FindWithTag("Title").GetComponent<TMP_Text>();
        GameObject[] prompts = GameObject.FindGameObjectsWithTag("Prompt");
        title.text = Prompts.MainMenuWelcomePrompt;
        isUsingController = Input.GetJoystickNames().Length > 0;
        controlPanel = mainMenu.transform.Find("Panel").gameObject;

        foreach (GameObject p in prompts) 
        {
            TMP_Text t = p.GetComponent<TMP_Text>();
            if (p.name == "Play Prompt") {
                t.text = isUsingController ? Prompts.MainMenuControllerStartPrompt : Prompts.MainMenuKeyboardStartPrompt;
            } else if (p.name == "Quit Prompt") {
                t.text = isUsingController ? Prompts.MainMenuControllerQuitPrompt : Prompts.MainMenuKeyboardQuitPrompt;
            } else if (p.name == "Skip Prompt") {
                t.text = isUsingController ? Prompts.MainMenuControllerSkipPrompt : Prompts.MainMenuKeyboardSkipPrompt;
                p.SetActive(false);
            } else if (p.name == "Prompt - Controls") {
                t.text = isUsingController ? Prompts.MainMenuControllerControlsPrompt : Prompts.MainMenuKeyboardControlsPrompt;
            }
        }
        controlPanel.SetActive(false);
    }

    void Update()
    {
        // Enter 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire2"))
        {
            if (skipPrompt.activeInHierarchy) 
            {
                LoadGame();
            } else 
            {
                newGame();
            }
        }

        // Create check that prevents this code from running when the user has started
        // a new game
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Fire1")) && !controlPanel.activeInHierarchy)
        {
            exitToDesktop();
        }
    }

    public void newGame()
    {
        Camera mainCamera = GameObject.Find("/Main Camera").GetComponent<Camera>();
        Image mainMenuBg = mainMenu.GetComponent<Image>();
        GameObject[] prompts = GameObject.FindGameObjectsWithTag("Prompt");

        foreach (GameObject p in prompts) 
        {
            if (p.name != "Skip Prompt" && p.name != "Prompt - Controls")
            {
                p.SetActive(false);
            }
        }

        title.text = Prompts.MainMenuControlsPrompt;
        title.color = new Color(1f, 1f, 1f, 1f);
        mainMenuBg.color = new Color(0f, 0f, 0f, 1f);
        mainCamera.backgroundColor = new Color(0f, 0f, 0f, 1f);
        controlPanel.SetActive(true);
        coroutine = ControlPanelTimer();
        StartCoroutine(coroutine);
    }

    public void exitToDesktop()
    {
        Application.Quit();
        // Code below will only run in the Unity Editor, not in the actual game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }

    private IEnumerator ControlPanelTimer()
    {
        int count = 0;
        while (count < 10)
        {
            yield return new WaitForSecondsRealtime(1f);
            count++;
            if (count == 4)
            {
                skipPrompt.SetActive(true);
            }
        }
        LoadGame();
    }


    public void LoadGame()
    {
        StopCoroutine(coroutine);
        SceneManager.LoadScene("Farm");
    }
    
}
