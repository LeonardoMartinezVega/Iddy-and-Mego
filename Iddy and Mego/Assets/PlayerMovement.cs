using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    private Rigidbody2D rb; // player
    private BoxCollider2D cl; //terrain
    private float speed = 1f; // base speed
    private static float max = 14f; // max speed.

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
        // takes whatever button that affects horizontal movement(currently a and d)
        if(Input.GetButton("Horizontal")){

            rb.velocity = new Vector2(dirX * speed, rb.velocity.y); // moves the player horizontally
            // the longer the button is held the faster movement becomes until it reaches the max
            if (speed < max){
                // rate at which speed is gained. subject to change
                speed += .01f;
            }
       }
       // when switching directions, or stopping motion, player loses momentum and returns to base speed 
       if (Input.GetButtonUp("Horizontal")){

            if (speed > 1f){
                speed = 1f;
            }
        }
       // checks if player is on the ground  to jump
        if (Input.GetButtonDown("Jump") && onGround()){
            //speed affects jump height but not as much as horizontal speed. subject to change
            rb.velocity = new Vector2(rb.velocity.x, (dirY * (speed / 2)) + 12f); 
        }
    }
    // checks if player is on the ground
    private bool onGround(){

        return Physics2D.BoxCast(cl.bounds.center, cl.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}
