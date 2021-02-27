using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool rightwardWind = true;
    public float windAcc;
    public float maxSpeed;
    public float damageValue;
    public float damgaeModifier1;
    public float damageModifier2;
    public float damageModifier3;
    //Add more public float damage modifiers if more obstacles are created

    Vector2 windForce;

    Rigidbody2D rb;
    BoxCollider2D col;

    public AudioSource AS;
    GameController gCont;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        windForce = new Vector2(windAcc, 0f);
        gCont = GameObject.Find("Game Controller").GetComponent<GameController>();
        gCont.GetHealth();
        gCont.curHealth--;
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
    
    void OnCollisionEnter (Collision collisionInfo)
    {
    
        {
            print("A collision has occured");
            UnityEngine.Debug.Log(collisionInfo.collider.name);
            {
                if (AS)
                {
                    GetComponent<AudioSource>().Play();
                    print("Collision sound working");
                }
                else
                {
                    print("Collision sound not assigned");
                }

                //if (collisionInfo.collider.name == "")
                // {
                //    curHealth = curHealth - damageValue * damageModifier1
                // }
                // elseIf (collisionInfo.collider.name == "")
                // { 
                //    curHealth = curHealth - damageValue * damageModifier2
                // }
                // elseIf (collisionInfo.collider.name == "")
                //{
                //    curHealth = curHealth - damageValue * damageModifier 3
                //}
                //etc. etc.
            }
        }
        
    }
}
