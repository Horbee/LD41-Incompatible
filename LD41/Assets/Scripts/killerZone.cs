using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killerZone : MonoBehaviour {

    public GameObject cannonLeft;
    public GameObject cannonRight;


    playerController pc;
    // Use this for initialization
    void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pc.respawnPlayer();
            return;
        }


        if (other.tag == "Ball")
        {
            other.transform.position = new Vector3(0, 5.66f, 0);
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.gravityScale = 0;

            rb.Sleep();

           // Debug.Log(rb.IsSleeping());
            
            return;
        }
        if (other.tag == "Bullet")
        {
            Destroy(other);
        }
            Destroy(other);

        
    }
}
