using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObjectScript : MonoBehaviour, BasicObjectInterface{
    public int index; 
    public Consumable.items type;
    public GameObject ItemManager;
    public GameObject gameobject;
  
    //this is the method that one has to implement (because of the adopted BasicObjectInterface)
    public void getHit(GameObject player, GameObject item){   
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager");
        // call the ItemManager's consumables hit method, and the item manager will notify everybody
        ItemManager.GetComponent<ItemManager>().consumablesHit(player, item);
    }
}
