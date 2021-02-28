using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileForward : MonoBehaviour
{
    public float distBack;
    public float distForward = 3f;
    float fullForward;
    void Start()
    {
        fullForward = distForward * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - distBack)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + fullForward, transform.position.z);
        }
    }
}
