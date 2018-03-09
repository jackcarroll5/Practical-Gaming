using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUpGob : MonoBehaviour
{

    private int points = 1;
    private int minPoints = 1;
    private int maxPoints = 3;
    private float growTime = 1.5f, growTimer = 0f;
    private float startScale = 0.0f;
    private float rotateSpeed = 360.0f;

    itemState currentState = itemState.grow;

    GameManagerCon managerCon;
    ScoreManager scoreMan;



    enum itemState { grow,destroy};

    Transform part;

    // Use this for initialization
    void Start () {
        growTimer = 0;
        transform.localScale = startScale * Vector3.one;
        managerCon = FindObjectOfType<GameManagerCon>();
        scoreMan = FindObjectOfType<ScoreManager>();

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        switch(currentState)//Switches state of gobstoppers when the item is collected. It has states of being destroyed and grows up when the other gobstopper is collected
        {
            case itemState.grow:
                growUp();

                break;

            case itemState.destroy:
                managerCon.destroyItem(this);
                Destroy(gameObject);

                break;
        }
		
	}

    public int pts//Sets up the points system. Generally increases the score by one
    {
        get
        {
          return points;//Returns one point for every gobstopper collected.
        }
        set
        {
            if(value >= maxPoints)
            {
                points = maxPoints;
            }
            else if(value <= minPoints)
            {
                points = minPoints;
            }
        }
    }

    /// <summary>
    /// Method that allows the gobstopper to grow at the start of the game and every time a gobstopper spawns.
    /// </summary>
    private void growUp()
    {
        growTimer += Time.deltaTime;
        transform.localScale = Mathf.Lerp(startScale, 1.5f, (growTimer / growTime)) * Vector3.one;
    }

    /// <summary>
    /// Confirms that the gobstopper was collected when the player picks up the gobstopper
    /// </summary>
    

    internal void IAm (GameManagerCon gameManager)
    {
        managerCon = gameManager;
    }

    /// <summary>
    /// All the events that occur once a gobstopper is colllected by the player (Line)
    /// </summary>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<FrontPersonInLine>())
            //if (col.gameObject.tag == "Player")
            {
            managerCon = FindObjectOfType<GameManagerCon>();
            //scoreMan.increaseScore(pts); 
            managerCon.destroyItem(this);  //Communicates with the manager to confirm that the item is destroyed      
            Destroy(gameObject);
        }
    }
}
