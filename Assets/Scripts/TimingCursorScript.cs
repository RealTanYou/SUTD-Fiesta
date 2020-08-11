using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingCursorScript : MonoBehaviour
{
    [SerializeField]
    public float speed = -0.01f;
    public string cursor_name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(speed,0,0);
    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        this.transform.Translate(speed,0,0);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        /*
        for(float f = 1f; f > 0.0f; f-=0.01f){
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r,
            this.GetComponent<SpriteRenderer>().color.g,
            this.GetComponent<SpriteRenderer>().color.b,
            f);
        }*/
    }
}
