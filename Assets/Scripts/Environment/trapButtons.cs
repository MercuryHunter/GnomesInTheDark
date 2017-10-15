using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapButtons : MonoBehaviour, objectInteration {

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
        if (buttonPressed)
        {
           // print("button pressed");
            timeDown += Time.deltaTime;
            if (timeDown > 3)
            {
                timeDown = 0;
                buttonPressed = false;
                trap.gameObject.SetActive(true);
            }
        }
    }



    public void interact( GameObject player)
    {

        //print("trigger went off");

           // print("opened button");
            buttonPressed = true;
            trap.gameObject.SetActive(false);
   
    }

}
