using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;

    public bool grounded;

    public LayerMask whatIsGround;

    private Collider2D col;

    public PlayerController instance;
    public bool interact_pressed = false;

    [SerializeField]
    public Vector2 inputvector = Vector2.zero;

    [SerializeField]
    public int playernumber = 0;
    [SerializeField]
    public bool connected = false;

    //private void  CollisionEnter2D(Collision2D other){
       // if (other.collider.tag == "Finish"){
            //print("Finished Race");
         //   GameManager.instance.GameScore +=1;
     //   }
  //  }
    // Start is called before the first frame update

    void Awake(){
        instance = this;
    }
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();

        col = GetComponent<Collider2D>();
        col.enabled = false;
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(connected) col.enabled = true;
        if(connected) rb.gravityScale = 1;
        if(this.transform.position.x > 265) col.enabled = true;
        grounded = Physics2D.IsTouchingLayers(col, whatIsGround);
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        rb.freezeRotation = true;

        //if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)){
          //  rb.velocity = new Vector2(moveSpeed*2.8f, rb.velocity.y);
        //}
        if (inputvector.x > 0 || inputvector.x < 0){
          Debug.Log("moving with force");
            rb.velocity = new Vector2(moveSpeed*2.8f, rb.velocity.y);
            inputvector = Vector2.zero;
        }
        //if (Input.GetKeyDown(KeyCode.W)){

          //  if (grounded){
            //rb.velocity = new Vector2(rb.velocity.x*1.0f, jumpForce);
            //}

      //  }
      if (interact_pressed){
            Debug.Log("attempting to jump");
            if (grounded){
            rb.velocity = new Vector2(rb.velocity.x*1.0f, jumpForce);
            }
            //inputvector = Vector2.zero;
            interact_pressed = false;
        }

    }
}
