using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxManager : MonoBehaviour
{

    public float paralaxFactor = 1f;
    public Transform cam;
    public float resetDistance = 19.2f;

    // Update is called once per frame
    void Update()
    {
        float paralaxOffset = cam.position.x*paralaxFactor;

        while (paralaxOffset >= resetDistance) {
            paralaxOffset -= resetDistance;
        }

        while (paralaxOffset < 0) {
            paralaxOffset += resetDistance;
        }

        Vector3 newPos = new Vector3(cam.position.x - paralaxOffset, cam.position.y,transform.position.z);
        transform.position = newPos;
    }
}
