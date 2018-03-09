using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManagerCon : MonoBehaviour {

    public enum ItemType { Gobstopper }

    public Transform gobClone;
    public Transform playerClone;

    private List<FrontPersonInLine> players;
    private List<PickUpGob> items;

    WorldFormation world;
    FrontPersonInLine congaExtends;
    ScoreManager score;
    PickUpGob sweet;

    //private int noCollectedGobs;
    private int nostartingGobs = 1;
    private int points = 1;
    private int minPoints = 1;
    private int maxPoints = 65;

    public AudioSource gulp;

    public Color[] colours = new Color[5];


    // Use this for initialization
    void Start () {

        world = FindObjectOfType<WorldFormation>();

        players = new List<FrontPersonInLine>();
         items = new List<PickUpGob>();

        gulp = GetComponent<AudioSource>();

        /* for (int i = 0; i < nostartingGobs; i++)
             spawnRanGob();*/

        

        //playerSpawn();
        Vector3 playerStartPos = world.randomEmptypos();
        //Instantiate(playerClone, playerStartPos + 0.3f * Vector3.up,Quaternion.identity);

        /*Vector3 itemPos = world.randomEmptypos();
           theItemSpawn(itemPos);*/
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Method that triggers the end game if the player(Line) hits the wall or the line itself
    /// </summary>
    public void gameOver()
    {
        SceneManager.LoadScene("SetStart");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Intended to spawn the player somewhere within the arena.
    /// </summary>
    public void playerSpawn()
    {
        //Vector3 posSpawn = new Vector3(1,1,1);
        Vector3 posSpawn1 = new Vector3(UnityEngine.Random.Range(1, world.arenaWidth - 1), world.heightofArenaFloor, UnityEngine.Random.Range(1, world.arenaDepth - 1));
        playerLineSpawnAt(posSpawn1);

    }

    /// <summary>
    /// Method for destroying the conga line if front person hits the wall or the line itself
    /// </summary>
    public void destroyConga(FrontPersonInLine player)
    {
       
    }

    /// <summary>
    /// Method to destroy the solo player if it hits the wall
    /// </summary>
    public void destroyPerson(FrontPersonInLine player)
    {
        players.Remove(player);
    }

    /// <summary>
    /// Method to destroy the gobstopper item
    /// </summary>
    public void destroyItem(PickUpGob item)
    {
        world = FindObjectOfType<WorldFormation>();

        congaExtends = FindObjectOfType<FrontPersonInLine>();

        sweet = FindObjectOfType<PickUpGob>(); 

        items.Remove(item);

        addScore(); 

        cloneNewPerson();

        gobCloneSpawn();

        gulp.Play(); //Creates the sound effect for gulping when the gobstopper is collected

        // spawnRanGob();         
    }

 /// <summary>
    /// Spawns gobstopper in a random section of the arena
    /// </summary>
    public void spawnRanGob()
    {
        ItemType newItemType = (ItemType)UnityEngine.Random.Range(0, 1);
        Vector3 posSpawn = world.randomEmptypos();

        itemSpawn(newItemType, posSpawn);

        sweet.IAm(this);

    }
    public void gobCloneSpawn()
    {
        //for(int i = 0; i < maxGobstoppers; i++)
        //{

        Vector3 gobStopSpawn = new Vector3(UnityEngine.Random.Range(1, world.arenaWidth - 1), world.heightofArenaFloor + 1, UnityEngine.Random.Range(1, world.arenaDepth - 1));
        //itemSpawn(ItemType.Gobstopper, gobStopSpawn);
       // itemSpawn(newItemType, gobStopSpawn);
        theItemSpawn(gobStopSpawn);
       //sweet.ConfirmSweetIns(ItemType.Gobstopper, 1, 10f);
        sweet.IAm(this);
        //}

       
    }

    public void cloneNewPerson()
    {      
          
            //playerClone.transform.position = new Vector3(playerClone.position.x, playerClone.position.y, playerClone.position.z - 4);
            playerLineSpawnAt(playerClone.transform.position);
            congaExtends.changeCameraPos();
    }
   
    public void addScore()
    {
         points = sweet.pts;
        score = FindObjectOfType<ScoreManager>();
        score.increaseScore(points);//Communicates with the ScoreManager to increase the points once the gobstopper collected.

    }


    public int pts
    {
        get
        {
            return points;
        }
        set
        {
            if (value >= maxPoints)
            {
                points = maxPoints;
            }
            else if (value <= minPoints)
            {
                points = minPoints;
            }
        }
    }


    /// <summary>
    /// Method to spawn the gobstopper within the arena.
    /// </summary>
    public void itemSpawn(ItemType newItemType, Vector3 posSpawn)
    {

        Transform newItem = Instantiate(gobClone, posSpawn, Quaternion.identity);
        PickUpGob newGobControl = newItem.GetComponent<PickUpGob>();
        newGobControl = FindObjectOfType<PickUpGob>();


        if (newGobControl)
        {
            print("FOUND SCRIPT");            
            items.Add(newGobControl);
        }
        else print("No script");
    }

    public void theItemSpawn(Vector3 posSpawn)
    {

        Transform newItem = Instantiate(gobClone, posSpawn, Quaternion.identity);
        PickUpGob newGobControl = newItem.GetComponent<PickUpGob>();
        newGobControl = FindObjectOfType<PickUpGob>();

        newGobControl.IAm(this);
        items.Add(newGobControl);
    }


    /// <summary>
    /// Possible method to spawn the player clone in order to make the player a parent of the front person in line
    /// </summary>
    public void playerLineSpawnAt(Vector3 posToSpawn)
    {
        Transform newPlayer = Instantiate(playerClone, posToSpawn, Quaternion.identity);
        FrontPersonInLine newPlayerControl = newPlayer.GetComponent<FrontPersonInLine>();
        newPlayerControl.confirmPlayer(this);

        newPlayerControl = FindObjectOfType<FrontPersonInLine>();
        newPlayer.gameObject.tag = "Clone";
        newPlayer.transform.parent = playerClone.transform;




        newPlayer.GetComponent<FrontPersonInLine>().enabled = false;      
        players.Add(newPlayerControl);
    }
}
