using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public Transform[] respawners;
    public ParticleSystem respawnFX;

    public GameObject bullet;

    Animator anim;

    public Text healthText;
    public Text ammoText;
    public Text scoreText;

    public float maxSpeed = 8f;
    public float moveForce = 100f;
    public float jumpForce = 1000f;
    Rigidbody2D rb;

    bool grounded = false;
    bool jump = false;
    bool facingRight = true;
    bool inCannon = false;

    int ammo, health, score;

    public GameObject scoreSound;
    public GameObject dieSound;
    public GameObject hurtSound;
    public GameObject cannonSound;

    AudioSource shootSound;

    public Transform groundCheck;
    // Use this for initialization
    void Start () {
        shootSound = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        //respawnPlayer();

        ammo = 25;
        health = 100;
        score = 0;
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if(Input.GetButtonDown("Jump") && grounded ){
            jump = true;
        }

        shoot();
       
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        
        anim.SetFloat("Speed", Mathf.Abs(h));

        if(h != 0)
        {
            /*if (h * rb.velocity.x < maxSpeed)
                rb.AddForce(Vector2.right * h * moveForce);*/

            /*if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);*/

            rb.velocity = new Vector2( maxSpeed * h, rb.velocity.y);
        }

        if(h > 0 && !facingRight)
        {
            flip();
        }else if (h < 0 && facingRight)
        {
            flip();
        }

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }

        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        ammoText.text = "Ammo: " + ammo;

        die();
    }

    void shoot()
    {
        if (inCannon) return;

        if(Input.GetButtonDown("Fire1") && ammo > 0)
        {
            shootSound.Play();
            anim.SetTrigger("Shoot");

            Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = cursorInWorldPos - (Vector2) transform.position;
            direction.Normalize();

            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
            Rigidbody2D body = bulletInstance.GetComponent<Rigidbody2D>();
            body.velocity = direction * 30;

            ammo--;
            ammoText.text = "Ammo: " + ammo;
        }
    }

    public void sitIntoCannon(Transform cannonTransform)
    {
        rb.bodyType = RigidbodyType2D.Static;
        inCannon = true;
        //position = cannon pos
        transform.position = cannonTransform.position;
        transform.rotation = cannonTransform.rotation;
    }

    public void getOutOfCannon()
    {
        
            inCannon = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.eulerAngles = new Vector3();

            respawnPlayer();
        
    }

    public void flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void shootOut(float sign)
    {
        //Debug.Log("Player shoot out");
        cannonSound.GetComponent<AudioSource>().Play();
        inCannon = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(sign * groundCheck.up.normalized * 1500f * 2);
        transform.eulerAngles = new Vector3();
    }

    public void pickedUpHP() {
        health = 100;
    }
    public void pickedUpAmmo()
    {
        ammo = 25;
    }

    public void pickedUpShield() { Debug.Log("Shield"); }
    public void pickedUpUzi() { Debug.Log("Uzi"); }
    public void pickedUpBazooka() { Debug.Log("Bazooka"); }

    public void respawnPlayer() {

        int id = Random.Range(0, respawners.Length);
        transform.position = respawners[id].position;
        rb.velocity = new Vector2(0,0);
        respawnFX.transform.position = respawners[id].position;
        respawnFX.Play();


    }

    public void getDamage()
    {
        hurtSound.GetComponent<AudioSource>().Play();
        health -= 5;
        
    }

    public void getScore()
    {
        score++;
        scoreSound.GetComponent<AudioSource>().Play();


    }

    public bool getInCannon()
    {
        return inCannon;
    }

    void die()
    {
        if(health <= 0)
        {
            dieSound.GetComponent<AudioSource>().Play();
            respawnPlayer();
            score = 0;
            health = 100;
            ammo = 25;

            Debug.Log("dead");
        }

    }

    public int myScore()
    {
        return score;
    }

    /*void OnDrawGizmos()
    {
        DrawHelperAtCenter(transform.up, Color.green, 2f);

    }

    private void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * 1000;
        Gizmos.DrawLine(transform.position, destination);
    }*/
}
