using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

    public GameObject enemyBullet;
    public ParticleSystem enemyRespawnFX;

    public Transform[] enemySpawnPoints;
    public Transform[] cannonSpawnPoints;
    public Transform[] cannonShootPoints;

    GameObject player;
    playerController pc;
    Transform targetPos;
    Transform target;

    int fireRate = 10;
    int playerDamage = 2;
    int health = 5;
    int score = 0;
    int whatToDo = 0;
    bool isDead = false;
    int waitingTime = 0;
    // 0 is shoot yourself out form a random cannon 
    // 1 is shooting at the player
    // 3 is shooting out the foot of the cannon if the player sits in it
    // 4 tries to block the ball
    // 5 dead

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();
        target = player.transform;

        InvokeRepeating("considerWhatToDo", 2, 5);
    }
	
	// Update is called once per frame
	void FixedUpdate () {



		if(whatToDo == 1)
        {
            float dist = float.MaxValue;
            int theID = 0;
            // which point is closer to the player
            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                float currentDist = Vector3.Distance(enemySpawnPoints[i].transform.position, player.transform.position);
                if (currentDist < dist)
                {
                    dist = currentDist;
                    theID = i;
                }
            }
            targetPos = enemySpawnPoints[theID];
            transform.position = targetPos.position;
            enemyRespawnFX.Stop();
            enemyRespawnFX.transform.position = targetPos.position;
            enemyRespawnFX.Play();

            ShootAtPlayer();
        }
        else if (whatToDo == 3)
        {
            float dist = float.MaxValue;
            int theID = 0;
            // which point is closer to the player
            for(int i = 0; i < cannonSpawnPoints.Length; i++)
            {
                float currentDist = Vector3.Distance(cannonSpawnPoints[i].transform.position, player.transform.position);
                if (currentDist < dist)
                {
                    dist = currentDist;
                    theID = i;
                }
            }
            targetPos = cannonSpawnPoints[theID];
            transform.position = targetPos.position;
            enemyRespawnFX.Stop();
            enemyRespawnFX.transform.position = targetPos.position;
            enemyRespawnFX.Play();

            shootAtCannon();
        }
        else if (whatToDo == 5)
        {
            transform.position = new Vector2(-10000, -10000);
            if(waitingTime > 4)
            {
                isDead = false;
                health = 5;
            }
        }

        if (health <= 0) isDead = true;
	}

    void considerWhatToDo()
    {
        //Debug.Log("methode called");
        if(pc.getInCannon())
        {
            whatToDo = 3;
            return;
        }
        else if (isDead)
        {
            whatToDo = 5;
            waitingTime++;
        }
        else
        {
            whatToDo = 1;
            return;
        }
    }


    void shootAtCannon()
    {
        if (fireRate > 0)
        {
            fireRate--;
            return;
        }

        if(fireRate <= 0) {

            float dist = float.MaxValue;
            int theID = 0;
            // which point is closer to the player
            for (int i = 0; i < cannonShootPoints.Length; i++)
            {
                float currentDist = Vector3.Distance(cannonShootPoints[i].transform.position, transform.position);
                if (currentDist < dist)
                {
                    dist = currentDist;
                    theID = i;
                }
            }

            target = cannonShootPoints[theID];

            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            direction.Normalize();

            GameObject bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Rigidbody2D body = bulletInstance.GetComponent<Rigidbody2D>();
            body.velocity = direction * 30;


            fireRate = 30;
        }
    }

    void ShootAtPlayer()
    {
        if (fireRate > 0)
        {
            fireRate--;
            return;
        }

        if (fireRate <= 0)
        {
        Debug.Log("shooting");
            target = player.transform;

            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            direction.Normalize();

            GameObject bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Rigidbody2D body = bulletInstance.GetComponent<Rigidbody2D>();
            body.velocity = direction * 30;

            fireRate = 30;
        }

    }

    public void dealDmg()
    {
        health--;
    }

}
