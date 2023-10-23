using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    bool isPressingEnter;
    bool isPressingEsc;

    void Update()
    {
        isPressingEnter = Input.GetKey(KeyCode.Return);
        isPressingEsc = Input.GetKey(KeyCode.Escape);
    }

    void FixedUpdate()
    {
        if (isPressingEnter) 
        {
            newGame();
        }

        if (isPressingEsc)
        {
            exitToDesktop();
        }
    }

    public void newGame()
    {
        SceneManager.LoadScene("TestLevel");
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
