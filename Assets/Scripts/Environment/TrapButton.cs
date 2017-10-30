using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour {

    public GameObject linkedTrap;
    
    private float timeDown;
    private float timeToReset = 3;
    
    private bool buttonPressed;
    
    private void Start()
    {
        timeDown = 0;
        buttonPressed = false;
    }

    private void Update()
    {
        if (buttonPressed)
        {
            timeDown += Time.deltaTime;
            if (timeDown > timeToReset)
            {
                timeDown = 0;
                buttonPressed = false;
                linkedTrap.gameObject.SetActive(true);
            }
        }
    }

    public void hitButton() {
        buttonPressed = true;
        linkedTrap.gameObject.SetActive(false);
    }
}
