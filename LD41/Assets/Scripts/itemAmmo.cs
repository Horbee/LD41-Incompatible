using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAmmo : MonoBehaviour {

    playerController pc;
    // Use this for initialization
    void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pc.pickedUpAmmo();
        }
    }
}
