using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {

    public Transform bulletTransform;
    public Transform baseTransform;
    public Transform cannonBaseTransform;

    float radius = 3f;

    playerController pc;
    bool playerInCannon = false;

	// Use this for initialization
	void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        //Debug.Log(colliders.Length);

        if (Input.GetKeyDown(KeyCode.E) )
        {
            if(!playerInCannon)
            {
                foreach (Collider2D coll in colliders)
                {
                    Rigidbody2D rb = coll.GetComponent<Rigidbody2D>();
                    if (rb != null && rb.tag == "Player")
                    {
                        playerInCannon = true;
                    }
                }
            }
            else
            {
                pc.getOutOfCannon();
                playerInCannon = false;
            }   
        }

        if ( playerInCannon)
        {
            pc.sitIntoCannon(bulletTransform);

            float v = Input.GetAxis("Vertical");

            Vector3 angles = baseTransform.localEulerAngles;
            angles.z += v * 5f;
            angles.z = Mathf.Clamp((angles.z <= 180) ? angles.z : -(360 - angles.z), -60, 30);

            baseTransform.localEulerAngles = angles;

            if (Input.GetButton("Fire1"))
            {
                playerInCannon = false;
                pc.shootOut(Mathf.Sign(cannonBaseTransform.localScale.y));
            }
           
        }

    }

    float ClampAngle(float angle, float from, float to)
    {
        if (angle > 180) angle = 360 - angle;
        angle = Mathf.Clamp(angle, from, to);
        if (angle < 0) angle = 360 + angle;
        return angle;
    }
}
