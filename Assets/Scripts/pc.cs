using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pc : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;

    public bool grounded;

    public LayerMask whatIsGround;

    private Collider2D col;

    private void OnCollisionEnter2D(Collision2D other){
        if (other.collider.tag == "Finish"){
            print("Finished Race");
            GameManager.instance.GameScore +=1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();

        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        grounded = Physics2D.IsTouchingLayers(col, whatIsGround);
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        

        
    }
}
