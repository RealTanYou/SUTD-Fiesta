using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToFinalScene : MonoBehaviour
{
    public Button BackFromControlsButton;

    void Start() 
    {
		Button btn = BackFromControlsButton.GetComponent<Button>();

		btn.onClick.AddListener(TaskOnClick);
        btn.Select();
	}

    void TaskOnClick()
    {
        Debug.Log("Back to main page");
        SceneManager.LoadScene(sceneName: "Scenes/FinalScene");
	}
}
