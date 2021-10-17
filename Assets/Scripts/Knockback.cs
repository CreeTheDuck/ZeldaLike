using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float thrust; //forceOfKnockback
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("enemy")) {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null) {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust; // normalize the vector so we dont go faster diagonally
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(knockCo(enemy));
            }
        }
    }

    private IEnumerator knockCo(Rigidbody2D enemy) { // so after hit they dont fly off forever
        if (enemy != null) {// if not dead
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero; // turn off veloctiy
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }
}
