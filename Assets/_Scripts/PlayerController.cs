using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool rightwardWind = true;
    public float windAcc;
    public float maxSpeed;

    Vector2 windForce;

    Rigidbody2D rb;
    BoxCollider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        windForce = new Vector2(windAcc, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        rb.AddForce((rightwardWind ? 1 : -1)*windForce);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = new Vector2((rb.velocity.x > 0 ? 1 : -1) * maxSpeed, rb.velocity.y);
            print("Boat travelling at max speed: ");
        }
    }

    public void InvertWind() {
        rightwardWind = !rightwardWind;
    }
    public void SetWindDirection(bool newDir) { rightwardWind = newDir; }
}
