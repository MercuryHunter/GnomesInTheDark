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

    private LanternFuel lanternFuel;

    public void Start() {
        inventory = gameObject.GetComponentInChildren<Inventory>();
        controller = GetComponent<BaseController>();

        interactibleMask = LayerMask.GetMask("Interactible");

        cam = GetComponentInChildren<Camera>();
        lanternFuel = GetComponentInChildren<LanternFuel>();
    }

    public void Update() {
        if (controller.interact()) {
            GameObject interactingObject = null;

            Ray camRay = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)); // From the middle of the current camera
            RaycastHit interactibleHit;
            if (Physics.Raycast(camRay, out interactibleHit, interactionDistance, interactibleMask)) {
                // Get game object to interact with
                interactingObject = interactibleHit.transform.gameObject;
                //if (interactingObject != null) Debug.Log("I am looking at: " + interactingObject.name);
            }

            if (interactingObject == null) return;

            if (interactingObject.tag == "Item") {
                if (!inventory.IsFull()) inventory.AddItem(interactingObject);
            }
            else if (interactingObject.tag == "CogSlot") {
                MachineManager machineManager = interactingObject.GetComponentInParent<MachineManager>();
                machineManager.AddCogIntoSlot(inventory.GetCogIfAvailable(), interactingObject);
            }
            else if (interactingObject.tag == "BreakableWall") {
                inventory.UseEquippedPick(interactingObject);
            }
            else if (interactingObject.tag == "OilRig") {
                lanternFuel.refillFuel();
            }
        }
    }
}