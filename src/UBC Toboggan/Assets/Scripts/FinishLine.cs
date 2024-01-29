using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Collider2D triggerLevelEnd;
    public bool levelEndTriggered = false;
    public GameObject cam;
    public cameraFollow camScript;
    public GameObject blackFade;
    private SpriteRenderer blackRenderer;
    float fadeAmount = 0f;
    float fadeTime = 0.25f;
    float fadeDelay = 0.25f;
    bool showLevelStartFade = true;
    float timer = 0.25f;
    float transitionTime = 5f;


    
    void Start() {
        camScript = cam.GetComponent<cameraFollow>();
        blackRenderer = blackFade.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (showLevelStartFade) { 
            timer -= Time.deltaTime;
            fadeAmount = timer / fadeTime;
            if (timer <= 0) {
                timer = 0;
                fadeAmount = 0f;
                showLevelStartFade = false;
            }
            blackRenderer.color = new Color(1f,1f,1f, fadeAmount);
        }
        else if (levelEndTriggered) {
            if (Time.timeScale == 0f) { // find actual problem here
                Time.timeScale = 1f;
            }
            timer += Time.deltaTime;
            if (timer >= fadeDelay) {
                fadeAmount = (timer-fadeDelay)/fadeTime;
            }
            if (fadeAmount > 1) {
                fadeAmount = 1f;
            }
            
            blackRenderer.color = new Color(1f,1f,1f, fadeAmount);
            if (timer > transitionTime) {
                // Create a function to retrieve the scene corresponding
                // to the next level
                SceneManager.LoadScene("Farm2");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!levelEndTriggered) {
            StartCoroutine(LoadScene());
            levelEndTriggered = true;
            camScript.followSpeed = 0f;
        }
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        UIManager.Instance.ShowResultsScreen();
    }
}
