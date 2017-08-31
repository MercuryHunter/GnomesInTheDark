using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private Transform originalPosition;
    private Transform currentLocation;
    public bool inInventory;
    public enum ItemType { COG, UTILITY};
    public ItemType itemType;

    private void Start()
    {
        originalPosition = transform;
        currentLocation = transform;
       // print(originalPosition);
        inInventory = false;
        itemType = ItemType.UTILITY;
    }

    public GameObject getItem()
    {
        return this.gameObject;
    }

    public void setTransform(Transform newLocation)
    {
        if (itemType == ItemType.UTILITY)
        {

        }
        else
        {
            currentLocation = newLocation;
            currentLocation.position = newLocation.position;
        }
        
        //gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.SetActive(true);
        inInventory = false;

    }

    public void pickUp()
    {
        gameObject.SetActive(false);
        inInventory = true;
    }
}
