using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, 5.66f, 0);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.gravityScale = 0;

            rb.Sleep();
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
