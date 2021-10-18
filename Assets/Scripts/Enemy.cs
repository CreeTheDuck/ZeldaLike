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
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake() {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage) {
        StartCoroutine(knockCo(myRigidbody, knockTime));
        TakeDamage(damage);
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
