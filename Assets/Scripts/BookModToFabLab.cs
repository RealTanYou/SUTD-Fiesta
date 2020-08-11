using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookModToFabLab : MonoBehaviour
{
    public Button BackButton;

    void Start() 
    {
        Button btn = BackButton.GetComponent<Button>();

        btn.onClick.AddListener(TaskOnClick);

        btn.Select();
	}

    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneName: "Scenes/FablabInstructions");
	}

    void Update()
    {
        
    }
}
