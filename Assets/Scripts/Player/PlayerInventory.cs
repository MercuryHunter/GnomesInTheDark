using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Item")
        {

            GetComponentInChildren<Inventory>().inItemTrigger = true;
            GetComponentInChildren<Inventory>().item = other.gameObject;
            
        }
        if (other.gameObject.tag == "CogMachine")
        {
            print(other.gameObject);
            other.gameObject.GetComponentInParent<MachineManager>().inCogMachineTrigger = true;
            other.gameObject.GetComponentInParent<MachineManager>().cogPosition = other.gameObject;
        }
        if (other.gameObject.tag== "BreakableWall")
        {
            GameObject tempPick = GetComponentInChildren<Inventory>().checkHasPick();
            if (tempPick != null)
            {
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
