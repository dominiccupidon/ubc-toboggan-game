using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Constants;

public class PauseMenu : MonoBehaviour
{
    public GameObject controlsTabPanel;
    public GameObject audioTabPanel;
    public GameObject controlsTabButton;
    public GameObject audioTabButton;

    TMP_Text musicVolume;
    TMP_Text effectsVolume;
    bool isControllerConnected;

    // Start is called before the first frame update
    void Start()
    {
        controlsTabPanel.SetActive(true);
        audioTabPanel.SetActive(false); 
        GameObject[] prompts = GameObject.FindGameObjectsWithTag("Prompt");
        isControllerConnected = UIManager.Instance.isControllerConnected;
        foreach (GameObject p in prompts)
        {
            TMP_Text t = p.GetComponent<TMP_Text>();
            if ((p.name == "L1" || p.name == "L2")  && !isControllerConnected)
            {
                p.SetActive(false);
            } 

            if (p.name == "Prompt - Controls")
            {
                t.text = isControllerConnected ? Prompts.PauseMenuControllerControlsPrompt : Prompts.PauseMenuKeyboardControlsPrompt;
            } else if (p.name == "Prompt - ESC,Q,R")
            {
                t.text = isControllerConnected ? Prompts.PauseMenuControllerExitPrompt : Prompts.PauseMenuKeyboardExitPrompt;
            }
                
        }
        initializeSliders();
    }


    void Update()
    {
        Tab t1 = controlsTabButton.GetComponent<Tab>();
        Tab t2 = audioTabButton.GetComponent<Tab>();
        if (Input.GetButtonDown("Shoulder1") && isControllerConnected)
        {
            t1.OnPointerClick(null);
        }

        if (Input.GetButtonDown("Shoulder2") && isControllerConnected)
        {
            t2.OnPointerClick(null);
        }
    }

    void initializeSliders()
    {
        TMP_Text[] texts = audioTabPanel.GetComponentsInChildren<TMP_Text>(true);
        foreach (TMP_Text t in texts) {
            if (t.gameObject.name == "Music Value") {
                musicVolume = t;
            } else if (t.gameObject.name == "Effects Value") {
                effectsVolume = t;
            }
        }

        musicVolume.text = String.Format("{0:0}%", UIManager.Instance.soundManager.musicSources[0].volume * 100);
        // Ask Aidan what value to use for the default effects volume 
        effectsVolume.text = String.Format("{0:0}%", UIManager.Instance.soundManager.effectSources[0].volume * 100); 
    }

    public void OnMusicSliderChanged(float value)
    {
        // Code to update the text %
        // Add code to keep sound value consistent across scenes
        UIManager.Instance.soundManager.changeMusicVolume(value);
        musicVolume.text = String.Format("{0:0}%", value * 100);
    }

    public void OnEffectsSliderChanged(float value) {
        UIManager.Instance.soundManager.wasEffectVolumeChangedDuringPause = true;
        UIManager.Instance.soundManager.changeEffectVolume(value);
        effectsVolume.text = String.Format("{0}%", value * 100);
    }
} 
