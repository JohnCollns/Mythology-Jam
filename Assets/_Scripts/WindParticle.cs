using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle : MonoBehaviour
{
    public float maxSpeed;
    public float windAcc;
    public float maxX;
    Vector3 standardPos;
    public float varianceY, timeScale, timeVarRange;
    float randTimeVariance;
    Vector2 windForce;
    Rigidbody2D rb;
    PlayerController pCont;
    Transform camTran;
    bool rightWind = true;
    public string playerObjName = "scratch boat";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        standardPos = transform.position;
        windForce = new Vector2(windAcc, 0f);
        pCont = GameObject.Find(playerObjName).GetComponent<PlayerController>();
        camTran = Camera.main.transform;
        randTimeVariance = Random.Range(0, timeVarRange);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rightWind != pCont.GetRightWardWind())
        {
            rightWind = pCont.GetRightWardWind();
            rb.AddForce(windForce * (rightWind ? 1f : -1f));
            //print("Wind particle changing direction.");
        }
        if (Mathf.Abs(rb.velocity.magnitude) < maxSpeed)
        {
            //bool rightWind = gCont.GetRightWind();
            rb.AddForce(windForce * (rightWind ? 1f : -1f));
        }
        transform.position = new Vector3(transform.position.x, camTran.position.y + standardPos.y + (varianceY * Mathf.Cos(randTimeVariance + timeScale * Time.time)), transform.position.z);

        if (Mathf.Abs(transform.position.x) > maxX) { transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z); }
    }
}
