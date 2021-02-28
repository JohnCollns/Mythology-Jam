using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour
{
    public float frictionConst;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector2 fricForce = frictionConst * -(rb.velocity * rb.velocity);
        Vector2 fricForce = -frictionConst * rb.velocity.normalized * rb.velocity.sqrMagnitude;
        rb.AddForce(fricForce, ForceMode2D.Force);
    }
}
