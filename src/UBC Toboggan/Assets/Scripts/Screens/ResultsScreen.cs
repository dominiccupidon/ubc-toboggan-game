using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using TMPro;

public class ResultsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject prompt = GameObject.Find("Prompt - Q,R");
        GameObject points = GameObject.Find("Points");

        bool isGameComplete = UIManager.Instance.isFinalResultsScreen;
        prompt.SetActive(isGameComplete);  
        TMP_Text prompt_text = prompt.GetComponent<TMP_Text>();
        TMP_Text points_text = points.GetComponent<TMP_Text>();
        
        prompt_text.text = UIManager.Instance.isControllerConnected ? Prompts.GameOverControllerExitPrompt : Prompts.GameOverKeyboardExitPrompt;
        string s1 = string.Format("Congrats, You scored {0:0} points", UIManager.Instance.scoreManager.GetLevelScore());
        string s2 = string.Format("Congrats, You scored {0:0} points for the entire game", UIManager.Instance.scoreManager.GetFinalScore());
        points_text.text = isGameComplete ? s2 : s1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
