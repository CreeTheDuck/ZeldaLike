using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Signal context;
    public bool playerInRange;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            //Debug.Log("Player in range");
            context.Raise(); // show context clue sign over player head when in range
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            //Debug.Log("Player out of range");
            context.Raise();
            playerInRange = false;
        }
    }
}
