using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostLevelManager : MonoBehaviour
{

    public float minBoostPosition = -1.02f;
    public Transform boostBar;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    { 
        playerManager playerManagerScript = player.GetComponent<playerManager>();
        float boostAmount = playerManagerScript.boostAmount;
        float maxBoost = playerManagerScript.maxBoost;
        float boostBarOffset = minBoostPosition * (1-boostAmount/maxBoost);
        Vector3 newPos = new Vector3(boostBar.position.x, boostBar.position.y + boostBarOffset, transform.position.z);
        transform.position = newPos;
    }
}
