using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickController : MonoBehaviour {
    private int numberHits;
    public bool isHolding;
    private int holdingPosition;
    private GameObject player;

    public void Start()
    {
        isHolding = false;
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
