using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
} 
