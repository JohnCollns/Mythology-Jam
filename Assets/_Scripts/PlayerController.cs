using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool travelFowards = true;
    public bool rightwardWind = true;
    public float windAcc;
    public float maxSpeed;
    public float forwardSpeed = 5f;
    public float forwardAcc, forwardDeacc;
    public float damageValue;
    public float damgaeModifier1;
    public float damageModifier2;
    public float damageModifier3;

    Vector2 windForce;
    Vector2 normalForwardForce;
    Vector2 forwardForce, forwardSlow;

    Rigidbody2D rb;
    BoxCollider2D col;

    public AudioSource AS;
    GameController gCont;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        windForce = new Vector2(windAcc, 0f);

        normalForwardForce = new Vector2(0, forwardSpeed);
        forwardForce = new Vector2(0f, forwardAcc);
        forwardSlow = new Vector2(0f, -forwardDeacc);
        if (travelFowards)
            rb.velocity = normalForwardForce;
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
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2((rb.velocity.x > 0 ? 1 : -1) * maxSpeed, rb.velocity.y);
            print("Boat travelling at max speed");
        }
        if (rb.velocity.y < forwardSpeed)
        {
            rb.AddForce(forwardForce);

        }
        if (rb.velocity.y > forwardSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, forwardSpeed);
        }
        
    }

    public void InvertWind() {
        rightwardWind = !rightwardWind;
    }
    public void SetWindDirection(bool newDir) { rightwardWind = newDir; }
    public void PlayerSlow()
    {
        rb.AddForce(forwardSlow);
        print("Slowing boat, y speed: " + rb.velocity.y);
    }
    
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
