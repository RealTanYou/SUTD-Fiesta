using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Card : Consumable
{
    public int amount = 1;
    public int __cost;

    //public AudioSource source;
    public int cost{
        get {
            return __cost;
        }
        private set {
            if (value >0){
                __cost = value;
            }
        }
    }
    protected GameObject card;
    // Start is called before the first frame update
    public Card (items type, int cost, float health = 0) : base(type, 0, health){
        this.cost = cost;
        ItemManager.onHit += hit;
    }

    public void create (GameObject card){
        card.GetComponent<BasicObjectScript>().type = type;
        card.GetComponent<BasicObjectScript>().index = 0;
        this.card = card;
    }

    // the method to be assigned by the delegate
    public void hit(items type, int index){
        if (type == this.type){
            card.SetActive(false);
            amount = 0;
        }
        //source.Play();
    }
}
