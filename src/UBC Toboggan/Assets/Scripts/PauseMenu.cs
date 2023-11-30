using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject controlsTab;
    public GameObject audioTab;
    // Start is called before the first frame update
    void Start()
    {
       controlsTab.SetActive(true);
       audioTab.SetActive(false); 
    }
    
    public void ShowControlsTab()
    {
        controlsTab.SetActive(true);
        audioTab.SetActive(false);
    }

    public void ShowAudioTab()
    {
        controlsTab.SetActive(false);
        audioTab.SetActive(true);
    }
}
