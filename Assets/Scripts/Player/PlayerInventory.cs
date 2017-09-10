using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger");
        if (other.gameObject.tag == "Item")
        {

            Inventory.inItemTrigger = true;
            Inventory.item = other.gameObject;
            
        }
        if (other.gameObject.tag == "CogMachine")
        {
            print(other.gameObject);
            other.gameObject.GetComponentInParent<MachineManager>().inCogMachineTrigger = true;
            other.gameObject.GetComponentInParent<MachineManager>().cogPosition = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Inventory.inItemTrigger = false;
            Inventory.item = null;
        }
        if (other.gameObject.tag == "CogMachine")
        {
            other.gameObject.GetComponentInParent<MachineManager>().inCogMachineTrigger = false;
            other.gameObject.GetComponentInParent<MachineManager>().cogPosition = null;
        }
    }
}
