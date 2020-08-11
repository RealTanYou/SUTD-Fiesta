using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Drink : Consumable
{
    public int amount = 10;
    public int __cost;
    public int cost {
        get {
            return __cost;
        }
        private set{
            if (value>0){
                __cost = value;
            }
        }
    }

    protected GameObject drink;

    public Drink (items type, int cost, float health = 0) : base(type, 0, health){
        this.cost = cost;
        ItemManager.onHit += hit;
    }

    public void create (GameObject drink){
        drink.GetComponent<BasicObjectScript>().type = type;
        drink.GetComponent<BasicObjectScript>().index = 0;
        this.drink = drink;
    }

    public void hit(items type, int index){
        if (type == this.type){
            drink.SetActive(false);
            //amount = 0;
        }
    }
}
