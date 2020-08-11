using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    //public float knockbackPower = 100;
    //public float knockbackDuration = 1;
    
    public List<string> playerwinorder = new List<string>();
    
    public static List<int> TWplayerscore = new List<int>{
        0,0,0,0
    };

    private void OnCollisionEnter2D(Collision2D other){
        
        if(other.gameObject.tag == "Player"){
        playerwinorder.Add(other.gameObject.name);
        }
        if(playerwinorder.Count >= 4){
            int score = 4;
            foreach(string playername in playerwinorder){
                TWplayerscore[int.Parse(playername[1].ToString())-1] = score;
                score--;
            }
            //foreach(int plscore in playerscore){
                //Debug.log(plscore);
            //}
            //end the game
            
        }
    }

}
