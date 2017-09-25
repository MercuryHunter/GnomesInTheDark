using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapButtons : MonoBehaviour {

    public GameObject trap;
    private bool inButtonTrigger;
    private double timeDown;
    private bool buttonPressed;
    
    private void Start()
    {
        inButtonTrigger = false;
        timeDown = 0;
        buttonPressed = false;
    }

    private void Update()
    {
        if (inButtonTrigger)
        {
            //print("trigger went off");
            if (Input.GetKeyDown("e"))
            {
                print("opened button");
                buttonPressed = true;
                trap.gameObject.SetActive(false);
            }
        }
        if (buttonPressed)
        {
            print("button pressed");
            timeDown += Time.deltaTime;
            if (timeDown > 3)
            {
                timeDown = 0;
                buttonPressed = false;
                trap.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            inButtonTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inButtonTrigger = false;
        }
    }
}
