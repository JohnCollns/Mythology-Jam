using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehav : MonoBehaviour
{
    bool leftSide;
    bool falling = false;
    public float fallRange;
    public string playerName = "scratch boat";
    public float fallRate;
    float totalAngle = 0;
    GameObject player;
    void Start()
    {
        leftSide = (transform.position.x > 0);
        player = GameObject.Find(playerName);
        if (!leftSide)
            fallRate *= -1;
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.y - player.transform.position.y) <= fallRange)
        {
            falling = true;
        }
        if (falling)
        {
            if (transform.childCount == 0)
                falling = false;
            else
            {
                totalAngle += fallRate;
                transform.GetChild(0).RotateAround(transform.position, Vector3.forward, fallRate);
                if (Mathf.Abs(totalAngle) >= 90f)
                    falling = false;
            }
        }
    }
    private void Update()
    {
        
    }
}
