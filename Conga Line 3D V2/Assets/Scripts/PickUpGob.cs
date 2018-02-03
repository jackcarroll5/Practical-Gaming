using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUpGob : MonoBehaviour
{
    public Transform part;

    private int points = 1;
    private int minPoints;
    private int maxPoints;
    private float growTime = 1.5f, growTimer = 0f;
    private float startScale = 0.0f;
    private float rotateSpeed = 270.0f;

    public Transform gobstopper;

    itemState currentState = itemState.grow;

    GameManagerCon managerCon;

    enum itemState { grow,destroy};

    // Use this for initialization
    void Start () {
        growTimer = 0;
        transform.localScale = startScale * Vector3.one;
        managerCon = FindObjectOfType<GameManagerCon>();
		
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
    }

    private void growUp()
    {
        growTimer += Time.deltaTime;
        transform.localScale = Mathf.Lerp(startScale, 1.5f, (growTimer - growTime)) * Vector3.one;
    }

    internal void ConfirmSweetIns(GameManagerCon.ItemType itemType, int score, float time)
    {
        Transform part;

        switch(itemType)
        {
            case GameManagerCon.ItemType.Gobstopper:
                part = Instantiate(gobstopper, transform.position, Quaternion.identity);
                break;
        }
        //part.transform.parent = transform;

        this.points = score;
       
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<FrontPersonInLine>())
            {
            

            managerCon.collectItem(this);
            Destroy(gameObject);
            }
    }
}
