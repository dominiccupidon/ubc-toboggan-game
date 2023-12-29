using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject prompt = GameObject.Find("Prompt - Q, R");
        prompt.SetActive(UIManager.Instance.isFinalResultsScreen);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
