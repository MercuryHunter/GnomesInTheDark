using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private Transform originalPosition;
    private Transform currentLocation;
    public bool inInventory;
    private bool isHolding;
    public enum ItemType { COG, UTILITY};
    public ItemType itemType;
    public GameObject holdingPosition;

    private void Start()
    {
        originalPosition = transform;
        currentLocation = transform;
       // print(originalPosition);
        inInventory = false;
        //itemType = ItemType.UTILITY;
    }

    public GameObject getItem()
    {
        return this.gameObject;
    }

    public void setTransform(Transform newLocation, bool drop)
    {
        if (itemType == ItemType.UTILITY && !drop)
        {
            Vector3 tempPos = holdingPosition.transform.position;
            // = currentLocation.position;
            tempPos.y += 0.5f;
            tempPos.x += 0.3f;
            currentLocation.position = tempPos;
            transform.position = holdingPosition.transform.position;
            isHolding = true;
        }
        else
        {
           // currentLocation = newLocation;
            Vector3 tempPos = holdingPosition.transform.position;
            tempPos.y = 1.3f;
            tempPos.x += 0.5f;
            currentLocation.position = tempPos;
            transform.position = currentLocation.position;
            print("went in here");
            isHolding = false;
            
        }
        
        //gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.SetActive(true);
        gameObject.GetComponent<SphereCollider>().enabled = true;
        inInventory = false;

    }

    public void pickUp()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        inInventory = true;
        
    }

    public bool checkHolding()
    {
        return isHolding;
    }

    public void setHolding(bool set)
    {
        isHolding = set;
    }
}
