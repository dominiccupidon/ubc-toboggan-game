using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beerCollectible : MonoBehaviour
{

    public float amount = 1f;
    public float speedMultiplier = 1f;
    public float magnitudeMultiplier = 0.25f;
    bool collected = false;
    float timer = 0f;
    float yOffset = 0f;
    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update() {
        if (!collected) {
            yOffset = (Mathf.Sin(timer*amount*speedMultiplier)+1)*magnitudeMultiplier;
            Vector3 newPos = new Vector3(initialPos.x,initialPos.y + yOffset,initialPos.z);
            transform.position = newPos;
            timer += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {

        playerManager playerManager = collider.GetComponent<playerManager>();

        if (playerManager != null && !collected) {
            if (playerManager.collectBeer(amount)) {
                gameObject.SetActive(false);
                collected = true;
            }
        }
    }
}
