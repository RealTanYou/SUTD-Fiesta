using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerPoints : MonoBehaviour
{
    public static PlayerPoints points;
    public static List<int> playerPoints = new List<int>{0,0,0,0};
    public static int player1Points;
    public static int player2Points;
    public static int player3Points;
    public static int player4Points;
    public TextMeshProUGUI canvastext;
    public static List<int> scoreList;

    // float timeCount;
    // public float delayTime = 2.0f;

    void Awake()
    {
        // // Pseudo-Singleton design pattern
        // if(points == null)
        // {
        //     DontDestroyOnLoad(gameObject);
        //     points = this;
        // }
        // else if(points != this)
        // {
        //     Destroy(gameObject);
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Contains("Book"))
        {
            scoreList = canvasscript.playerscore;

            playerPoints[0] = playerPoints[0] + scoreList[0];
            playerPoints[1] = playerPoints[1] + scoreList[1];
            playerPoints[2] = playerPoints[2] + scoreList[2];
            playerPoints[3] = playerPoints[3] + scoreList[3];
        }
        else if(SceneManager.GetActiveScene().name.Contains("AfterFablab"))
        {
            scoreList = ItemManager.danplayerscores;

            playerPoints[0] = playerPoints[0] + scoreList[0];
            playerPoints[1] = playerPoints[1] + scoreList[1];
            playerPoints[2] = playerPoints[2] + scoreList[2];
            playerPoints[3] = playerPoints[3] + scoreList[3];
        }
        else if(SceneManager.GetActiveScene().name.Contains("Canteen"))
        {
            scoreList = Finish.TWplayerscore;

            playerPoints[0] = playerPoints[0] + scoreList[0];
            playerPoints[1] = playerPoints[1] + scoreList[1];
            playerPoints[2] = playerPoints[2] + scoreList[2];
            playerPoints[3] = playerPoints[3] + scoreList[3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        string text = "Current Scores:\n" + "\n";

        // This is to show old score + new score
        // text += 
        // "Player 1:\t" + playerPoints[0] + " + " + scoreList[0] + "\n" +
        // "Player 2:\t" + playerPoints[1] + " + " + scoreList[1] + "\n" +
        // "Player 3:\t" + playerPoints[2] + " + " + scoreList[2] + "\n" +
        // "Player 4:\t" + playerPoints[3] + " + " + scoreList[3];

        // StartCoroutine(PlsWait());

        // This is for scores that have been calculated.
        text += 
        "Player 1:\t\t" + playerPoints[0] + "\n" +
        "Player 2:\t\t" + playerPoints[1] + "\n" +
        "Player 3:\t\t" + playerPoints[2] + "\n" +
        "Player 4:\t\t" + playerPoints[3];

        canvastext.text = text;
        canvastext.fontSize = 50;
        canvastext.alignment = TextAlignmentOptions.BaselineLeft;
    }

    // IEnumerator PlsWait()
    // {
    //     yield return new WaitForSeconds(3);
    // }
}
