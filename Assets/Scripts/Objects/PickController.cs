using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickController : MonoBehaviour {
    public GameObject wall;
    public bool inWallTrigger;
    private int numberHits;
    public bool isHolding;
    private int holdingPosition;
    private GameObject player;

    public void Start()
    {
        inWallTrigger = false;
        isHolding = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (inWallTrigger && isHolding)
            {
                Destroy(wall);
                inWallTrigger = false;
                wall = null;
                player.GetComponentInChildren<Inventory>().destroyItem(holdingPosition);
            }
        }
    }

    public bool hitWall()
    {
        
        return false;
    }

    public bool checkHolding()
    {
        return isHolding;
    }

    public void setHolding(bool set)
    {
        isHolding = set;
    }

    public void setHoldingPosition(int pos)
    {
        holdingPosition = pos;
    }

    public void addPlayer(GameObject playerNew)
    {
        player = playerNew;
    }
}
