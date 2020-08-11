using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine.InputSystem;
//generate each browser tab
//also handles when player finishes their selection of mods
public class BrowserScript : MonoBehaviour
{
    [SerializeField]
    public GameObject browsertabprefab;
    [SerializeField]
    public GameObject[] browsertabs = new GameObject[5];
    [SerializeField]
    public Vector2 playerposition, inputvector = Vector2.zero;
    [SerializeField]
    public int playernumber = 0, currenttab = 0, currentmod = 0;
    public List<(string,string)> modschosen = new List<(string, string)>();
    [SerializeField]
    public bool interact_pressed, locked = false, connected = false, finished = false;
    private bool has_moved = false;
    private TextMeshPro[] display;
    public BrowserScript instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerposition = this.transform.position;
        instance = this;
        //modnamespath = Application.dataPath + "/Texts/";
        //var modnamesjson = File.ReadAllText(modnamespath + "modnames.json");
        //pillars modnames = JsonUtility.FromJson<pillars>(modnamesjson);
        //browsertabs[0] = Instantiate(browsertabprefab, playerposition,Quaternion.identity);
        //browsertabs[0].GetComponent<BrowserTabScript>().pillarname = "ISTD";
        //browsertabs[0].GetComponent<BrowserTabScript>().modnames = modnames.ISTD;
    }
    // Start is called before the first frame update
    void Start()
    {
        modschosen = MainCameraScript.sharedinstance.modschosen;
        browsertabs[0] = Instantiate(browsertabprefab, playerposition,Quaternion.identity,this.transform);
        browsertabs[0].GetComponent<BrowserTabScript>().pillarname = "ISTD";
        browsertabs[0].GetComponent<BrowserTabScript>().nextpillarname = "EPD";
        browsertabs[0].GetComponent<BrowserTabScript>().modnames = MainCameraScript.sharedinstance.randomly_selected_mods_buttons["ISTD"];
        browsertabs[0].GetComponent<BrowserTabScript>().playernum = playernumber;
        browsertabs[1] = Instantiate(browsertabprefab, playerposition,Quaternion.identity,this.transform);
        browsertabs[1].GetComponent<BrowserTabScript>().pillarname = "EPD";
        browsertabs[1].GetComponent<BrowserTabScript>().nextpillarname = "ESD";
        browsertabs[1].GetComponent<BrowserTabScript>().modnames = MainCameraScript.sharedinstance.randomly_selected_mods_buttons["EPD"];
        browsertabs[1].GetComponent<BrowserTabScript>().playernum = playernumber;
        browsertabs[2] = Instantiate(browsertabprefab, playerposition,Quaternion.identity,this.transform);
        browsertabs[2].GetComponent<BrowserTabScript>().pillarname = "ESD";
        browsertabs[2].GetComponent<BrowserTabScript>().nextpillarname = "ASD";
        browsertabs[2].GetComponent<BrowserTabScript>().modnames = MainCameraScript.sharedinstance.randomly_selected_mods_buttons["ESD"];
        browsertabs[2].GetComponent<BrowserTabScript>().playernum = playernumber;
        browsertabs[3] = Instantiate(browsertabprefab, playerposition,Quaternion.identity,this.transform);
        browsertabs[3].GetComponent<BrowserTabScript>().pillarname = "ASD";
        browsertabs[3].GetComponent<BrowserTabScript>().nextpillarname = "HASS";
        browsertabs[3].GetComponent<BrowserTabScript>().modnames = MainCameraScript.sharedinstance.randomly_selected_mods_buttons["ASD"];
        browsertabs[3].GetComponent<BrowserTabScript>().playernum = playernumber;
        browsertabs[4] = Instantiate(browsertabprefab, playerposition,Quaternion.identity,this.transform);
        browsertabs[4].GetComponent<BrowserTabScript>().pillarname = "HASS";
        browsertabs[4].GetComponent<BrowserTabScript>().nextpillarname = "";
        browsertabs[4].GetComponent<BrowserTabScript>().modnames = MainCameraScript.sharedinstance.randomly_selected_mods_buttons["HASS"];
        browsertabs[4].GetComponent<BrowserTabScript>().playernum = playernumber;
        browsertabs[0].SetActive(true);
        browsertabs[1].SetActive(false);
        browsertabs[2].SetActive(false);
        browsertabs[3].SetActive(false);
        browsertabs[4].SetActive(false);
        foreach(GameObject browser in browsertabs){
            //browser.transform.localScale = new Vector2(21,37.5f);
            browser.transform.Translate(0,5.35f,1);
        }

        //display = Instantiate(displayprefab,new Vector3(playerposition.x+1,playerposition.y-4.35f,-1),Quaternion.identity,this.transform);
        //display.transform.localScale = new Vector2(1,1);
        //display.GetComponentInChildren<TextMeshPro>().text = format_mod_text(modschosen[currentmod]);
        display = GetComponentsInChildren<TextMeshPro>();
        display[0].fontSize = 2;
        display[1].fontSize = 2.5f;
        format_mod_text(modschosen[currentmod]);
        //display.GetComponentInChildren<Transform>().localScale = new Vector2(1,2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //read controller input y axis
        if(Mathf.Abs(inputvector.x) <= 0.2) has_moved = false;
        //if(Input.GetKeyDown(ControlScript.sharedinstance.players[playernumber].right) && currenttab < 4 && !ControlScript.sharedinstance.players[playernumber].lockedbytimingbar){
        if(inputvector.x > 0.2 && !locked && currenttab < 4 && !has_moved){
            Debug.Log("RIGHT");
            browsertabs[currenttab].SetActive(false);
            currenttab++;
            browsertabs[currenttab].SetActive(true);
            has_moved = true;
        }
        //else if(Input.GetKeyDown(ControlScript.sharedinstance.players[playernumber].left) && currenttab > 0 && !ControlScript.sharedinstance.players[playernumber].lockedbytimingbar){
        else if (inputvector.x < -0.2 && !locked && currenttab > 0 && !has_moved){
            Debug.Log("left");
            browsertabs[currenttab].SetActive(false);
            currenttab--;
            browsertabs[currenttab].SetActive(true);
            has_moved = true;
        }
        
    }
    public void getnextmod(){
        //get the next mod
        currentmod++;
        if(currentmod >= modschosen.Count){
            if(!canvasscript.sharedinstance.a_player_has_won){
                canvasscript.sharedinstance.winner = playernumber;
                finished = true;
                //canvasscript.sharedinstance.won = true;
                //canvasscript.sharedinstance.endgame = true;
                canvasscript.sharedinstance.a_player_has_won = true;
                canvasscript.sharedinstance.playerwinorder.Add("Player " + (playernumber+1).ToString());
                canvasscript.playerscore[playernumber] = canvasscript.sharedinstance.score;
                canvasscript.sharedinstance.score--;
                Debug.Log(playernumber);

                Debug.Log(locked);
            }
            else{
                finished = true;
                //a player has already won: register as 2nd, 3rd or 4th place.
                canvasscript.sharedinstance.playerwinorder.Add("Player " + (playernumber+1).ToString());
                canvasscript.playerscore[playernumber] = canvasscript.sharedinstance.score;
                canvasscript.sharedinstance.score--;
                Debug.Log(playernumber);
                Debug.Log(locked);
            }
        }
        else{
            format_mod_text(modschosen[currentmod]);
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(finished) locked = true;
    }

    private void format_mod_text((string,string) t){
        //string result = t.Item1 + "\n" + t.Item2;
        StartCoroutine(fadetext(display[0],t.Item1));
        StartCoroutine(fadetext(display[1],t.Item2));
        //display[0].text = t.Item1;
        //display[1].text = t.Item2;
    }
    private IEnumerator fadetext(TextMeshPro text, string output_text){
        yield return StartCoroutine(FadeOutText(2f, text, output_text));
        //yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(FadeInText(2f, text,output_text));
    }
    private IEnumerator FadeInText(float timespeed, TextMeshPro text, string output_text){
        text.text = output_text;
        Debug.Log("starting fadein");
        text.color = new Color(text.color.r, text.color.g,text.color.b,0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timespeed));
            yield return null;
        }
    }
    private IEnumerator FadeOutText(float timespeed, TextMeshPro text, string output_text){
        text.color = new Color(text.color.r, text.color.g,text.color.b,1);
        Debug.Log("starting fadeout");
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timespeed));
            yield return null;
        }
    }
}
