using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    //public float moveSpeed;
    public static CollisionEvent instance;

    
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj){
        float timer =0;

        while (knockbackDuration > timer){
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction*knockbackPower);
            //rb.AddForce(- * knockbackPower);
            
        }
        yield return 0;
    }
}
