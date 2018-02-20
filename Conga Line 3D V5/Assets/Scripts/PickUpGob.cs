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
    private int maxGobstoppers = 1;

    public Transform gobstopper;

    itemState currentState = itemState.grow;

    GameManagerCon managerCon;
    ScoreManager scoreMan;
    WorldFormation objSpawnInWorld;
    FrontPersonInLine congaExtends;

    enum itemState { grow,destroy};

    Transform part;

    public AudioSource gulp;

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

        switch(currentState)
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

    public int pts
    {
        get
        {
          return points;
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
    internal void ConfirmSweetIns(GameManagerCon.ItemType itemType, int score, float time)
    {
        switch (itemType)
        {
            case GameManagerCon.ItemType.Gobstopper:
                part = Instantiate(gobstopper, transform.position, Quaternion.identity);
                break;
        }
        part.transform.parent = transform;

        this.points = score;

    }

    /// <summary>
    /// All the events that occur once a gobstopper is colllected by the player (Line)
    /// </summary>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<FrontPersonInLine>())
        {
            //scoreMan.increaseScore(pts); 

            managerCon.destroyItem(this);        
            Destroy(gameObject);
        }
    }
}
