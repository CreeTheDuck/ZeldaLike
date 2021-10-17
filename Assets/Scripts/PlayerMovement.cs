using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { //enumerator that holds all player states used for animations or doing something ie states
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour{

    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start(){
        currentState = PlayerState.walk; // start on walk state
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // initalize position as down so that hit boxes arent all on by default
        animator.SetFloat("moveX", 0); 
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update(){
        change = Vector3.zero; //every frame reset how much the player movement has changed
        change.x = Input.GetAxisRaw("Horizontal"); //defined by defualt in unity
        change.y = Input.GetAxisRaw("Vertical"); //Raw snaps directly so it doesnt feel floaty
        //Debug.Log(change); //send change to the console so you can see what the player is doing
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack) {
            StartCoroutine(AttackCo());
        }
        else if( currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo() {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null; // wait one frame
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f); // 1/3 of a second
        currentState = PlayerState.walk; // reset state
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
        change.Normalize(); // normalizes speed so the character isnt fast when walking diagonally
        myRigidbody.MovePosition(
            transform.position += change * speed * Time.deltaTime); // change postion by change times speed so not choppy times time that has passed since the last frame
    }
}
