using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PauseMenu : MonoBehaviour
{
    public GameObject controlsTab;
    public GameObject audioTab;

    TMP_Text musicVolume;
    TMP_Text effectsVolume;

    // Start is called before the first frame update
    void Start()
    {
        controlsTab.SetActive(true);
        audioTab.SetActive(false); 
        initializeSliders();
    }

    void initializeSliders()
    {
        TMP_Text[] texts = audioTab.GetComponentsInChildren<TMP_Text>(true);
        foreach (TMP_Text t in texts) {
            if (t.gameObject.name == "Music Value") {
                musicVolume = t;
            } else if (t.gameObject.name == "Effects Value") {
                effectsVolume = t;
            }
        }

        musicVolume.text = String.Format("{0}%", UIManager.Instance.soundManager.musicSources[0].volume * 100);
        // Ask Aidan what value to use for the default effects volume 
        effectsVolume.text = String.Format("{0}%", UIManager.Instance.soundManager.effectSources[0].volume * 100); 
    }

    public void OnMusicSliderChanged(float value)
    {
        // Code to update the text %
        // Add code to keep sound value consistent across scenes
        UIManager.Instance.soundManager.changeMusicVolume(value);
        musicVolume.text = String.Format("{0}%", value * 100);
    }

    public void OnEffectsSliderChanged(float value) {
        UIManager.Instance.soundManager.wasEffectVolumeChangedDuringPause = true;
        UIManager.Instance.soundManager.changeEffectVolume(value);
        effectsVolume.text = String.Format("{0}%", value * 100);
    }
} 
