using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy{

    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    // Start is called before the first frame update
    void Start() {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate(){ // only move on physics calls not every frame
        CheckDistanceFromTarget();
    }

    void CheckDistanceFromTarget() {
        if((Vector3.Distance(target.position, transform.position) <= chaseRadius)
            && (Vector3.Distance(target.position, transform.position) > attackRadius)) {
            // move log towards target at move speed *speed since the last frame
            if ((currentState == EnemyState.idle) || (currentState == EnemyState.walk) && (currentState != EnemyState.stagger)) {// dont move towards player when attacking or staggering
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius) {
            anim.SetBool("wakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector) {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnim(Vector2 direction) {
        //Debug.Log(direction);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            //Debug.Log("x is greater");
            if (direction.x > 0) { SetAnimFloat(Vector2.right); }
            if (direction.x < 0) { SetAnimFloat(Vector2.left); }
        } else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
            //Debug.Log("y is greater");
            if (direction.y > 0) { SetAnimFloat(Vector2.up); }
            if (direction.y < 0) { SetAnimFloat(Vector2.down); }
        }
    }

    private void ChangeState(EnemyState newState) {
        if(currentState != newState) {
            currentState = newState;
        }
    }
}
