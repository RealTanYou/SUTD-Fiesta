using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class ControlScript : MonoBehaviour
{
    public static ControlScript sharedinstance;
    public struct playercontrols{
        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;
        public KeyCode interact;
        public bool lockedbytimingbar;
    }

    public playercontrols[] players = new playercontrols[4];

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        sharedinstance = this;
        //read controls from file
        /*
        string controlstext = File.ReadAllText(Application.dataPath + "/Texts/" + "controls.json");
        Debug.Log(controlstext);
        Dictionary<string,List<string>> controlsjson = JsonConvert.DeserializeObject<Dictionary<string,List<string>> >(controlstext);
        //asign player controls
        Debug.Log(controlsjson.ToString());
        foreach(var entry in controlsjson){
            List<KeyCode> temp = new List<KeyCode>();
            Debug.Log("getting keys");
            foreach(string key in entry.Value){
                temp.Add((KeyCode) System.Enum.Parse(typeof(KeyCode), key));
            }
            Debug.Log(temp.ToString());
            if(int.Parse(entry.Key) == 0) setplayercontrol(0,new List<KeyCode>{KeyCode.Joystick1Button10,KeyCode.Joystick1Button15,KeyCode.Joystick1Button16,KeyCode.Joystick1Button17,KeyCode.Joystick1Button18});
            else setplayercontrol(int.Parse(entry.Key),temp);
        }
        foreach(string joystick in Input.GetJoystickNames()){
            Debug.Log(joystick);
        }
        */
        //setplayercontrol(0,new List<KeyCode>{KeyCode.W,KeyCode.S,KeyCode.A,KeyCode.D,KeyCode.LeftAlt});
        //setplayercontrol(1,new List<KeyCode>{KeyCode.T,KeyCode.G,KeyCode.F,KeyCode.H,KeyCode.Space});
        //setplayercontrol(2,new List<KeyCode>{KeyCode.I,KeyCode.K,KeyCode.J,KeyCode.L,KeyCode.RightAlt});
        //setplayercontrol(3,new List<KeyCode>{KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.Delete});
        //Debug.Log(players[0].up);
        //Debug.Log(players[0].down);
        //Debug.Log(players[0].left);
        //Debug.Log(players[0].right);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    ///<summary>
    ///set a player number control
    ///must give a list of all 5 controls, even if only changing one
    ///order of change if: up, down, left, right, interact
    ///</summary>
    void setplayercontrol(int playernum, List<KeyCode> controls){
        players[playernum].up = controls[0];
        players[playernum].down = controls[1];
        players[playernum].left = controls[2];
        players[playernum].right = controls[3];
        players[playernum].interact = controls[4];
    }
}
