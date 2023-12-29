using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Collider2D triggerLevelEnd;
 
    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        UIManager.Instance.ShowResultsScreen();
    }
}
