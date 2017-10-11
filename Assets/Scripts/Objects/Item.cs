using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    /* each item is set in the inventory, it is used to store its position, the item type and where it is help */
    private Transform originalPosition;
    private Transform currentLocation;
    public bool inInventory;
    private bool isHolding;
    public enum ItemType { COG, UTILITY, LEVER};
    public ItemType itemType;
    private GameObject holdingPosition;
    

    private void Start()
    {
        // 2 different transforms. The start transform for it to be reset to if needed
        originalPosition = transform;
        //the transform that it is set to when it is moved and dropped
        currentLocation = transform;
        inInventory = false;
    }

    public GameObject getItem()
    {
        // returns the gameobject of the item if it is needed
        return this.gameObject;
    }

    public void setTransform(Transform newLocation, bool drop)
    {
        //this is essentially when an item is dropped or equiped
        // drop is a bool that specifies if the item is being equiped or dropped
        if (itemType == ItemType.UTILITY && !drop)
        {
            // this is for it being equiped
            // moves the pick to the position to a slighly ofcentre position from the holding position
            Vector3 tempPos = holdingPosition.transform.position;
            tempPos.y += 0.5f;
            tempPos.x += 0.3f;
            currentLocation.position = tempPos;
            transform.position = holdingPosition.transform.position;
            // lets the pick controller know that it is being held so it cant be picked up agained
            GetComponent<PickController>().setHolding(true);
        }
        else
        {
            // if it is being dropped
            // the offset postion from the holding position
            Vector3 tempPos = holdingPosition.transform.position;
            tempPos.y = 1.3f;
            tempPos.x += 0.5f;
            currentLocation.position = tempPos;
            transform.position = currentLocation.position;
            // if it is a pick, then it must be set holding as it could be equiped at the time it is dropped
            if (itemType == ItemType.UTILITY)
            {
                GetComponent<PickController>().setHolding(false);
            }
            transform.parent = null;

        }
        //activates all the gameobjects components again
        gameObject.SetActive(true);
        gameObject.GetComponent<SphereCollider>().enabled = true;
        inInventory = false;


    }

    public void pickUp(GameObject newParent)
    {
        // deactivates objects in the gameobject
        gameObject.SetActive(false);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        inInventory = true;
        // set the items parent to the current player
        transform.parent = newParent.transform;
      // one goes up to UI, next takes you up to player, Because I need the onject position for the pick or cog
        holdingPosition = transform.parent.transform.parent.FindChild("ObjectPosition").gameObject;
    }

   

    
}
