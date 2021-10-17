using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour{

    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    public void Knock(Rigidbody2D myRigidbody, float knockTime) {
        StartCoroutine(knockCo(myRigidbody, knockTime));
    }

    private IEnumerator knockCo(Rigidbody2D myRigidbody, float knockTime) { // so after hit they dont fly off forever
        if (myRigidbody != null) {// if not dead
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero; // turn off veloctiy
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
