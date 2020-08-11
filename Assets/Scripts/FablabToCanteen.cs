using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FablabToCanteen : MonoBehaviour
{
    public Button NextButton;

    void Start() 
    {
        Button btn = NextButton.GetComponent<Button>();

        btn.onClick.AddListener(TaskOnClick);

        btn.Select();
	}

    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneName: "Scenes/CanteenInstructions");
	}

    void Update()
    {
        
    }
}
