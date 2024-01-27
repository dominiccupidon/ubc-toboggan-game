using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    TMP_Text title;
    // Image background;

    void Start()
    {
        title = mainMenu.GetComponentInChildren<TMP_Text>(true);
        title.text = "Welcome";
    }

    public void newGame()
    {
        SceneManager.LoadScene("Farm");
        SpriteRenderer mainMenuRenderer = mainMenu.GetComponent<SpriteRenderer>();
        mainMenuRenderer.color = new Color(1f, 1f, 1f, 0f);
        title.text = "How to Play";
    }

    public void exitToDesktop()
    {
        Application.Quit();
        // Code below will only run in the Unity Editor, not in the actual game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }
}
