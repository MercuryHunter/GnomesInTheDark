using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInventory : MonoBehaviour {
    /* Class is used for the player to interact with the items to add to the inventory or use 
     * to break walls or add to a cog machines */

    public float interactionDistance;
    private int interactibleMask;
    private Camera cam;

    private Inventory inventory;
    private BaseController controller;

    private Collider otherObject;
    private MachineManager interactingMachineManager;

    private LanternFuel lanternFuel;

    public void Start() {
        inventory = gameObject.GetComponentInChildren<Inventory>();
        controller = GetComponent<BaseController>();
        
        interactingMachineManager = null;
        interactibleMask = LayerMask.GetMask("Interactible");
        
        cam = GetComponentInChildren<Camera>();
        lanternFuel = GetComponentInChildren<LanternFuel>();
    }

    public void Update() {
        if (controller.interact()) {
            Ray camRay = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)); // From the middle of the current camera
            RaycastHit interactibleHit;

            GameObject interactingObject = null;

            //Debug.DrawRay(camRay.origin, camRay.direction);
            if (Physics.Raycast(camRay, out interactibleHit, interactionDistance, interactibleMask)) {
                // Get game object to interact with
                interactingObject = interactibleHit.transform.gameObject;
                if (interactingObject != null) {
                    Debug.Log("I am looking at: " + interactingObject.name);
                }
            }

            if (interactingObject == null) return;
            
            if (interactingObject.tag == "Item") {
                // TODO: Pickup item (if has inventory space)
            } else if (interactingObject.tag == "CogSlot") {
                // TODO: Fill in CogSlot
            } else if (interactingObject.tag == "BreakableWall") {
                // TODO: Use pick if has pick
            } else if (interactingObject.tag == "OilRig") {
                lanternFuel.refillFuel();
            }
            
            // TODO: Refactor
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
        /*
        if (other.gameObject.tag == "OilRig") {
            print("enetered oil rig" + other.name);
            GetComponentInChildren<LanternFuel>().setInOilRig(true);
        }
        */
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
        /*
        if (other.gameObject.tag == "OilRig") {
            GetComponentInChildren<LanternFuel>().setInOilRig(false);
        }
        */
    }
}