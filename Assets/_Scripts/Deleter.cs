using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    public string[] ignoredTags;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool ignored = false;
        foreach(string tag in ignoredTags)
        {
            if (tag == collision.gameObject.tag) { ignored = true; }
        }
        if (!ignored)
        {
            print("Deleter is destroying: " + collision.gameObject.name);
            Destroy(collision.gameObject);
        }
        // Need to add some list of things that are not deleted by the deleter. 
    }
}
