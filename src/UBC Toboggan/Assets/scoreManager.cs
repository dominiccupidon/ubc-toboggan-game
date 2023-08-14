using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    public static float score = 0f;
    
    public Text text;

    void Update() {
        text.text = score.ToString();
    }

}
