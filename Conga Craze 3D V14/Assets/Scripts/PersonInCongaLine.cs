using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInCongaLine : MonoBehaviour {

    // Use this for initialization

    GameObject personInFrontOfMe;
    float positionVar = 0.1f;
    private float rotationVar = 0.1f;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(transform.position, personInFrontOfMe.transform.position, positionVar);
        transform.rotation = Quaternion.Slerp(transform.rotation, personInFrontOfMe.transform.rotation, rotationVar);


		
	}


    public void joinTheLineBehindMe(GameObject person)
    {

        personInFrontOfMe = person;
    }

}
