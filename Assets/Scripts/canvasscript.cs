using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class canvasscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public bool startgame = true, endgame = false;
    public bool a_player_has_won = false, countingdown=false;
    public List<string> playerwinorder = new List<string>();
    public static List<int> playerscore = new List<int>{
        0,0,0,0
    }; //to be transfered to the main screen 
    [SerializeField]
    public AudioClip[] countdownaudio = new AudioClip[11];
    private bool sound_played = true;
    public float starttime;
    public int winner, score = 4;
    public TextMeshProUGUI canvastext;
    public static canvasscript sharedinstance;
    
    public Dictionary<int,string> countdowndict = new Dictionary<int, string>{
        [0] = "3",
        [1] = "2",
        [2] = "1",
        [3] = "GO!"
    };

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        sharedinstance = this;
        canvastext = this.GetComponentInChildren<TextMeshProUGUI>();
        canvastext.fontSize = 100;
        canvastext.alignment = TextAlignmentOptions.Center;
    }
    void Start()
    {
        lockORunlockPlayers(true);
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(startgame){
            int countdown = Mathf.FloorToInt(Time.time - starttime);
            if(countdown < 4){
                if(canvastext.text != countdowndict[countdown]){
                    sound_played = false;
                    Debug.Log("playing sound");
                    Debug.Log(countdown);
                }
                if(!sound_played){
                    //this.GetComponentsInChildren<AudioSource>()[countdown].Play();
                    AudioSource.PlayClipAtPoint(countdownaudio[3-countdown],GameObject.FindGameObjectWithTag("MainCamera").transform.position,1f);
                    Debug.Log("sound has been played");
                    sound_played = true;
                }
                canvastext.text = countdowndict[countdown];
            }
            else{
                startgame = false;
                lockORunlockPlayers(false);
                canvastext.text = "";
            }
        }
        if(a_player_has_won){
            //start countdown of 10 seconds;
            if(!countingdown){
                starttime = Time.time;
                countingdown = true;
            }
            int countdown = Mathf.FloorToInt(Time.time - starttime);
            if(countdown < 10){
                if(canvastext.text != (10-countdown).ToString()){
                    sound_played = false;
                    Debug.Log("playing sound");
                    Debug.Log(countdown);
                }
                if(!sound_played){
                    //this.GetComponentsInChildren<AudioSource>()[countdown].Play();
                    AudioSource.PlayClipAtPoint(countdownaudio[10-countdown],GameObject.FindGameObjectWithTag("MainCamera").transform.position,1f);
                    Debug.Log("sound has been played");
                    sound_played = true;
                }
                canvastext.text = (10-countdown).ToString();
            }
            else{
                endgame = true;
            }

        }
        if(endgame && score > 0){
            //this.GetComponentInChildren<Text>().text = "Winner is player " + (winner+1).ToString();
            string text = "Final order:\n";
            //find how each player who has not completed did:
            Dictionary<int,int> playernummods = new Dictionary<int, int>();
            for(int i = 0; i < 4; i++){
                //ControlScript.sharedinstance.players[i].lockedbytimingbar = state;
                GameObject temp;
                temp = GameObject.FindGameObjectWithTag("browserplayer" + i.ToString());
                if(!temp.GetComponent<BrowserScript>().instance.finished){
                    //player has not finished, so get how many mods they have enrolled
                    playernummods[i] = temp.GetComponent<BrowserScript>().instance.currentmod;
                    Debug.Log("player " + i.ToString() + " has got " + playernummods[i].ToString());
                }         
            }
            Debug.Log(playernummods.Count);
            playernummods = playernummods.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach(KeyValuePair<int,int> pl in playernummods){
                Debug.Log("Player " + (pl.Key+1).ToString());
                playerwinorder.Add("Player " + (pl.Key+1).ToString());
                playerscore[pl.Key] = score;
                score--;
                if(score <= 0) break;
            }
            Debug.Log(playerscore.Count);
            foreach(int score in playerscore){
                Debug.Log(score);
            }
            text += 
            "First:\t\t" + playerwinorder[0] + "\n" +
            "Second:\t\t" + playerwinorder[1] + "\n" +
            "Third:\t\t" + playerwinorder[2] + "\n" +
            "Forth:\t\t" + playerwinorder[3];
            //score = 4;
            //for(int i = 0; i < 4; i++){
            //    playerscore[int.Parse(playerwinorder[i][playerwinorder[i].Length-1].ToString())] = score;
            //    score--;   
            //}
            lockORunlockPlayers(true);
            endgame = false;
            canvastext.text = text;
            canvastext.fontSize = 50;
            canvastext.alignment = TextAlignmentOptions.Flush;
            this.GetComponentInChildren<Image>().enabled = true;
            //Time.timeScale = 0;

            StartCoroutine(ChangeScene());
        }
    }

    void lockORunlockPlayers(bool state){
        //true = lock, false = unlock.
        for(int i = 0; i < ControlScript.sharedinstance.players.Length; i++){
            //ControlScript.sharedinstance.players[i].lockedbytimingbar = state;
            GameObject temp;
            temp = GameObject.FindGameObjectWithTag("browserplayer" + i.ToString());
            temp.GetComponent<BrowserScript>().instance.locked = state;
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneName: "Scenes/TallyScoreAfterBookMod");
    }
}
