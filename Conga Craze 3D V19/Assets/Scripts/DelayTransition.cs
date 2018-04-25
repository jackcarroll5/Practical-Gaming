using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayTransition : MonoBehaviour
{
    float timeForGameOver = 4.0f;

	// Use this for initialization
	void Start () {

       
		
	}
	
	// Update is called once per frame
	void Update () {

        timeForGameOver -= Time.deltaTime;

        if (timeForGameOver <= 0)
        {
            SceneManager.LoadScene("SetStart");
        }

    }
}
