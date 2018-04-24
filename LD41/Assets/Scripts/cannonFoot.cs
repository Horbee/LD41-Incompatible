using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonFoot : MonoBehaviour {

    public int health = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void decreaseHealth()
    {
        health--;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
