using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class pillars{
    public List<string> ISTD;
    public List<string> EPD;
    public List<string> ESD;
    public List<string> ASD;
    public List<string> HASS;

    public string tostring(){
        string message = "ISTD: ";
        foreach (string mod in ISTD){
            message += mod + ", ";
        }
        message += "\nEPD";
        foreach (string mod in EPD){
            message += mod + ", ";
        }
        message += "\nESD";
        foreach (string mod in ESD){
            message += mod + ", ";
        }
        message += "\nASD";
        foreach (string mod in ASD){
            message += mod + ", ";
        }
        message += "\nHASS";
        foreach (string mod in HASS){
            message += mod + ", ";
        }
        return message;
    }
    public List<string> getpillarmods(string name){
        switch(name){
            case "ISTD":
            return ISTD;

            case "EPD":
            return EPD;

            case "ESD":
            return ESD;

            case "ASD":
            return ASD;

            case "HASS":
            return HASS;

            default:
            return ISTD;
        }
    }
    ///<summary>
    ///randomly choose 3 mods from a given pillar to return
    ///</summary>
    public List<string> getthreemodsfrompillar(string name){
        System.Random rand = new System.Random();
        switch(name){
            case "ISTD":
            return ISTD.OrderBy(x => rand.Next()).Take(3).ToList();

            case "EPD":
            return EPD.OrderBy(x => rand.Next()).Take(3).ToList();

            case "ESD":
            return ESD.OrderBy(x => rand.Next()).Take(3).ToList();

            case "ASD":
            return ASD.OrderBy(x => rand.Next()).Take(3).ToList();

            case "HASS":
            return HASS.OrderBy(x => rand.Next()).Take(3).ToList();

            default:
            return ISTD.OrderBy(x => rand.Next()).Take(3).ToList();
        }
    }
}