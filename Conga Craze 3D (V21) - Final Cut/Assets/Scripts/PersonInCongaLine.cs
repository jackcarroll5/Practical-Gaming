using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInCongaLine : MonoBehaviour {

    // Use this for initialization

    GameObject personInFrontOfMe;
    float positionVar = 0.05f;
    private float rotationVar = 0.1f;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Allows the people in the line behind the main person in front of the line to turn and move in conjunction with the front perosn in line 
        //so the people behind the main person can follow the front person
        transform.position = Vector3.Lerp(transform.position, personInFrontOfMe.transform.position, positionVar);
        transform.rotation = Quaternion.Slerp(transform.rotation, personInFrontOfMe.transform.rotation, rotationVar);
		
	}



    //Get the person to go behind the line when the gobstopper item is acquired which continues as long as the line doesn't hit
    //the line itself or the walls of the arena
    public void joinTheLineBehindMe(GameObject person)
    {
        personInFrontOfMe = person;
    }

}
