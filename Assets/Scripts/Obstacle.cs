using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //public float knockbackPower = 100;
    //public float knockbackDuration = 1;

    private void OnCollisionEnter2D(Collision2D other){
        
        if(other.gameObject.tag == "Obstacle"){
            SoundManager.PlaySound("obstacle_metal");
            this.transform.position = new Vector2(this.transform.position.x - 5.0f, this.transform.position.y);
            
        }
        else if(other.gameObject.tag == "Bird"){
            SoundManager.PlaySound("obstacle_bird");
            this.transform.position = new Vector2(this.transform.position.x - 5.0f, this.transform.position.y);}

        else if(other.gameObject.tag == "Finish"){
            SoundManager.PlaySound("success_drumroll_AndEating");
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            
        }
    }
}