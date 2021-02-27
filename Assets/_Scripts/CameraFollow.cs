using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerObj;
    float yDis;
    Vector3 displacement;
    void Start()
    {
        yDis = transform.position.y - playerObj.transform.position.y;
        displacement = new Vector3(0, yDis, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = playerObj.transform.position + displacement;
        transform.position = new Vector3(0, playerObj.transform.position.y + yDis, transform.position.z);
    }
}
