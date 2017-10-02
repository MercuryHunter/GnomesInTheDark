using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    /* Class is used for the player to interact with the items to add to the inventory or use 
     * to break walls or add to a cog machines */

    private void OnTriggerEnter(Collider other)
    {
        // On the item trigger with the player  
        if (other.gameObject.tag == "Item")
        {
            // if it is part of the item class, interacts with the inventory

            GetComponentInChildren<Inventory>().inItemTrigger = true;
            GetComponentInChildren<Inventory>().item = other.gameObject;
            
        }
        if (other.gameObject.tag == "CogMachine")
        {
            // if it is part of the cog machines class, interacts with the machine managern
            other.gameObject.GetComponentInParent<MachineManager>().inCogMachineTrigger = true;
            other.gameObject.GetComponentInParent<MachineManager>().cogPosition = other.gameObject;
            other.gameObject.GetComponentInParent<MachineManager>().player = gameObject;
        }
        if (other.gameObject.tag== "BreakableWall")
        {
            // gets the pick which is used to break a wall, if there is one
            GameObject tempPick = GetComponentInChildren<Inventory>().checkHasPick();
            if (tempPick != null)
            {
                // if it is part of the breakable wall class, interacts with the pick controller
                tempPick.GetComponent<PickController>().inWallTrigger = true;
                tempPick.GetComponent<PickController>().wall = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {

            GetComponentInChildren<Inventory>().inItemTrigger = false;
            GetComponentInChildren<Inventory>().item = null;
        }
        if (other.gameObject.tag == "CogMachine")
        {
            other.gameObject.GetComponentInParent<MachineManager>().inCogMachineTrigger = false;
            other.gameObject.GetComponentInParent<MachineManager>().cogPosition = null;
        }
        if (other.gameObject.tag == "BreakableWall")
        {
            if (other.gameObject.tag == "BreakableWall")
            {
                GameObject tempPick = GetComponentInChildren<Inventory>().checkHasPick();
                if (tempPick != null)
                {
                    tempPick.GetComponent<PickController>().inWallTrigger = false;
                    tempPick.GetComponent<PickController>().wall = null;
                }
            }
        }
    }
}
