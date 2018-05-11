using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDiscoBall : MonoBehaviour {

    public float spinSpeed = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //Get the disco ball to spin around the arena to create an energetic atmosphere
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
		
	}
}
