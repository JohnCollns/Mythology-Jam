using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool rightwardWind = true;
    public float windForce;
    public AudioSource AS;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
}
