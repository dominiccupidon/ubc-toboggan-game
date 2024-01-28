using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 1f;
    public float minX = 1f;

    public Transform target;

    void Start()
    {
        Vector3 newPos = new Vector3(Mathf.Max(target.position.x + xOffset, minX), target.position.y + yOffset,-10f);
        transform.position = newPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(Mathf.Max(target.position.x + xOffset, minX), target.position.y + yOffset,-10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed*Time.deltaTime);
    }
}
