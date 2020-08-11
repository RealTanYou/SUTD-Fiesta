using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private BrowserScript browser;
    private bool pressed = false, previous_pressed, pressed_second = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private float movement_lag;
    private Scene currentScene;


    public GameObject player;
    [SerializeField] private float movementSpeed = 5f;
    public PlayerController playerController; // Taken from generated C# script
    public int playernumber;
    public Rigidbody2D rb2d;
    [SerializeField]
    public Sprite[] playersprites = new Sprite[4];

    public float moveSpeed;
    public float jumpForce;
    // private Rigidbody2D rb;
    // [SerializeField]
    // private PlayerInput playerInput;
    // [SerializeField]
    // private PlayerController controller;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if(playerInput){
            /*
            Debug.Log(playerInput.name);
            var browsers = FindObjectsOfType<BrowserScript>();
            browsers = browsers.OrderBy(b => b.instance.playernumber).ToArray();
            Debug.Log(browsers.Length);
            var index = playerInput.playerIndex;
            Debug.Log(index);
            browser = browsers.FirstOrDefault(m => m.connected == false).instance;
            //browser = browsers[3];
            browser.connected = true;
            movement_lag = Time.time;
            */
            DontDestroyOnLoad(this.gameObject);
            player = this.gameObject;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            SceneManager.sceneLoaded += nextlevel;
            currentScene = SceneManager.GetActiveScene();
            
            //rebinding controls
            //var tvg = playerInput.currentActionMap.bindings;
            //foreach(var tvgg in tvg){
                //if(!string.IsNullOrEmpty(tvgg.overridePath))
            //    Debug.Log(tvgg.id.ToString() + " | " + tvgg.path); //path is the inputaction asset default path
                // if(tvgg.path.Contains("/f")){
                //     playerInput.currentActionMap.ApplyBindingOverride(8,new InputBinding{overridePath = "<Keyboard>/t"});
                // }
                //Debug.Log(tvgg.ToString());
            //}
            //playerInput.currentActionMap.ApplyBindingOverride(1,new InputBinding("Player/Interact[/Keyboard/t]"));
            //tvg = playerInput.currentActionMap.bindings;
            //foreach(var tvgg in tvg){
            //    Debug.Log(tvgg.ToString());
            //}
            //playerInput.GetComponent<InputActionMap>().ApplyBindingOverride(1,new InputBinding())
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {

    }
    public void OnMove(InputValue value){
        if(currentScene.name.Contains("Book")){
        Debug.Log("moving");
        browser.inputvector = value.Get<Vector2>();
        }
        else if(currentScene.name.Contains("FabLab"))
        {
            Vector2 direction = value.Get<Vector2>();
            //rb2d.AddForce()
            Debug.Log("recieved message for movement");
            Debug.Log(direction.ToString());
            rb2d.AddForce(direction * movementSpeed);
        }
        else if(currentScene.name.Contains("Canteen"))
        {
            playerController.inputvector = value.Get<Vector2>();
        }
        
    }
    public void OnInteract(InputValue value1){
        
        if(currentScene.name.Contains("Book")){
            //tan you's control code
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
        // else if(currentScene.name.Contains("FabLab")){
        //     //daniel's control code here
        // }
        else if(currentScene.name.Contains("Canteen")){
            //Tae Wong's control code here
            playerController.interact_pressed = true;
        }
        
    }

    void nextlevel(Scene scene, LoadSceneMode mode){
        if(scene.name.Contains("Book")){
            Debug.Log(playerInput.name);
            var browsers = FindObjectsOfType<BrowserScript>();
            browsers = browsers.OrderBy(b => b.instance.playernumber).ToArray();
            Debug.Log(browsers.Length);
            var index = playerInput.playerIndex;
            Debug.Log(index);
            browser = browsers.FirstOrDefault(m => m.connected == false).instance;
            //browser = browsers[3];
            browser.connected = true;
            movement_lag = Time.time;
        }
        else if(scene.name.Contains("FabLab")){
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<CircleCollider2D>().enabled = true;
            //daniel's code to handle assigning players
            FindObjectOfType<ItemManager>().playerscores.Add(0);
            playernumber = FindObjectOfType<ItemManager>().playerscores.Count-1;
            player = this.gameObject;
            Debug.Log("Player has become this.gameObject");
            player.GetComponent<SpriteRenderer>().sprite = playersprites[playernumber];
            Debug.Log("Player has got sprite renderer");
            playerController = new PlayerController();
            Debug.Log("Player has new PlayerController");
            rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        }
        else if(scene.name.Contains("Canteen")){
            //tae wong's code here
            //playerInput = GetComponent<PlayerInput>();
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
                Debug.Log(playerInput.name);
                var controllers = FindObjectsOfType<PlayerController>();
                Debug.Log(controllers.Length);
                controllers = controllers.OrderBy(b => b.instance.playernumber).ToArray();
                Debug.Log(controllers.Length);
                var index = playerInput.playerIndex;
                Debug.Log(index);
                playerController = controllers.FirstOrDefault(m => m.connected == false).instance;
                playerController.connected = true;  
        }
        else if(scene.name.Contains("userInterface")){
            //xiang hao's code here
        }
        currentScene = scene;
    }

    void OnTriggerEnter2D(Collider2D other){
       	BasicObjectInterface obj = other.GetComponent<BasicObjectInterface>();

       	if (obj != null){
        	obj.getHit(player, other.gameObject);
            GetComponent<AudioSource>().Play();     		   
		}
	}
    
}
