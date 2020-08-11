using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BrowserTabScript : MonoBehaviour
{
    [SerializeField]
    public GameObject modbuttonPreFab;
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();
    [SerializeField]
    public int numofbuttons, playernum, index;
    public List<string> modnames;
    public string pillarname, nextpillarname;
    private bool has_moved = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        int distance = -7;
        for(int i = 0; i < modnames.Count; i++){
            buttons.Add(Instantiate(modbuttonPreFab,
                new Vector2(this.transform.position.x,this.transform.position.y+distance),
                Quaternion.identity,this.transform));
            buttons[i].GetComponentInChildren<TextMeshPro>().text = modnames[i];
            buttons[i].GetComponent<buttonscript>().playernum = playernum;
            buttons[i].GetComponent<buttonscript>().unhighlightself();
            //buttons[i].transform.localScale = new Vector2(0.45f,0.15f);
            distance+=2;
        }
        buttons[0].GetComponent<buttonscript>().highlightself();
        Debug.Log(pillarname);
        this.GetComponentsInChildren<TextMeshPro>()[0].transform.Translate(-1.3f,-0.35f,0);
        this.GetComponentsInChildren<TextMeshPro>()[0].text = pillarname;
        this.GetComponentsInChildren<TextMeshPro>()[1].transform.Translate(0.08f,-0.30f,0);
        this.GetComponentsInChildren<TextMeshPro>()[1].text = nextpillarname;
        this.GetComponentsInChildren<TextMeshPro>()[1].fontSize = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float value = GetComponentInParent<BrowserScript>().inputvector.y;
        bool locked = GetComponentInParent<BrowserScript>().locked;
        if(Mathf.Abs(value) <= 0.2) has_moved = false;
        //if(Input.GetKeyDown(ControlScript.sharedinstance.players[playernum].up) && index < buttons.Count-1 && !ControlScript.sharedinstance.players[playernum].lockedbytimingbar){
        if(value > 0.2 && !locked && index < buttons.Count-1 && !has_moved){
            buttons[index].GetComponent<buttonscript>().unhighlightself();
            index++;
            buttons[index].GetComponent<buttonscript>().highlightself();
            has_moved = true;
        }
        //else if(Input.GetKeyDown(ControlScript.sharedinstance.players[playernum].down) && index > 0  && !ControlScript.sharedinstance.players[playernum].lockedbytimingbar){
        else if(value < -0.2 && !locked && index > 0 && !has_moved){
            buttons[index].GetComponent<buttonscript>().unhighlightself();
            index--;
            buttons[index].GetComponent<buttonscript>().highlightself();
            has_moved = true;
        }
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        this.GetComponent<AudioSource>().Play();
        for(int i = 0; i < buttons.Count; i++){
            buttons[i].SetActive(true);
            if(i == 0) buttons[i].GetComponent<buttonscript>().highlightself();
        }
        index = 0;
        
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        foreach(GameObject button in buttons){
            button.GetComponent<buttonscript>().unhighlightself();
            button.SetActive(false);
        }
    }
}
