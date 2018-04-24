using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCannon : MonoBehaviour {

    public bool spawnLeft = true;
    public GameObject cannonStart;
    public GameObject cannonLeft;
    public GameObject cannonRight;

    playerController pc;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CannonBaseLeft")
        {
            Debug.Log("LEFT");

            if(spawnLeft)
            {
                if(cannonStart != null)
                {
                    Destroy(cannonStart);
                }
                cannonStart = Instantiate(cannonLeft, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                if (pc.getInCannon()) pc.getOutOfCannon();
            }

        }
        else if (other.tag == "CannonBaseRight")
        {
            Debug.Log("RIGHT");

            if (!spawnLeft)
            {
                if (cannonStart != null)
                {
                    Destroy(cannonStart);
                }
                cannonStart = Instantiate(cannonRight, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                if(pc.getInCannon()) pc.getOutOfCannon();
            }

        }


    }
}
