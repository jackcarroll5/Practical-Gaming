using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldFormation : MonoBehaviour
{
   public int arenaDepth = 90;
    public int arenaWidth = 70;
   public float heightofArenaFloor = 0.0f;

    public float speed = 0.5f;
    public Color startColour;
    public Color endColour;
    public bool repeat = false;
    float startTime;

    FrontPersonInLine control;

    public Transform arenaWallPrefab,danceFloorPrefab;
    public LayerMask mask;

    // Use this for initialization
    void Start () {
        createArena();	
	}
	
	// Update is called once per frame
	void Update () {
        mask = LayerMask.GetMask("Arena Wall");

        mask = ~mask;


        //Switch the colour very few seconds to illuminate the dance floor throughout the game
        if(!repeat)
        {
            float t = (Time.time - startTime) * speed;
           danceFloorPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(startColour, endColour, t);
        }
        else
        {
            float t = (Mathf.Sin(Time.time - startTime) * speed);
            danceFloorPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(startColour, endColour, t);
        }

    }

   
    void createDanceFloorAt(Vector3 v)
    {
       createDanceFloorAt(v);
    }

    /// <summary>
    /// Method to form the dance floor arena for the conga line to dance
    /// </summary>
    void createDanceFloorAt1(Vector3 v)
    {
        Transform newBoy = Instantiate(danceFloorPrefab, snapTo(v, v.y), Quaternion.identity);
        newBoy.parent = transform;
    }

    /// <summary>
    /// Spawns the player in a random position in the arena
    /// </summary>
    internal Vector3 randomEmptypos()
    {
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(1, arenaWidth - 1), heightofArenaFloor + 1, UnityEngine.Random.Range(1, arenaDepth - 1));

        if (emptyAt(spawnPos))
            return spawnPos;
        else
            return randomEmptypos();
    }

    internal bool emptyAt(Vector3 pos)
    {
        Collider[] objectInArea = Physics.OverlapSphere(pos, 0.5f);

        //Check for walls
        




        return objectInArea.Length == 0;
    }

    /// <summary>
    /// Method that snaps a wall block to other blocks where the instantiation of blocks can spread down across a line (X or Z axis)
    /// </summary>
    private Vector3 snapTo(Vector3 v, float height)
    {
        Vector3 s1 = snapTo(v);
        return new Vector3(s1.x, height, s1.z);
    }

    internal Vector3 snapTo(Vector3 v)
    {
        return new Vector3(Mathf.RoundToInt(v.x), heightofArenaFloor, Mathf.RoundToInt(v.z));
    }

    /// <summary>
    /// Determines the edges of the walls of the arena
    /// </summary>
    private bool onTheEdge(int x, int z)
    {
        return (x == 0) || (z == 0) || (x == (arenaWidth - 1)) || (z == (arenaDepth - 1));
    }

    /// <summary>
    /// Forms the walls for the arena to create the boundaries for the conga line to avoid because if it hits the wall, it's game over.
    /// </summary>
    public void createWallAt(Vector3 v)
    {
        Transform nkotb = Instantiate(arenaWallPrefab, snapTo(v), Quaternion.identity);
        nkotb.parent = transform;
    }

    /// <summary>
    /// Main method for forming the entire arena with its walls.
    /// </summary>
   private void createArena()
    {
        for (int x = 0; x < arenaWidth; x++)
        {
            for (int z = 0; z < arenaDepth; z++)
            {
                createDanceFloorAt1(new Vector3(x, -1, z));

                if (onTheEdge(x, z))
                    createWallAt(new Vector3(x, 0, z));
            }
        }
    }
}
