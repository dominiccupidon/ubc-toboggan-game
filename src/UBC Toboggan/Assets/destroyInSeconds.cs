using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyInSeconds : MonoBehaviour
{
    public float secondsToDestroy = 1f;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, secondsToDestroy);
        player = GameObject.FindWithTag("Player");
    }

    void Update() {
        transform.position = player.transform.position;
    }
}
