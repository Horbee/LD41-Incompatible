using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour {

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
            other.GetComponent<playerController>().getDamage();
        }
        else if (other.tag == "CannonFoot")
        {
            cannonFoot cf = other.GetComponent<cannonFoot>();
            cf.decreaseHealth();
        }
        //Destroy(gameObject);
    }
}
