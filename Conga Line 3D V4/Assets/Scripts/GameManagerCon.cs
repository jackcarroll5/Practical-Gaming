﻿using System.Collections;
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

    //private int noCollectedGobs;
    private int nostartingGobs = 1;


    // Use this for initialization
    void Start () {

        world = FindObjectOfType<WorldFormation>();

        players = new List<FrontPersonInLine>();
         items = new List<PickUpGob>();

        for (int i = 0; i < nostartingGobs; i++)
            spawnRanGob();


        Vector3 playerStartPos = world.randomEmptypos();
       // Instantiate(playerClone, playerStartPos + 0.3f * Vector3.up,Quaternion.identity);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void gameOver()
    {
        SceneManager.LoadScene(0);
    }


    public void spawnRanGob()
    {
        ItemType newItemType = (ItemType)UnityEngine.Random.Range(0, 1);
        Vector3 posSpawn = world.randomEmptypos();

        itemSpawn(newItemType, posSpawn);

    }

    public void playerSpawn()
    {
        //Vector3 posSpawn = new Vector3(1,1,1);
        Vector3 posSpawn1 = new Vector3(UnityEngine.Random.Range(1, world.arenaWidth - 1), world.heightofArenaFloor, UnityEngine.Random.Range(1, world.arenaDepth - 1));
        playerLineSpawnAt(posSpawn1);

    }

    public void destroyConga(FrontPersonInLine player)
    {
        throw new System.NotImplementedException();
    }

    public void destroyPerson(FrontPersonInLine player)
    {
        players.Remove(player);
    }

    public void destroyItem(PickUpGob item)
    {
        items.Remove(item);
    }

    public void itemSpawn(ItemType newItemType,Vector3 posSpawn)
    {

        Transform newItem = Instantiate(gobClone,posSpawn,Quaternion.identity);    
        PickUpGob newGobControl = newItem.GetComponent<PickUpGob>();
        newGobControl = FindObjectOfType<PickUpGob>();

        
        if (newGobControl)
        {
            print("FOUND SCRIPT");
            newGobControl.ConfirmSweetIns(newItemType,(1 + (int)newItemType), 10.0f);
            items.Add(newGobControl);
        }
        else print("no script");  
    }


    public void playerLineSpawnAt(Vector3 posToSpawn)
    {
        Transform newPlayer = Instantiate(playerClone, posToSpawn, Quaternion.identity);
        FrontPersonInLine newPlayerControl = newPlayer.GetComponent<FrontPersonInLine>();
        newPlayerControl.confirmPlayer(this);
        //newPlayer.transform.parent = playerClone;
        players.Add(newPlayerControl);
    }
}
