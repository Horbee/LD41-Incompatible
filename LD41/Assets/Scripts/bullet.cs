using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //resume 
        }
        else if (other.tag == "CannonFoot")
        {
            cannonFoot cf = other.GetComponent<cannonFoot>();
            cf.decreaseHealth();
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy")
        {
           // Debug.Log("enemy");
            enemyAI ea = other.GetComponent<enemyAI>();
            ea.dealDmg();
            Destroy(gameObject);
        }
    }
}
