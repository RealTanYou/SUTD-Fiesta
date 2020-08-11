using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField]
    public TextAsset modnamesta;
    string modnamespath;
    [SerializeField]
    //public Dictionary<string,List<string>> modschosen = new Dictionary<string, List<string>>();
    public List<(string,string)> modschosen = new List<(string, string)>();
    public pillars modnames;
    public static MainCameraScript sharedinstance;
    private Dictionary<int,string> pillarnames = new Dictionary<int, string> {
        [0] = "ISTD",
        [1] = "EPD",
        [2] = "ESD",
        [3] = "ASD",
        [4] = "HASS",};
    public  Dictionary<string,List<string>> randomly_selected_mods_buttons =  new Dictionary<string, List<string>>();
    [SerializeField]
    public int nummods = 8;
     /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        sharedinstance = this;
        Screen.SetResolution(1280,800,false,60);
        //modnamespath = Application.dataPath + "/Scripts/";
        //var modnamesjson = File.ReadAllText(modnamespath + "modnames.json");
        modnames = JsonUtility.FromJson<pillars>(modnamesta.ToString());
        //select 3 mods per pillar
        for(int i = 0; i < pillarnames.Count; i++){
            randomly_selected_mods_buttons[pillarnames[i]] = modnames.getthreemodsfrompillar(pillarnames[i]);
        }
        //generate list of random mods to be picked.
        for(int i = 0; i < nummods; i++){
            //randomly choose 1 pillar and its mod.
            bool selected = false;
            while(!selected){
                int chosenpillar = Random.Range(0,5); //0-4
                List<string> templist = randomly_selected_mods_buttons[pillarnames[chosenpillar]];
                string choice = templist[Random.Range(0,templist.Count)];
                if(modschosen.Contains((pillarnames[chosenpillar],choice))){
                    continue;
                }
                else{
                    modschosen.Add((pillarnames[chosenpillar],choice));
                    selected = true;
                }
            }
        }
        //finally, add hass mod
        //modschosen.Add((pillarnames[4], modnames.getpillarmods(pillarnames[4])[Random.Range(0,modnames.getpillarmods(pillarnames[4]).Count)]));
        /*
        foreach(var entry in modschosen){
            string message = entry.Item1 + ": " + entry.Item2;
            Debug.Log(message.ToString());
        }*/

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
