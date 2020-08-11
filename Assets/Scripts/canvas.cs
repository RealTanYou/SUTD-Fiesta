using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class canvas : MonoBehaviour
{
    
    public Finish finish;
    public GameObject goal;
    public string text;

    public TextMeshProUGUI canvastext;
    
    // Start is called before the first frame update
    void Start()
    {
    //goal = GameObject.Find("Finish");
    // finish = FindObjectOfType<Finish>();
    //finish= GetComponent<Finish>();
    canvastext = this.GetComponentInChildren<TextMeshProUGUI>();
    canvastext.fontSize = 100;
    canvastext.alignment = TextAlignmentOptions.Center;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<4; i++){
            if(Finish.TWplayerscore[i]==0){
            text= " ";
        }
        else if (Finish.TWplayerscore.Count>=4){
             //this.GetComponentInChildren<Text>().text = "Winner is player " + (winner+1).ToString();
            text = "Final order:\n";
            text += 
            "First:\t\t\t" + Finish.TWplayerscore[0] + "\n" +
            "Second:\t\t\t" + Finish.TWplayerscore[1] + "\n" +
            "Third:\t\t\t" + Finish.TWplayerscore[2] + "\n" +
            "Forth:\t\t\t" + Finish.TWplayerscore[3];
            this.GetComponentInChildren<TextMeshProUGUI>().text= text;
            canvastext.text = text;
            canvastext.fontSize = 50;
            canvastext.alignment = TextAlignmentOptions.Flush;
            this.GetComponentInChildren<Image>().enabled = true;
            FindObjectOfType<Finish>().enabled = false;
            
            StartCoroutine(ChangeScene());
        }
        }
}
        IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(sceneName: "Scenes/TallyScoreAfterCanteen");
        }
}
