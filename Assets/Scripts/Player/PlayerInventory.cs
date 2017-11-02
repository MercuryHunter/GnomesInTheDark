using System;
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
    private PlayerCamera playerCamera;

    private LanternFuel lanternFuel;

    private int playerNum;

    public void Start() {
        inventory = gameObject.GetComponentInChildren<Inventory>();
        controller = GetComponent<BaseController>();
        playerCamera = GetComponentInChildren<PlayerCamera>();

        interactibleMask = LayerMask.GetMask("Interactible");

        cam = GetComponentInChildren<Camera>();
        lanternFuel = GetComponentInChildren<LanternFuel>();
        
        playerNum = Convert.ToInt32(gameObject.name.Substring(6, 1));
    }

    public void Update() {
        if (inventory.showingInventory) return; // Don't do any interactions if in inventory please
        
        if (controller.interact()) {
            GameObject interactingObject = null;

            Ray camRay = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)); // From the middle of the current camera
            RaycastHit interactibleHit;
            if (Physics.Raycast(camRay, out interactibleHit, interactionDistance, interactibleMask)) {
                // Get game object to interact with
                interactingObject = interactibleHit.transform.gameObject;
                if (interactingObject != null) Debug.Log("I am looking at: " + interactingObject.name);
            }

            if (interactingObject == null) return;

            if (interactingObject.tag == "Item") {
                if (!inventory.IsFull()) inventory.AddItem(interactingObject);
            }
            else if (interactingObject.tag == "CogSlot") {
                MachineManager machineManager = interactingObject.GetComponentInParent<MachineManager>();
                machineManager.AddCogIntoSlot(inventory.GetCogIfAvailable(), interactingObject);
            }
            else if (interactingObject.tag == "LeverSlot") {
                MachineManager machineManager = interactingObject.GetComponentInParent<MachineManager>();
                bool succeeded = machineManager.addLever(inventory.GetLeverIfAvailable());
                if (!succeeded) {
                    if (inventory.HasLever()) {
                        // TODO: Say not enough cogs
                    }
                    else {
                        // TODO: Say this is lever slot, pls get lever
                    }
                }
            }
            else if (interactingObject.tag == "BreakableWall") {
                inventory.UseEquippedPick(interactingObject);
            }
            else if (interactingObject.tag == "OilRig") {
                lanternFuel.refillFuel();
            }
            else if (interactingObject.tag == "Letters") {
                // TODO: Ummm..
                //interactingObject.GetComponent<letterController>().interact(gameObject);
            }
            else if (interactingObject.tag == "TrapButton") {
                interactingObject.GetComponent<TrapButton>().hitButton();
            }
            else if (interactingObject.tag == "Teleporter") {
                int levelNumber = Convert.ToInt32(interactingObject.transform.parent.name.Substring(10,1));
                if (interactingObject.name == "Location1")
                    levelNumber++;

                Transform newTransform =
                    interactingObject.gameObject
                        .GetComponentInParent<TeleporterManager>()
                        .getOtherTeleporter(interactingObject.GetComponent<Collider>())
                        .transform;

                this.transform.position = newTransform.position;
                this.transform.eulerAngles = newTransform.eulerAngles;
                playerCamera.setBodyPointVector(newTransform);
                GameObject.Find("GameManager").GetComponent<GameManager>().changeLevel(levelNumber, playerNum);
            }else if (interactingObject.tag == "SlimeBase")
            {
                //Transform newTransform = interactingObject.transform;
                // this.transform.position = newTransform.position;
                //this.transform.eulerAngles = newTransform.eulerAngles;
                interactingObject.gameObject.GetComponent<SlimeBaseController>().releasePlayer();
            }
        }
    }
}