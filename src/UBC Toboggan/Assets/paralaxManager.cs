using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxManager : MonoBehaviour
{

    public float paralaxFactor = 1f;
    public Transform cam;
    public float resetDistance = 19.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        // if (paralaxOffset >= resetDistance) {
        //     float diff = paralaxOffset - resetDistance;
        //     paralaxOffset -= (diff + 1);
        // }

        // if (paralaxOffset < 0) {
        //     float diff = 0 - paralaxOffset;
        //     diff = resetDistance > (diff + 1) ? resetDistance : diff + 1;
        //     paralaxOffset += diff;
        // }

        Vector3 newPos = new Vector3(cam.position.x - paralaxOffset, cam.position.y,transform.position.z);
        transform.position = newPos;
    }
}
