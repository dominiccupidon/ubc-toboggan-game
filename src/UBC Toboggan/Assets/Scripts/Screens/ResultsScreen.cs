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
        prompt.SetActive(UIManager.Instance.isFinalResultsScreen);  
        TMP_Text t = prompt.GetComponent<TMP_Text>();
        t.text = UIManager.Instance.isControllerConnected ? Prompts.GameOverControllerExitPrompt : Prompts.GameOverKeyboardExitPrompt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
