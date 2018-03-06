using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class FrontPersonInLine : MonoBehaviour
{
    private GameManagerCon manager;
    HighScoreManager desHighScore;

    private float speedCurrent = 15.0f;
    private float turnSpeed = 180.0f;
    private Vector3 newPos;
    public static int moveSpeed = 8;
    public Vector3 straight = Vector3.forward;
    public int maxClones = 0;

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

        changeCameraPos();

        isAlright(newPos);
	
	}

    private bool isAlright(Vector3 newPos)
    {
        return Physics.CheckCapsule(newPos,newPos,0.4f,23);
    }

    /// <summary>
    /// Allow the player to turn left
    /// </summary>
    private void turnLeft()
    {
        transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Allow the player to turn right
    /// </summary>
    private void turnRight()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Checks if the player is using the right button to turn left
    /// </summary>
    private bool mustTurnLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    /// <summary>
    /// Checks if the player is using the right button to turn right
    /// </summary>
    private bool mustTurnRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    /// <summary>
    /// Enables the player to automatically straight when the game begins
    /// </summary>
    private void moveStraight()
    {
        transform.Translate(straight * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Change the position of the camera as the player turns left or right.
    /// </summary>
    public void changeCameraPos()
    {
        Camera.main.transform.position = transform.position + 4.0f * Vector3.up - 6 * transform.forward;
        Vector3 focus = transform.position + 10 * transform.forward;
        Camera.main.transform.rotation = Quaternion.LookRotation((focus - Camera.main.transform.position).normalized);
    }

    /// <summary>
    /// Confirm that the player has spawned
    /// </summary>
    internal void confirmPlayer(GameManagerCon gameMan)
    {
        manager = gameMan;
    }

    public void OnCollisionEnter(Collision col)
    {
        transform.position -= speedCurrent * transform.forward * Time.deltaTime;

        manager = FindObjectOfType<GameManagerCon>();
       // desHighScore = FindObjectOfType<HighScoreManager>();

        //if(col.gameObject.GetComponent<WorldFormation>())
            FindObjectOfType<WorldFormation>();
           //if(col.gameObject.GetComponent<WorldFormation>().tag == "Wall")
            if(col.gameObject.tag == "Wall")
        {
            manager.destroyPerson(this);       
            Destroy(gameObject);
            manager.gameOver();
            
        }
    }
}
