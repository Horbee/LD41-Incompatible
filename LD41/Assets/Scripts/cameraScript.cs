using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    private Transform player;
    private playerController pc;
    public float smoothness;

    public Vector2 maxXandY;
    public Vector2 minXandY;

    Vector3 offset;
    float lowestY;
    float initialSize = 7.515685f;
    float zoomOutSize = 11.5f;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pc = player.GetComponent<playerController>();
        offset = transform.position - player.position;
       // lowestY = transform.position.y;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        Vector3 targetPos = player.position + offset;

        targetPos.x = Mathf.Clamp(targetPos.x, minXandY.x, maxXandY.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minXandY.y, maxXandY.y);

        if(pc.getInCannon())
        {
            Camera.main.orthographicSize = zoomOutSize;
        } else
        {
            Camera.main.orthographicSize = initialSize;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothness * Time.deltaTime);

    //    if (transform.position.y < lowestY)
      //      transform.position = new Vector3(transform.position.x, lowestY, transform.position.z);
    }
}
