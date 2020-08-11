using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Shoes : Consumable
{
    public int amount = 1;
    private int __cost;
    public int cost {
        get{
            return __cost;
        }
        private set {
            if (value>0){
                __cost = value;
            }
        }
    }
    protected GameObject shoes;
    public Shoes(items type, int cost, float health = 0) : base(type, 0, health){
        this.cost = cost;
        ItemManager.onHit += hit;
    } 

    public void create (GameObject shoes){
        shoes.GetComponent<BasicObjectScript>().type = type;
        shoes.GetComponent<BasicObjectScript>().index = 0;

        this.shoes = shoes;
    }

    public void hit(items type, int index){
        if (type == this.type){
            shoes.SetActive(false);
            amount = 0;
        }
    }

}
