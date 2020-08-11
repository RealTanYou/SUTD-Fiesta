using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayerwinscript : MonoBehaviour
{
    [SerializeField]
    public int playernum;
    // Start is called before the first frame update
    void Start()
    {
        if(!canvasscript.sharedinstance.a_player_has_won){
                canvasscript.sharedinstance.winner = playernum;
                canvasscript.sharedinstance.a_player_has_won = true;
                Debug.Log(playernum);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
