using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterations : MonoBehaviour {
    private BaseController controller;
    private GameObject gameObjectInteracting;
    private objectInteration tempObject;
    private bool inTrigger;

    private bool update;

    private void Start() {
        controller = GetComponentInParent<BaseController>();
        update = true;
    }

    private void Update() {
        if (inTrigger) {
            if (controller.interact()) {
                if (tempObject != null) {
                    tempObject.interact(this.gameObject);
                    if(gameObjectInteracting.GetComponent<letterController>() != null) disableUpdates();
                }
            }
        }
    }

    public void disableUpdates() {
        update = false;
    }

    public void enableUpdates() {
        update = true;
        tempObject = null;
        gameObjectInteracting = null;
    }

    private void OnTriggerEnter(Collider other) {
        if (!update) return;
        // print(other.gameObject.name);
        tempObject = other.gameObject.GetComponent<objectInteration>();
        if (tempObject != null) {
            gameObjectInteracting = other.gameObject;
//            print(other.gameObject.name);
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!update) return;
        inTrigger = false;
    }
}