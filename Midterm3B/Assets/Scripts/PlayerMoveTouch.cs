using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//This script goes onto the Player object
public class PlayerMoveTouch : MonoBehaviour {

    //private Animator anim;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public Vector2 movement;

    //touch control variables:
    private bool moveLeftOn = false;
    private bool moveRightOn = false;
    private bool moveUpOn = false;
    private bool moveDownOn = false;

    void Start(){
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
         //keyboard input:
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //movement buttons:
        if (moveLeftOn) { movement.x = -1; }
        if (moveRightOn) { movement.x = 1; }
        if (moveUpOn) { movement.y = 1; }
        if (moveDownOn) { movement.y = -1; }

        //paddle movement:
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveLeft(){moveLeftOn = true;}
    public void MoveLeftOff(){moveLeftOn = false;}
    public void MoveRight(){moveRightOn = true;}
    public void MoveRightOff(){moveRightOn = false;}
    public void MoveUp(){moveUpOn = true;}
    public void MoveUpOff(){moveUpOn = false;}
    public void MoveDown(){moveDownOn = true;}
    public void MoveDownOff(){moveDownOn = false;}
}