using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using System.Linq;


public class PersistentControls : MonoBehaviour
{
    // [SerializeField]
    // private PlayerInput playerInput;
    // [SerializeField]
    // private BrowserScript browser;
    // private bool pressed = false, previous_pressed, pressed_second = false;
    // private float movement_lag;

    // void Awake()
    // {
    //     playerInput = GetComponent<PlayerInput>();

    //     if(playerInput)
    //     {
    //         DontDestroyOnLoad(this.gameObject);
    //         SceneManager.sceneLoaded += nextlevel;
    //     }
    // }

    // void nextlevel(Scene scene, LoadSceneMode mode){
    //     //if(SceneManager.GetActiveScene().name.Contains("Book")){
    //     if(scene.name.Contains("Book")){
    //         var browsers = FindObjectsOfType<BrowserScript>();
    //         browsers = browsers.OrderBy(b => b.instance.playernumber).ToArray();
    //         var index = playerInput.playerIndex;
    //         browser = browsers.FirstOrDefault(m => m.connected == false).instance;
    //         browser.connected = true;
    //         movement_lag = Time.time;
    //     }
    // }

    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private BrowserScript browser;
    private bool pressed, previous_pressed, pressed_second = false;
    private float movement_lag;
    private Scene currentScene;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        if(playerInput)
        {
            Debug.Log(playerInput);
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += nextlevel; 
            currentScene = SceneManager.GetActiveScene();
            //rebinding controls
            var tvg = playerInput.currentActionMap.bindings;
            foreach(var tvgg in tvg)
            {
                Debug.Log(tvgg.id.ToString() + " | " + tvgg.path); //path is the inputaction asset default path
                // Debug.Log("current action map" + playerInput.currentActionMap);
                // if(tvgg.path.Contains("/f"))
                // {
                //     playerInput.currentActionMap.ApplyBindingOverride(8,new InputBinding{overridePath = "<Keyboard>/t"});
                // }
                //Debug.Log(tvgg.ToString());
            }
            // playerInput.GetComponent<InputActionMap>().ApplyBindingOverride(1,new InputBinding());
        }
    }
    void Update()
    {
        if(pressed && !pressed_second){ //button has been pressed
            pressed_second = true;
        }
        else if (pressed && pressed_second){
            Debug.Log("button pressed once");
            browser.interact_pressed = false;
            pressed = false;
            pressed_second = false;
        }
    }

    public void OnMove(InputValue value){
        Debug.Log("moving");
        browser.inputvector = value.Get<Vector2>();
    }
    public void OnInteract(InputValue value1){
        Debug.Log("interacting");
        if(value1.isPressed && !pressed && value1.isPressed != previous_pressed){ //button has not been pressed yet
        Debug.Log("button pressed");
            browser.interact_pressed = value1.isPressed;
            pressed = true;
        }
        else if (!value1.isPressed){
        Debug.Log("button released");
            browser.interact_pressed = false;
            pressed = false;
        }
        previous_pressed = value1.isPressed;
    }

    void nextlevel(Scene scene, LoadSceneMode mode){
        if(scene.name.Contains("Book")){
            var browsers = FindObjectsOfType<BrowserScript>();
            browsers = browsers.OrderBy(b => b.instance.playernumber).ToArray();
            browser = browsers.FirstOrDefault(m => m.connected == false).instance;
            browser.connected = true;
            movement_lag = Time.time;
        }
    }
}
