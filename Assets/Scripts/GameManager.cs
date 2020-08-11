using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int GameScore;
    // Start is called before the first frame update
    public void Awake(){
        instance=this;
    }
    void Start()
    {
        GameScore=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
