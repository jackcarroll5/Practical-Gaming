using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// Current Score of the game. Set to 0 at start
    /// </summary>
    private int currentScore = 0;
    /// <summary>
    /// Shows high score from game. Will carry over to restart of game each time until high score is beaten.
    /// </summary>
    private int highScore = 0;
    /// <summary>
    /// Text of Score to be placed at top right corner of screen.
    /// </summary>
    TextMesh scoreText;

    // Use this for initialization
    void Start () {

       scoreText = GetComponent<TextMesh>();
        increaseScore(0);
        scoreText.transform.position = new Vector3(5,4,0);
        scoreText.transform.parent = Camera.main.transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Adds up the score each time the gobstopper is collected
    /// </summary>
    public void increaseScore(int scoreAdd)
    {
        currentScore += scoreAdd;
        scoreText.text = "Score: " + currentScore;
        scoreText.characterSize = 0.04f;
        scoreText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        scoreText.fontSize = 250;
        //https://stackoverflow.com/questions/30873343/how-to-set-a-font-for-a-ui-text-in-unity-3d-programmatically
        /*Code for setting up a font for score text*/

        print(currentScore);
    }
}
