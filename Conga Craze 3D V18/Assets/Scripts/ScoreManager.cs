using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// Current Score of the game. Set to 0 at start
    /// </summary>
    public int currentScore = 0;
 
    /// <summary>
    /// Text of Score to be placed at top right corner of screen.
    /// </summary>
    TextMesh scoreText;

    // Use this for initialization
    void Start () {

       scoreText = GetComponent<TextMesh>();
        increaseScore(0);//Starts game at 0 points
        scoreText.transform.position = new Vector3(15,10,0);
        scoreText.transform.parent = Camera.main.transform;//Attaches score text to camera so it can be seen on screen.	
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
        /*Displays the score on the top right corner of the screen and sets up the font size.*/
        scoreText.text = "Score: " + currentScore;
        scoreText.characterSize = 0.035f;
        scoreText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        scoreText.fontSize = 230;
        //https://stackoverflow.com/questions/30873343/how-to-set-a-font-for-a-ui-text-in-unity-3d-programmatically
        /*Code for setting up a font for score text*/

        print(currentScore);
    }
}
