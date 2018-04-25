using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {

    /// <summary>
    /// Current Score of the game. Set to 0 at start
    /// </summary>
    private int highscore = 0;
    private static HighScoreManager instanceRef;

    ScoreManager finalScore;

    /// <summary>
    /// Text of Score to be placed at top right corner of screen.
    /// </summary>
    TextMesh highScoreText;

    // Use this for initialization
    void Start()
    {
        print("Starting");
        highScoreText = GetComponent<TextMesh>();
         highScoreText.text = "High Score: " + highscore;
        highScoreText.transform.position = new Vector3(1.5f, 10, 62);
        //highScoreText.transform.parent = Camera.main.transform;//Attaches score text to camera so it can be seen on screen.
        //DontDestroyOnLoad(gameObject);


       
    }
    //https://answers.unity.com/questions/233284/objects-being-duplicated-with-dontdestroyonload.html
    //Destroy duplicated high score text when starting game

    private void Awake()
    {
        if(instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        
    }

    private void setHighScore()
    {
        finalScore = FindObjectOfType<ScoreManager>();

        if (finalScore) print("found score");

        if (finalScore.currentScore > highscore)
        {
            highscore = finalScore.currentScore;
            print("High Score is: " + highscore);
             highScoreText.text = "High Score: " + highscore;
        }
       
        highScoreText.characterSize = 0.035f;
        highScoreText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        highScoreText.fontSize = 230;

        print(highscore);
    }

    // Update is called once per frame
    void Update () {
        setHighScore();
        PlayerPrefs.SetInt("highscore", highscore);
    }
}
