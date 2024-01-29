using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject skipBtn;

    TMP_Text title;
    IEnumerator coroutine;
    Image mainMenuBg;

    void Start()
    {
        title = mainMenu.GetComponentInChildren<TMP_Text>(true);
        title.text = "Welcome";
    }

    public void newGame()
    {
        Camera mainCamera = GameObject.Find("/Main Camera").GetComponent<Camera>();
        Image mainMenuBg = mainMenu.GetComponent<Image>();
        Button[] btns = mainMenu.GetComponentsInChildren<Button>();
        GameObject controlPanel = mainMenu.transform.Find("Panel").gameObject;
        foreach (Button b in btns) {
            if (b.gameObject.name != "Skip Button")
            {
                b.gameObject.SetActive(false);
            }
        }
        title.text = "How to Play";
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
                skipBtn.SetActive(true);
            }
        }
        LoadGame();
    }


    public void LoadGame()
    {
        StopCoroutine(coroutine);
        SceneManager.LoadScene("Farm");
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            newGame();
        }
    }
}
