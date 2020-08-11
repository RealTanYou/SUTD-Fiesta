using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NowToCanteen : MonoBehaviour
{
    public Button StartGameButton;

	void Start() 
    {
        Button startbtn = StartGameButton.GetComponent<Button>();
		
		startbtn.onClick.AddListener(TaskOnClickStart);

        // Allows controllers to toggle the buttons without having to use the mouse 
        // to click on the buttons first.
        startbtn.Select(); 
	}

	void TaskOnClickStart()
    {
        SceneManager.LoadScene(sceneName: "Scenes/Canteen");
	}
}
