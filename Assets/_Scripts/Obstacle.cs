using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float damage;
    public float fullPushAcc;
    Rigidbody2D rb;

    void OnCollisionStay2D(Collision2D collision)   //OnCollisionStay2D
    {
        //Vector3 dist = transform.position - collision.transform.position;
        //float pushAcc = 
        if (collision.gameObject.tag == "Player")
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized;
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 pushForce = direction * fullPushAcc;
            otherRb.AddForce(pushForce, ForceMode2D.Impulse);

            print(name + " has collided with player, applying force: " + pushForce);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 pushForce = direction * fullPushAcc;
            otherRb.AddForce(pushForce, ForceMode2D.Impulse);

            print(name + " has collided with player, applying force: " + pushForce);
            if (rb != null)
            {
                rb.AddForce(-pushForce, ForceMode2D.Impulse);
            }
        }
    }
    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody2D>();
        }
        catch
        {
            rb = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
