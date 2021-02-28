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

    ArrayList overworldObstacles, underworldObstacles;
    //ArrayList[] obstacles = new ArrayList[2];

    void Start()
    {
        curHealth = maxHealth;
        pCont = GameObject.Find("scratch boat").GetComponent<PlayerController>();
        fullHealthScale = healthbarObj.transform.localScale.x;
        StartCoroutine(HealthDrain());
        mainLight = GameObject.Find("Main Light").GetComponent<Light>();
        overworldObstacles = new ArrayList();
        underworldObstacles = new ArrayList();
        //obstacles = [overworldObstacles, overworldObstacles];
        FindObstacles();
        foreach (GameObject obj in underworldObstacles)
        {
            obj.SetActive(false);
        }
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
        Color prevColor = (normalRiver ? underworldColor : overworldColor);
        //StopCoroutine(LerpLight(prevColor));
        mainLight.color = prevColor;
        StartCoroutine(LerpLight(targetColor));

        foreach (GameObject obj in overworldObstacles)
        {
            obj.SetActive(normalRiver);
        }
        foreach (GameObject obj in underworldObstacles)
        {
            obj.SetActive(!normalRiver);
        }
    }

    void FindObstacles()
    {
        //overworldObstacles, underworldObstacles
        GameObject obParent = GameObject.Find("Obstacles");
        for (int i=0; i < obParent.transform.childCount; i++)
        {
            if (obParent.transform.GetChild(i).GetComponent<Obstacle>().world == 0)
                overworldObstacles.Add(obParent.transform.GetChild(i).gameObject);
            else
                underworldObstacles.Add(obParent.transform.GetChild(i).gameObject);


        }
        print("Obstacles found: " + obParent.transform.childCount + ", overworld: " + overworldObstacles.Count + ", underworld: " + underworldObstacles.Count);
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
        print("River change now legal");
    }
    IEnumerator LerpLight(Color targetColor)
    {
        //print("Starting LerpLight");
        Color baseColor = mainLight.color;
        //print("BaseColor: " + baseColor + ", targetColor: " + targetColor);
        //float startTime = Time.time;
        //float targetTime = startTime + colorChangeTime;
        //Color newColor = Color.Lerp(baseColor, targetColor, )
        //print("Difference between mainLight.color and targetColor: " + Mathf.Abs(mainLight.color.g - targetColor.g));
        while (Mathf.Abs(mainLight.color.g - targetColor.g) > 0.001f)
        {
            Color newColor = Color.Lerp(mainLight.color, targetColor, colorChangeTime * Time.deltaTime);
            mainLight.color = newColor;
            //print("Updating color of mainLight to: " + newColor);
            yield return new WaitForEndOfFrame();
        }
    }

    public bool GetRightWind() { return rightWind; }
}
