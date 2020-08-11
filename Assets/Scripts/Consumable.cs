using System.Collections;

using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // System.Serializable  is so that the edior field appears at Unity during runtime and you can change the values conveniently for debuggubg or testing

public class Consumable{
   
    // enumerate items like this
    public enum items {
        // enumerate pickups
        SHORTS = 0,
        SKIRT = 1,
        PANTS = 2,
        JEANS = 3,
        SLIPPERS = 4,
        SNEAKERS = 5,
        HEELS = 6,
        CARD = 7,
        DRINK = 8

    } 

    // declare a variable of type 'items'
    // using autoproperties {get; private set;}
    // cannot be initialised in the beginning
    
    public items type{get; private set;}
    public float damage{get; private set;}
    public float health{get; private set;}

    // create the constructor
    public Consumable (items type, float damage, float health){
        this.type = type;
        this.damage = damage;
        this.health = health;
    }
}




