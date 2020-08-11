using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class buttonscript : MonoBehaviour
{
    public int playernum;
    [SerializeField]
    public Color glowcolor, currentColor;
    //private Color savedcolor, currentColor, targetColor;
    public bool highlighted  = false;
    [SerializeField]
    public Sprite inactive_button, active_button, target_sprite;
    public Animator wrong_butt_anim;

    [SerializeField]
    public GameObject timingbarprefab, timingbar;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //savedcolor = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().sprite = inactive_button;
        //Debug.Log(savedcolor.ToString());
        //Debug.Log(glowcolor.ToString());
        currentColor = this.GetComponent<SpriteRenderer>().color;
        glowcolor.a = 1;
        wrong_butt_anim = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!(this.GetComponent<SpriteRenderer>().sprite == target_sprite))
        {
            this.GetComponent<SpriteRenderer>().sprite = target_sprite;
            //this.GetComponent<SpriteRenderer>().color = new Color(255,0,0,0);
            //Debug.Log("currently highlighting this color");
        }
        //check if currently highligheed 
        if(highlighted){
            bool locked = GetComponentInParent<BrowserScript>().locked;
            bool interact = GetComponentInParent<BrowserScript>().interact_pressed;
            this.GetComponents<AudioSource>()[0].Play();
            //if(Input.GetKeyDown(ControlScript.sharedinstance.players[playernum].interact)  && !ControlScript.sharedinstance.players[playernum].lockedbytimingbar){
            if(interact && !locked){
                GameObject browser = GameObject.FindGameObjectWithTag("browserplayer" + playernum.ToString());
                //Debug.Log(browser.tag);
                if(browser.GetComponent<BrowserScript>().modschosen[browser.GetComponent<BrowserScript>().currentmod].Item2 == this.GetComponentInChildren<TextMeshPro>().text){
                    timingbar = Instantiate(timingbarprefab,this.transform.position,Quaternion.identity,this.transform);
                    timingbar.transform.localScale = new Vector2(1,1);
                    timingbar.GetComponent<TimingBarScript>().playernumber = playernum;
                    //browser.GetComponent<BrowserScript>().
                }
                else{
                    this.GetComponents<AudioSource>()[1].Play();
                    //add a red blink to indicate that the wrong button is pressed.
                    //wrong_butt_anim.Play("wrong_button_pressed",0);
                    StartCoroutine(red_blink());
                    
                }
                
            }
        }
        if(timingbar != null){
            if(timingbar.activeSelf){
                if(timingbar.GetComponent<TimingBarScript>().finished){
                    if(timingbar.GetComponent<TimingBarScript>().successORfail){
                        //success; go to next mod
                        GameObject browser = GameObject.FindGameObjectWithTag("browserplayer" + playernum.ToString());
                        browser.GetComponent<BrowserScript>().getnextmod();
                    }
                    else{
                        this.GetComponents<AudioSource>()[2].Play();
                    }
                    Debug.Log("destroying timingbar");
                    Destroy(timingbar);
                }
            }
        }
    }
    
    public void highlightself(){
        //targetColor = glowcolor;
        target_sprite = active_button;
        //enabled = true;
        highlighted = true;
    }

    public void unhighlightself(){
        //targetColor = savedcolor;
        target_sprite = inactive_button;
        //enabled = true;
        highlighted = false;
        this.GetComponent<SpriteRenderer>().color = new Color(255,0,0,0);
    }
    /*
    private void OnMouseEnter()
        {
        targetColor = glowcolor;
        //enabled = true;
        highlighted = true;
        Debug.Log("currently highlighting");
        }
        private void OnMouseExit()
        {
        targetColor = savedcolor;
        //enabled = true;
        highlighted = false;
        Debug.Log("currently not");
        }*/
    IEnumerator red_blink(){
            this.GetComponent<SpriteRenderer>().color = glowcolor;
            yield return new WaitForSeconds(1);
            this.GetComponent<SpriteRenderer>().color = currentColor;
            yield return new WaitForSeconds(1);
            Debug.Log("blinked");
        
    }

}
