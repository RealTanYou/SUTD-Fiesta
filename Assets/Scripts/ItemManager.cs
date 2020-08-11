using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


[System.Serializable] // System.Serializable  is so that the edior field appears at Unity during runtime and you can change the values conveniently for debuggubg or testing

public class ItemManager : MonoBehaviour
{
//items variables, remember all of them are 'Consumable'
    private Card jeans;
    private Card longpants;
    private Card slippers;
    private Card heels;
    private Drink drink;
    private Card card;
    private Card skirt;
    private Card shorts;
    private Shoes shoes;

    // prefabs variables
    // all consumables are managed from this script
    // only care about the types of prefabs you want to create
    // dont need to care about the number of times objects are created
    
    public GameObject Background;
    public GameObject CardPrefab;
    public GameObject DrinkPrefab;
    public GameObject HeelsPrefab;
    public GameObject JeansPrefab;
    public GameObject PantsPrefab;
    public GameObject ShortsPrefab;
    public GameObject SkirtPrefab;
    public GameObject ShoesPrefab;
    public GameObject SlippersPrefab;

    //public AudioSource source;
    private float rangeX, rangeY;
    public delegate void ActionHit(Consumable.items type, int index);
    public static event ActionHit onHit;

    public int score{get; private set;} //properties that can be read by others but not set
    public Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    //public Text winText;            //Store a reference to the UI Text component which will display the 'You win' message.
    public float spawnTime = 3f;
     
    public List<int> playerscores;

    public static List<int> danplayerscores = new List<int>{
        0,0,0,0
    };

    public float timeRemaining;
    public bool timerIsRunning = true;
    public Text timeText;
    public Text winText, countext;

    void Start(){
        // create instances for card, drink, heels, jeans, pants, shoes, shorts, skirt, slippers
        // int amount, float damage, float health
        
        drink = new Drink(Consumable.items.DRINK, 20, 20);
        card = new Card(Consumable.items.CARD,120,120);
        shoes = new Shoes(Consumable.items.SNEAKERS,25,25);
        jeans = new Card(Consumable.items.JEANS,45,45);
        longpants = new Card(Consumable.items.PANTS, 60, 60);
        slippers = new Card(Consumable.items.SLIPPERS, 10, 10);
        heels = new Card(Consumable.items.HEELS, 30,30);
        skirt = new Card(Consumable.items.SKIRT, 20, 20);
        shorts = new Card(Consumable.items.SHORTS, 35,35);


        //get the background
        rangeX = Background.GetComponent<SpriteRenderer>().sprite.bounds.extents.x*0.8f;
        rangeY = Background.GetComponent<SpriteRenderer>().sprite.bounds.extents.y*0.8f;

        
        //instantiate();
        StartCoroutine(CreateDrink());
        StartCoroutine(CreateCard());
        StartCoroutine(CreateShoes());
        StartCoroutine(CreateJeans());
        StartCoroutine(CreateLongPants());
        StartCoroutine(CreateSlippers());
        StartCoroutine(CreateShorts());

        // Starts the timer automatically
        timerIsRunning = true;
        winText.gameObject.SetActive(false);

    }


    private void instantiateDrink(){
        drink.create(Instantiate(DrinkPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));  
    }
    private void instantiateCard(){
        card.create(Instantiate(CardPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }
    private void instantiateShoes(){
        shoes.create(Instantiate(ShoesPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }
    private void instantiateJeans(){
        jeans.create(Instantiate(JeansPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }
    private void instantiateLongPants(){
        longpants.create(Instantiate(PantsPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }
    private void instantiateSlippers(){
        slippers.create(Instantiate(SlippersPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }
    private void instantiateHeels(){
        heels.create(Instantiate(HeelsPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }
    private void instantiateSkirt(){
        skirt.create(Instantiate(HeelsPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }
    private void instantiateShorts(){
        shorts.create(Instantiate(ShortsPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }

    IEnumerator CreateDrink(){
        while(true){ 
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            instantiateDrink();
        }   
    }

    IEnumerator CreateCard(){
        while(true){
            yield return new WaitForSeconds(Random.Range(15f, 30f));
            instantiateCard();
        }
    }

    IEnumerator CreateShoes(){
        while(true){
            yield return new WaitForSeconds(Random.Range(10f, 12f));
            instantiateShoes();
        }
    }

    IEnumerator CreateJeans(){
        while(true){
            yield return new WaitForSeconds(Random.Range(8f, 10f));
            instantiateJeans();
        }
    }

    IEnumerator CreateLongPants(){
        while(true){
            yield return new WaitForSeconds(Random.Range(7f, 13f));
            instantiateLongPants();
        }
    }
    IEnumerator CreateSlippers(){
        while(true){
            yield return new WaitForSeconds(Random.Range(5f, 7f));
            instantiateSlippers();
        }
    }
    IEnumerator CreateHeels(){
        while(true){
            yield return new WaitForSeconds(Random.Range(8f, 13f));
            instantiateHeels();
        }
    }
    IEnumerator CreateSkirt(){
        while(true){
            yield return new WaitForSeconds(Random.Range(6f, 13f));
            instantiateSkirt();
        }
    }
    IEnumerator CreateShorts(){
        while(true){
            yield return new WaitForSeconds(Random.Range(5f, 13f));
            instantiateShorts();
        }
    }

    void SetCountText(){
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        //countText.text = "P1: " + playerscores[0].ToString() + " P2: " +  playerscores[1].ToString() + " P3: " +  playerscores[2].ToString() + " P4: " +  playerscores[3].ToString();
        countText.text = "";
        for (int i = 0; i < playerscores.Count; i++){
            countText.text += "P" + (i+1).ToString() + ": " + playerscores[i].ToString() +"\n";

        }

        if (score >= 500){
            //... then set the text property of our winText object to "You win!"
            // winText.text = "You win!";
            //winText.enabled = true;
            // stop time
            //Time.timeScale = 0;
            // Application.Quit();
        }
    }

    //this is the method that will invoke onHit event
    public void consumablesHit(GameObject player, GameObject item){
        Consumable.items type = item.GetComponent<BasicObjectScript>().type;
        Debug.Log("consumables Hit!" + " of type " + type.ToString());
        //GetComponent<AudioSource>().Play();
        //bool tobedestroyed = false;
        if (onHit != null){ 
            //check if the event has any listeners
            //onHit(type, index); //if yes, call them
            //Destroy(item);

        }
        int playerindex = player.GetComponent<PlayerInputHandler>().playernumber;
        // score update
        if (type == Consumable.items.CARD){
            playerscores[playerindex] += (int) card.health;
            //+120
        }

        else if (type == Consumable.items.DRINK){
            playerscores[playerindex] -= (int) drink.health;
            //-20
        }

        else if (type == Consumable.items.HEELS){
            playerscores[playerindex] -= (int) heels.health;
            //-30
        }

        else if (type == Consumable.items.JEANS){
            playerscores[playerindex] += (int) jeans.health;
            //+45
        }

        else if (type == Consumable.items.PANTS){
            playerscores[playerindex] += (int) longpants.health;
            //+60
        }

        else if (type == Consumable.items.SHORTS){
            playerscores[playerindex] -= (int) shorts.health;
            //-35
        }

        else if (type == Consumable.items.SKIRT){
            playerscores[playerindex] -= (int) skirt.health;
            //-20
        }

        else if (type == Consumable.items.SNEAKERS){
            playerscores[playerindex] += (int) shoes.health;
            //+25
        }

        else if (type == Consumable.items.SLIPPERS){
            playerscores[playerindex] -= (int) slippers.health;
            //-10
        }

        SetCountText();
        Destroy(item);
        //GetComponent<AudioSource>().Play();   

        for(int i = 0; i < playerscores.Count; i++){
            Debug.Log("player " + i.ToString() + " score is: " + playerscores[i].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                winText.gameObject.SetActive(true);
                winText.text = countext.text;

                //countext.transform.position = new Vector3(0,0,0);
                
                StartCoroutine(ChangeScene1());
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator ChangeScene1()
    {
        // List<int> playerscore = new List<int>{-10,-20,40,30};
        Dictionary<int,int> playerwinorder = new Dictionary<int,int>();
        int i = 0;
        
        foreach(int score in playerscores){
            playerwinorder[i] = score;
            i++;
        }

        var result = playerwinorder.OrderByDescending(s => s.Value);
        
        int highestscore = 4;
        foreach(KeyValuePair<int,int> item in result){
            danplayerscores[item.Key] = highestscore;
            highestscore--;
        }

        Debug.Log("Score 1:  " + danplayerscores[0].ToString());
        Debug.Log("Score 2:  " + danplayerscores[1].ToString());
        Debug.Log("Score 3:  " + danplayerscores[2].ToString());
        Debug.Log("Score 4:  " + danplayerscores[3].ToString());
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(sceneName: "Scenes/TallyScoreAfterFablab");
    }
}
 