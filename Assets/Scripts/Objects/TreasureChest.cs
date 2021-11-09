using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable {

    public Item contents;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    public Inventory playerInventory;
    private Animator anim;

    
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (!isOpen) {
                // Open the chest
                OpenChest();
            }
            else {
                // Chest is already open
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest() {
        // Dialog window on
        dialogBox.SetActive(true);
        // dialog text = content text
        dialogText.text = contents.itemDescription;
        // add contents to player inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // Raise signal to the player to animate getting the item
        raiseItem.Raise();
        // raise the context clue so no more clue to open
        context.Raise();
        // set the chest to opened
        isOpen = true;
        anim.SetBool("opened", true);
    }

    public void ChestAlreadyOpen() {
            // Turn dialog off
            dialogBox.SetActive(false);
            // raise the signal to the player to stop animating
            raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) {
            //Debug.Log("Player in range");
            context.Raise(); // show context clue sign over player head when in range
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) {
            //Debug.Log("Player out of range");
            context.Raise();
            playerInRange = false;
        }
    }
}
