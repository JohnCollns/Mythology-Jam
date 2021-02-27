using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool rightwardWind = true;
<<<<<<< HEAD
    public float windAcc;
    public float maxSpeed;

    Vector2 windForce;

    Rigidbody2D rb;
    BoxCollider2D col;

=======
    public float windForce;
    public AudioSource AS;
>>>>>>> 57cc9c1e2e77689ea1c96b0279f7b889a07d1b37
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
<<<<<<< HEAD

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
=======
    
    void OnCollisionEnter (Collision collisionInfo)
    {
    
        {
            UnityEnine.Debug.Log("A collision has occured");
            UnityEngine.Debug.Log(collisionInfo.collider.name);
            {
                if (AS)
                {
                    GetComponent<AudioSource>().Play();
                    UnityEngine.Debug.Log("Collision sound working");
                }
                else
                {
                    UnityEngine.Debug.Log("Collision sound not assigned")
                }
                
                
            }
        }
        
    }
>>>>>>> 57cc9c1e2e77689ea1c96b0279f7b889a07d1b37
}
