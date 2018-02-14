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
    private int maxGobstoppers = 40;

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

    private void growUp()
    {
        growTimer += Time.deltaTime;
        transform.localScale = Mathf.Lerp(startScale, 1.5f, (growTimer / growTime)) * Vector3.one;
    }

    internal void ConfirmSweetIns(GameManagerCon.ItemType itemType, int score, float time)
    {       
        switch(itemType)
        {
            case GameManagerCon.ItemType.Gobstopper:
                part = Instantiate(gobstopper, transform.position, Quaternion.identity);
                break;
        }
        part.transform.parent = transform;

        this.points = score;
       
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<FrontPersonInLine>())
            {
            scoreMan = FindObjectOfType<ScoreManager>();
            scoreMan.increaseScore(pts);
        
           // managerCon.destroyItem(this);        
            Destroy(gameObject);        
            objSpawnInWorld = FindObjectOfType<WorldFormation>();
           

            Vector3 gobStopSpawn = new Vector3(UnityEngine.Random.Range(1, objSpawnInWorld.arenaWidth - 1), objSpawnInWorld.heightofArenaFloor + 1, UnityEngine.Random.Range(1,objSpawnInWorld.arenaDepth - 1));
            managerCon.itemSpawn(GameManagerCon.ItemType.Gobstopper,gobStopSpawn); 
            ConfirmSweetIns(GameManagerCon.ItemType.Gobstopper,1,10f);

            congaExtends = FindObjectOfType<FrontPersonInLine>();

                      for(int i = 0; i < congaExtends.maxClones; i++)
                      {
                      managerCon.playerClone.transform.localPosition = new Vector3(managerCon.playerClone.position.x,managerCon.playerClone.position.y,managerCon.playerClone.position.z - 4);
                       managerCon.playerLineSpawnAt(managerCon.playerClone.transform.localPosition);
                       congaExtends.changeCameraPos();
                      }

            gulp.Play();
        }
    }
}
