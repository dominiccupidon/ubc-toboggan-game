using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void newGame()
    {
        SceneManager.LoadScene("Farm");
    }

    public void exitToDesktop()
    {
        Application.Quit();
        // Code below will only run in the Unity Editor, not in the actual game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            newGame();
        }
    }
}
