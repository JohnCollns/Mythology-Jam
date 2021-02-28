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

    public float changeRiverCooldown = 2f;
    bool allowChangeRiver = true;

    PlayerController pCont;
    Light mainLight;
    public Color overworldColor, underworldColor;
    public float colorChangeTime = 0.6f;

    void Start()
    {
        curHealth = maxHealth;
        pCont = GameObject.Find("scratch boat").GetComponent<PlayerController>();
        fullHealthScale = healthbarObj.transform.localScale.x;
        StartCoroutine(HealthDrain());
        mainLight = GameObject.Find("Main Light").GetComponent<Light>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (allowChangeRiver)
            {
                ChangeRiver();
                StartCoroutine(RiverCooldown());
            }
        }

        float healthRatio = curHealth / maxHealth;
        healthbarObj.transform.localScale = new Vector3(fullHealthScale * healthRatio, 1f, 1f);
    }

    void ChangeRiver()
    {
        normalRiver = !normalRiver;
        pCont.SetRiver(normalRiver ? 0 : 1);
        //mainLight.color = (normalRiver ? overworldColor : underworldColor);
        Color targetColor = (normalRiver ? overworldColor : underworldColor);
        StartCoroutine(LerpLight(targetColor));
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

    IEnumerator RiverCooldown()
    {
        //normalRiver = !normalRiver;
        allowChangeRiver = false;

        yield return new WaitForSeconds(changeRiverCooldown);
        allowChangeRiver = true;
        
    }
    IEnumerator LerpLight(Color targetColor)
    {
        //print("Starting LerpLight");
        Color baseColor = mainLight.color;
        print("BaseColor: " + baseColor + ", targetColor: " + targetColor);
        //float startTime = Time.time;
        //float targetTime = startTime + colorChangeTime;
        //Color newColor = Color.Lerp(baseColor, targetColor, )
        print("Difference between mainLight.color and targetColor: " + Mathf.Abs(mainLight.color.g - targetColor.g));
        while (Mathf.Abs(mainLight.color.g - targetColor.g) > 0.05f)
        {
            Color newColor = Color.Lerp(mainLight.color, targetColor, colorChangeTime * Time.deltaTime);
            mainLight.color = newColor;
            print("Updating color of mainLight to: " + newColor);
            yield return new WaitForEndOfFrame();
        }
    }

    public bool GetRightWind() { return rightWind; }
}
