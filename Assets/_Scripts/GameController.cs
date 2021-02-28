using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float maxHealth = 60 * 2;
    float curHealth;
    public float drainFreq, drainDamage;
    public float constHealthCost;
    public float travelSpeed;

    public GameObject healthbarObj;
    float fullHealthScale;

    bool normalRiver = true;
    bool rightWind = true;

    PlayerController pCont;

    void Start()
    {
        curHealth = maxHealth;
        pCont = GameObject.Find("scratch boat").GetComponent<PlayerController>();
        fullHealthScale = healthbarObj.transform.localScale.x;
        StartCoroutine(HealthDrain());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G)) { curHealth -= 40f; }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pCont.SetWindDirection(rightWind);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pCont.SetWindDirection(!rightWind);
        }
        if (Input.GetKey(KeyCode.S)) { pCont.PlayerSlow(); }

        float healthRatio = curHealth / maxHealth;
        healthbarObj.transform.localScale = new Vector3(fullHealthScale * healthRatio, 1f, 1f);
    }
    public void TakeDamage(float damageAmt)
    {
        curHealth -= damageAmt;
        if (curHealth <= 0)
        {
            print("Health has fallen to 0, game over. (Not yet implemented)");
        }
    }
    public float GetHealth() { return curHealth; }

    IEnumerator HealthDrain()
    {
        //curHealth -= drainDamage;
        while (true) { 
            TakeDamage(drainDamage);
            //print("Taking damage over time");
            yield return new WaitForSeconds(drainFreq);
        }
    }

    public bool GetRightWind() { return rightWind; }
}
