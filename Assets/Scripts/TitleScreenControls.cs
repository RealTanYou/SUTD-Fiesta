using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenControls : MonoBehaviour
{
    public Button StartGameButton;
	public Button ChangeControlsButton;

	void Start() 
    {
		Button ctrlbtn = ChangeControlsButton.GetComponent<Button>();
        Button startbtn = StartGameButton.GetComponent<Button>();
		
		startbtn.onClick.AddListener(TaskOnClickStart);
        ctrlbtn.onClick.AddListener(TaskOnClickControl);

        // Allows controllers to toggle the buttons without having to use the mouse 
        // to click on the buttons first.
        startbtn.Select(); 
	}

	void TaskOnClickStart()
    {
        SceneManager.LoadScene(sceneName: "Scenes/BookModInstructions");
	}

	void TaskOnClickControl()
    {
            SceneManager.LoadScene(sceneName: "Scenes/ChangeControls");
	}
    
}
