using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {
    public Item currentItem;
    public List<Item> items = new List<Item>(); // our inventory
    public int numberOfKeys; // how many keys we have

    public void AddItem(Item itemToAdd) {
        // Is the item a key?
        if (itemToAdd.isKey) {
            numberOfKeys++;
        }
        else {
            if (!items.Contains(itemToAdd)) {
                items.Add(itemToAdd);
            }
        }
    }
}
