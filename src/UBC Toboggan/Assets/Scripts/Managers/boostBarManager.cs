using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostBarManager : MonoBehaviour
{

    public Transform cam;
    public float yOffset = 0f;
    public float xOffset = 0f;

    void Start()
    {
        UIManager.Instance.boostBarManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(cam.position.x + xOffset, cam.position.y + yOffset, 0f);
        transform.position = newPos;
    }

    public void ToggleBar(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
