using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool travelFowards = true;
    public bool rightwardWind = true;
    bool overworld = true;
    short world = 0;
    public float windAcc;
    //public float forwardSpeedOver = 5f, forwardSpeedUnder = 7.5f;
    public float maxSideSpeed;
    public float[] forwardSpeed = { 5f, 7.5f };
    public float forwardAcc, forwardDeacc;
    public float minForwardSpeed = 2f;
    public float slowedTurnMultiplier = 1.4f;
    public float frictionConst;
    public float acceptableSpeedDif;

    public float damageValue;
    public float[] damageModifiers;

    Vector2 windForce;
    Vector2 normalForwardForce;
    Vector2 forwardForce, forwardSlow;

    ArrayList collidedObjects;
    ArrayList collidedObstacles;

    Rigidbody2D rb;
    BoxCollider2D col;

    public AudioSource AS;
    GameController gCont;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        windForce = new Vector2(windAcc, 0f);

        normalForwardForce = new Vector2(0, forwardSpeed[world]);
        forwardForce = new Vector2(0f, forwardAcc);
        forwardSlow = new Vector2(0f, -forwardDeacc);
        if (travelFowards)
            rb.velocity = normalForwardForce;
        gCont = GameObject.Find("Game Controller").GetComponent<GameController>();

        collidedObjects = new ArrayList();
        collidedObstacles = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float slowedRatio = (forwardSpeed[world] - rb.velocity.y) / (forwardSpeed[world] - minForwardSpeed); 
        //rb.AddForce((rightwardWind ? 1 : -1)*(windForce + slowedRatio * windForce));
        rb.AddForce((rightwardWind ? 1 : -1) * windForce);

        if (Mathf.Abs(rb.velocity.x) > maxSideSpeed)
        {
            rb.velocity = new Vector2((rb.velocity.x > 0 ? 1 : -1) * maxSideSpeed, rb.velocity.y);
            //print("Boat travelling at max sideways speed: " + maxSideSpeed);
        }
        if (rb.velocity.y < forwardSpeed[world])
        {
            rb.AddForce(forwardForce);
            print("Accelerating boat, y speed: " + rb.velocity.y);
        }
        if (rb.velocity.y > forwardSpeed[world])
        {
            // v1
            //rb.velocity = new Vector2(rb.velocity.x, forwardSpeed[world]);
            //Vector2 fricForce2D = -frictionConst * rb.velocity.normalized * rb.velocity.sqrMagnitude;
            //Vector2 fricForce = new Vector2(0f, fricForce2D.y);

            // v2
            //float fricAcc = -frictionConst * rb.velocity.y;
            //Vector2 fricForce = new Vector2(0, fricAcc);

            float speedDif = rb.velocity.y - forwardSpeed[world];
            if (speedDif < acceptableSpeedDif)
            {
                rb.velocity = new Vector2(rb.velocity.x, forwardSpeed[world]);
                print("Cutting boat speed down to: " + forwardSpeed[world]);
            }
            else
            {
                float fricAcc = -frictionConst * rb.velocity.y;
                Vector2 fricForce = new Vector2(0, fricAcc);
                rb.AddForce(fricForce);
                print("Boat has exceeded its world speed, slowing down by: " + fricForce);
            }

            
        }
        
    }

    public void InvertWind() { rightwardWind = !rightwardWind; }
    public void SetWindDirection(bool newDir) { rightwardWind = newDir; }
    public bool GetRightWardWind() { return rightwardWind; }
    public void PlayerSlow()
    {
        rb.AddForce(forwardSlow);
        print("Slowing boat, y speed: " + rb.velocity.y);
        if (rb.velocity.y < minForwardSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, minForwardSpeed);
            print("Boat maximally deccelerated");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Boat has entered trigger: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Obstacle")
        {
            if (collidedObjects.Contains(collision.gameObject))
            {

            }
            else
            {
                Obstacle thisObstacle = collision.gameObject.GetComponent<Obstacle>();
                collidedObstacles.Add(thisObstacle);
                collidedObjects.Add(collision.gameObject);
                gCont.TakeDamage(thisObstacle.damage);
            }
        }
        if (AS)
        {
            GetComponent<AudioSource>().Play();
            print("Trigger sound working");
        }
        else
        {
            print("Trigger sound error");
        }
    }

    void OnCollisionEnter2D (Collision2D collisionInfo)
    {
    
        print("A collision has occured with: "+ collisionInfo.collider.name);
        {
            if (collisionInfo.gameObject.tag == "Obstacle")
            {
                if (collidedObjects.Contains(collisionInfo.gameObject)) 
                {

                }
                else
                {
                    Obstacle thisObstacle = collisionInfo.gameObject.GetComponent<Obstacle>();
                    collidedObstacles.Add(thisObstacle);
                    collidedObjects.Add(collisionInfo.gameObject);
                    gCont.TakeDamage(thisObstacle.damage);
                }
            }
                
            if (AS)
            {
                GetComponent<AudioSource>().Play();
                print("Collision sound working");
            }
            else
            {
                print("Collision sound not assigned");
            }


        }
        
        
    }

    public void SetRiver(int newRiver)
    {
        world = (short)newRiver;
        print("Setting river to: " + world);
        
        
    }
}
