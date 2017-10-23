using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterations : MonoBehaviour {

    private BaseController controller;
    private objectInteration tempObject;
    private bool inTrigger;
    private void Start()
    {
        controller = GetComponentInParent<BaseController>();
    }

    private void Update()
    {
        if (inTrigger)
        {
            if (controller.interact())
            {
                tempObject.interact(this.gameObject);
            }
            if (controller.attack())
            {
                tempObject.interact(this.gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
       // print(other.gameObject.name);
        tempObject = other.gameObject.GetComponent<objectInteration>();
        if (tempObject != null)
        {
            print(other.gameObject.name);
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tempObject = other.gameObject.GetComponent<objectInteration>();
        if (tempObject != null)
        {
            inTrigger = false;
        }
    }
}
