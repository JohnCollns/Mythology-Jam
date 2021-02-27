using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float maxHealth = 60 * 2;
    public float constHealthCost;
    public float travelSpeed;

    bool normalRiver = true;
    bool rightWind = true;

    PlayerController pCont;

    void Start()
    {
        pCont = GameObject.Find("scratch boat").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            pCont.SetWindDirection(rightWind);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pCont.SetWindDirection(!rightWind);
        }
    }
}
