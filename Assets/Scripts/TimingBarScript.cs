using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingBarScript : MonoBehaviour
{
    //this script will not only handle the timing bar, but the timing cursors as well
    //I will only consider success if all hits are close to the center
    [SerializeField]
    public int playernumber = 0,numcursors = 12, cursorindex = 0, cursorsdestroyedSuccess = 0, cursorsdestroyedfailed = 0;
    List<Collider2D> another = new List<Collider2D>();
    Collider2D closestcollider;
    [SerializeField]
    public GameObject cursorprefab;
    [SerializeField]
    public float speed = -0.01f;
    [SerializeField]
    public List<GameObject> cursors = new List<GameObject>();
    private float starttime;
    private List<float> cursorstarttime = new List<float>();
    public bool finished, successORfail;

    private float minx_success, maxx_success;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Debug.Log(this.GetComponent<Renderer>().bounds.max.x);
        Debug.Log(this.GetComponent<Renderer>().bounds.min.x);
        Debug.Log(this.transform.position.ToString());
        for(int i = 0; i < numcursors; i++){
            cursors.Add(Instantiate(cursorprefab,
                new Vector2(this.GetComponent<Renderer>().bounds.max.x+0.3f,this.GetComponent<Renderer>().bounds.center.y),
                Quaternion.identity, this.transform));
            cursors[i].GetComponent<TimingCursorScript>().speed = 0;
            cursors[i].GetComponent<TimingCursorScript>().name = "cursor " + i.ToString();
            //cursors[i].transform.localScale = new Vector2(0.1f,2f);
        }
        //find min and max success;
        float midrange = (this.GetComponent<Renderer>().bounds.max.x + this.GetComponent<Renderer>().bounds.min.x)/2;
        float range = this.GetComponent<Renderer>().bounds.max.x - this.GetComponent<Renderer>().bounds.min.x;
        minx_success = midrange - (range * 0.25f);
        maxx_success = midrange + (range * 0.25f);
        Debug.Log(midrange);
        Debug.Log(range);
        Debug.Log(minx_success);
        Debug.Log(maxx_success);
        //foreach(GameObject cursor in cursors){
        //    Debug.Log(cursor.transform.position.ToString());
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //ControlScript.sharedinstance.players[playernumber].lockedbytimingbar = true;
        GetComponentInParent<BrowserScript>().locked = true;
        float totaltime = 0.0f;
        //start a countdown to 5 seconds
        starttime = Time.time;
        //generate numcursors amount of starttime for the cursors
        //leave last blank, which moves only when 'time' is up
        for(int i = 0; i < numcursors-1; i++){
            totaltime += Random.Range(5.0f/(numcursors*2),5.0f/numcursors);
            cursorstarttime.Add(totaltime);
        }
        cursorstarttime.Add(5.0f);
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
        if(numcursors == cursorsdestroyedSuccess){
            finished = true;
            successORfail = true;
        }
        if(!finished){
            if(cursorindex < cursorstarttime.Count){
                if(Time.time - starttime > cursorstarttime[cursorindex]){
                    cursors[cursorindex].GetComponent<TimingCursorScript>().speed = speed;
                    cursorindex++;
                }
            }
            bool interact = GetComponentInParent<BrowserScript>().interact_pressed;
            //if(Input.GetKeyDown(ControlScript.sharedinstance.players[playernumber].interact)){
            if(interact){
                //find the closest touching collider
                closestcollider = null;
                if(another.Count > 0) closestcollider = another[0];
                Debug.Log("gotkeydown");
                
                //found closestcollider
                if(closestcollider != null){
                    //check the distance from closest collider to center of timing bar. if must be within the middle 20% of the bar;
                    Debug.Log(closestcollider.GetComponent<TimingCursorScript>().name);
                    //Debug.Log(closestcollider.gameObject.transform.position.ToString());
                    //Debug.Log(cursors[0].transform.position.ToString());
                    if(closestcollider.transform.position.x >= minx_success && closestcollider.transform.position.x <= maxx_success){
                        //within range, allow
                        another.Remove(closestcollider);
                        Destroy(closestcollider.gameObject);
                        cursorsdestroyedSuccess++;
                        this.GetComponentsInParent<AudioSource>()[3].Play();
                        Debug.Log("success in cursors");
                    }
                    else{
                        //out of range, immediately fail
                        Debug.Log("failed");
                        finished = true;
                        successORfail = false;
                        //destroy all remaining cursors
                        foreach(GameObject cursor in cursors){
                            if(cursor != null){
                            if(cursor.activeSelf){
                                Destroy(cursor);
                            }
                            }
                        }
                        //Destroy(this);
                    }
                    
                }
                /* if(this.GetComponent<Collider2D>().IsTouching(another)){
                    if(another.tag == "timingcursor"){
                        Debug.Log("touching timing bar");
                        Destroy(another.gameObject);
                    }
                } */
            }
        }
        if(cursorindex == numcursors && numcursors > cursorsdestroyedSuccess && numcursors == (cursorsdestroyedSuccess + cursorsdestroyedfailed)){
            finished = true;
            successORfail = false;
            Debug.Log("did not succeed");
        }
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        another.Add(other);
        Debug.Log(other.transform.position);
        Debug.Log("has entered");
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        another.Remove(other);
        if(other.transform.position.x <= this.GetComponent<Renderer>().bounds.min.x){
            Destroy(other.gameObject);
            cursorsdestroyedfailed++;
            finished = true;
            successORfail = false;
        }
        Debug.Log("has left");
    }

    ///<summary>
    ///return true if one is closer to the center of this transfrom
    ///return false if two is close
    ///</summary>
    bool closestToCenter(Collider2D one, Collider2D two){
        bool result = true;
        float distanceone = Vector2.Distance(this.transform.position,one.transform.position);
        float distancetwo = Vector2.Distance(this.transform.position,two.transform.position);
        if (distancetwo < distanceone) result = false;
        return result;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
         GetComponentInParent<BrowserScript>().locked =  false;
    }
}
