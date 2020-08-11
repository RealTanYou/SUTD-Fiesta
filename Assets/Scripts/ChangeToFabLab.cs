using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeToFabLab : MonoBehaviour
{
    public Button StartGameButton;

    // Start is called before the first frame update
    void Start()
    {
        Button startbtn = StartGameButton.GetComponent<Button>();   
        startbtn.onClick.AddListener(TaskOnClickStart);
        startbtn.Select(); 
    }
	void TaskOnClickStart()
    {
        SceneManager.LoadScene(sceneName: "Scenes/FabLab");
	}
}
