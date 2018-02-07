using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrontPersonInLine : MonoBehaviour
{
    private GameManagerCon manager;
    private float speedCurrent = 15.0f;
    private float turnSpeed = 180.0f;
    private Vector3 newPos;
    public static int moveSpeed = 8;
    public Vector3 straight = Vector3.forward;

    enum playerState { Move,Collect,Collapse}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        newPos = transform.position;

        if (mustTurnLeft())
            turnLeft();

        if (mustTurnRight())
            turnRight();

        moveStraight();
	
	}

   /* private bool isAlright(Vector3 newPos)
    {
        return Physics.CheckCapsule(newPos, 0.4f);
    }*/
   private void turnLeft()
    {
       transform.Rotate(Vector3.up,-turnSpeed * Time.deltaTime);
    }

   private void turnRight()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    private bool mustTurnLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

   private bool mustTurnRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private void moveStraight()
    {
        transform.Translate(straight * moveSpeed * Time.deltaTime);
    }

    private void changeCameraPos()
    {
        Camera.main.transform.position = transform.position + 2.0f * Vector3.up - 3 * transform.forward;
        Vector3 focus = transform.position + 5 * transform.forward;
        Camera.main.transform.rotation = Quaternion.LookRotation((focus - Camera.main.transform.position).normalized);
    }

    internal void confirmPlayer(GameManagerCon gameMan)
    {
        manager = gameMan;
    }

    public void OnCollisionEnter(Collision col)
    {
        transform.position -= speedCurrent * transform.forward * Time.deltaTime;
        if(col.gameObject.GetComponent<WorldFormation>())
        {
            Destroy(gameObject);
        }
    }
}
