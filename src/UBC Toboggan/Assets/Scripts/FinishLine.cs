using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Collider2D triggerLevelEnd;
    bool levelEndTriggered = false;
 
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!levelEndTriggered) {
            StartCoroutine(LoadScene());
            levelEndTriggered = true;
        }
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        UIManager manager = GetComponentInParent<UIManager>();
        manager.ShowResultsScreen();
    }
}
