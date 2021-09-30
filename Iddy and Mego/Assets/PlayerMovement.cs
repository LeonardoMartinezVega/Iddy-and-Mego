using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    private Rigidbody2D rb;
    private BoxCollider2D cl;
    private float speed = 1f;
    private static float max = 14f;

    [SerializeField] private LayerMask ground;

    // Start is called before the first frame update
    private void Start(){

        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update(){

        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Jump");
 
        if(Input.GetButton("Horizontal")){

            Debug.Log(speed);
            Debug.Log(dirY);
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

            if (speed < max){
                speed += .01f;
            }
       }

       if (Input.GetButtonUp("Horizontal")){

            if (speed > 1f){
                speed = 1f;
            }
        }
       
        if (Input.GetButtonDown("Jump") && onGround()){

            rb.velocity = new Vector2(rb.velocity.x, (dirY * (speed / 2)) + 12f);
        }
    }
    private bool onGround(){

        return Physics2D.BoxCast(cl.bounds.center, cl.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}
