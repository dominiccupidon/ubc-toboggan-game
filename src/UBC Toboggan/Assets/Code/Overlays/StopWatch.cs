using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    private float secondsElapsed;
    private bool isTimerRunning;
    public Text clock;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.stopWatch = this;
        secondsElapsed = 0f;
        isTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            secondsElapsed += Time.deltaTime;
            if (secondsElapsed > 90) // Consider setting the limit to 7.5 minutes (450 seconds)
            {
                isTimerRunning = false;
                Application.Quit(); // Add logic to transition to a game over screen;
            }
            displayTime();
        }    
    }
    
    void displayTime()
    {
        float minutes = Mathf.FloorToInt(secondsElapsed / 60);
        float seconds = secondsElapsed % 60;
        float milliseconds = (secondsElapsed % 1) * 1000; 
        clock.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void HideStopWatch()
    {
        gameObject.SetActive(false);
    }

    public void ShowStopWatch()
    {
        gameObject.SetActive(true);
    }
}
