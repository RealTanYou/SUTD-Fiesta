using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PodiumArrangement : MonoBehaviour
{
    public GameObject pos1, pos2, pos3, pos4;

    public Sprite[] robotList = new Sprite[4];

    public Button HomeButton;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<int,int> playerwinorder = new Dictionary<int,int>();
        int i = 0;
        
        foreach(int score in PlayerPoints.playerPoints){
            playerwinorder[i] = score;
            i++;
        }

        var result = playerwinorder.OrderByDescending(s => s.Value).ToArray();
        
        //int highestscore = 4;
        //foreach(KeyValuePair<int,int> item in result){
        //    danplayerscores[item.Key] = highestscore;
        //    highestscore--;
        //}
        pos1.GetComponent<SpriteRenderer>().sprite = robotList[result[0].Key];
        pos2.GetComponent<SpriteRenderer>().sprite = robotList[result[1].Key];
        pos3.GetComponent<SpriteRenderer>().sprite = robotList[result[2].Key];
        pos4.GetComponent<SpriteRenderer>().sprite = robotList[result[3].Key];

        Button btn = HomeButton.GetComponent<Button>();

		btn.onClick.AddListener(TaskOnClick);
        btn.Select();
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneName: "Scenes/MainMenu");
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
