using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        change = Vector3.zero; //every frame reset how much the player movement has changed
        change.x = Input.GetAxisRaw("Horizontal"); //defined by defualt in unity
        change.y = Input.GetAxisRaw("Vertical"); //Raw snaps directly so it doesnt feel floaty
        //Debug.Log(change); //send change to the console so you can see what the player is doing
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove(){
        if (change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX", change.x); //change the direction the player is facing
            animator.SetFloat("moveY", change.y);  //change the direction the player is facing
            animator.SetBool("moving", true); //when moving turn on the moving animation
        }else { animator.SetBool("moving", false); } //When not moving stop animating movement
    }

    void MoveCharacter(){
        myRigidbody.MovePosition(
            transform.position += change * speed * Time.deltaTime); // change postion by change times speed so not choppy times time that has passed since the last frame
    }
}
