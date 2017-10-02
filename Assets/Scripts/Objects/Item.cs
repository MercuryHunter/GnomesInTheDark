﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private Transform originalPosition;
    private Transform currentLocation;
    public bool inInventory;
    private bool isHolding;
    public enum ItemType { COG, UTILITY};
    public ItemType itemType;
    private GameObject holdingPosition;
    

    private void Start()
    {
        originalPosition = transform;
        currentLocation = transform;
       // print(originalPosition);
        inInventory = false;
      //  holdingPosition = transform.parent.FindChild("ObjectPosition").gameObject;
        
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
            GetComponent<PickController>().setHolding(true);
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
            if (itemType == ItemType.UTILITY)
            {
                GetComponent<PickController>().setHolding(false);
            }

        }
        
        //gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.SetActive(true);
        gameObject.GetComponent<SphereCollider>().enabled = true;
        inInventory = false;

    }

    public void pickUp(GameObject newParent)
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        inInventory = true;
        transform.parent = newParent.transform;
        print(newParent.name);
      //  GameObject tempGame = GetComponentInParent<GameObject>("UI")
      // one goes up to UI, next takes you up to player, Because I need the onject position for the pick or cog
        holdingPosition = transform.parent.transform.parent.FindChild("ObjectPosition").gameObject;
    }

   

    
}
