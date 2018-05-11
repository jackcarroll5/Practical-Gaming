using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Switch the scene to the main game's scene
        SceneManager.LoadScene("Gobstopper Collecting Test");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
