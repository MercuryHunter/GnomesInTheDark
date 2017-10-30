using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    /* each item is set in the inventory, it is used to store its position, the item type and where it is help */
    public bool isHolding;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public enum ItemType {
        COG,
        UTILITY,
        LEVER
    };

    public ItemType itemType;
    private GameObject holdingPosition;

    public void Start() {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public GameObject getItem() {
        // returns the gameobject of the item if it is needed
        return gameObject;
    }

    private void ActivateObject() {
        gameObject.SetActive(true);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void DeactivateObject() {
        gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public void Drop() {
        // if it is being dropped
        // the offset postion from the holding position
        Vector3 position = holdingPosition.transform.position;
        position.y = 1.3f;
        position.x += 0.5f;
        
        transform.position = position;
        transform.rotation = originalRotation;
        transform.parent = null;
        
        ActivateObject();
    }

    public void PickUp(GameObject newParent) {
        DeactivateObject();
        
        // set the items parent to the current player
        transform.parent = newParent.transform; // TODO: This is duplicated in the equip...
        // one goes up to UI, next takes you up to player, Because I need the onject position for the pick or cog
        holdingPosition = transform.parent.transform.parent.FindChild("ObjectPosition").gameObject;
    }
    
    public void Equip(GameObject newParent) {
        isHolding = true;
        
        transform.parent = newParent.transform;

        transform.position = holdingPosition.transform.position;
        transform.rotation = holdingPosition.transform.rotation;
        
        ActivateObject();
    }

    public void Dequip() {
        isHolding = false;
        transform.parent = null;
        DeactivateObject();
    }
}