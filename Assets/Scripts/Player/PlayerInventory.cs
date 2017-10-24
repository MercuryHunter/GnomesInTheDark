using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInventory : MonoBehaviour {
    /* Class is used for the player to interact with the items to add to the inventory or use 
     * to break walls or add to a cog machines */

    private Inventory inventory;
    private BaseController controller;

    private Collider otherObject;
    private MachineManager interactingMachineManager;

    public void Start() {
        inventory = gameObject.GetComponentInChildren<Inventory>();
        controller = GetComponent<BaseController>();
        interactingMachineManager = null;
    }

    public void Update() {
        if (controller.interact()) {
            if (interactingMachineManager != null) {
                // Hey inventory, do we have cogs, if so, add them
                if (inventory.HasCog()) {
                    interactingMachineManager.AddCogIntoSlot(inventory.GetCogIfAvailable(), otherObject.gameObject);
                    
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        otherObject = other;

        // On the item trigger with the player  
        if (other.gameObject.tag == "Item") {
            // if it is part of the item class, interacts with the inventory

            GetComponentInChildren<Inventory>().inItemTrigger = true;
            GetComponentInChildren<Inventory>().item = other.gameObject;
        }
        if (other.gameObject.tag == "CogSlot") {
            // TODO: Change this from slot to machine, machine controlling which slot to fill in 
            
            interactingMachineManager = other.gameObject.GetComponentInParent<MachineManager>();
        }
        if (other.gameObject.tag == "BreakableWall") {
            // gets the pick which is used to break a wall, if there is one
            GameObject tempPick = GetComponentInChildren<Inventory>().checkHasPick();
            if (tempPick != null) {
                // if it is part of the breakable wall class, interacts with the pick controller
                tempPick.GetComponent<PickController>().inWallTrigger = true;
                tempPick.GetComponent<PickController>().wall = other.gameObject;
            }
        }
        if (other.gameObject.tag == "OilRig") {
            print("enetered oil rig" + other.name);
            GetComponentInChildren<LanternFuel>().setInOilRig(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Item") {
            GetComponentInChildren<Inventory>().inItemTrigger = false;
            GetComponentInChildren<Inventory>().item = null;
        }
        if (other.gameObject.tag == "CogMachine") {
            other.gameObject.GetComponentInParent<MachineManager>().inCogMachineTrigger = false;
            other.gameObject.GetComponentInParent<MachineManager>().cogPosition = null;
        }
        if (other.gameObject.tag == "BreakableWall") {
            if (other.gameObject.tag == "BreakableWall") {
                GameObject tempPick = GetComponentInChildren<Inventory>().checkHasPick();
                if (tempPick != null) {
                    tempPick.GetComponent<PickController>().inWallTrigger = false;
                    tempPick.GetComponent<PickController>().wall = null;
                }
            }
        }
        if (other.gameObject.tag == "OilRig") {
            GetComponentInChildren<LanternFuel>().setInOilRig(false);
        }
    }
}