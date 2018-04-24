using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level3 : MonoBehaviour {
    playerController pc;
    // Use this for initialization
    void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(pc.myScore()  >= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
	}
}
