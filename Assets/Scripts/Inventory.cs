using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {

    public Button cog1;
    public Button cog2;
    public Button pick1;
    public Sprite cogTexture;
    public Sprite EmptyTexture;
    public EventSystem EventSystem;
    private bool isInInventory = false;
    private int numButtons = 3;
    private bool[] buttonShowing = new bool[3];
    int buttonOn = 0;

    public void Start()
    {
        for (int i = 0; i < numButtons; i++)
        {
            buttonShowing[i] = false;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (isInInventory)
            {
                isInInventory = false;
                GetComponent<Canvas>().enabled = false;
            }
            else{
                isInInventory = true;
                GetComponent<Canvas>().enabled = true;
            }
            
        }

       
    }

    public void onItemClick(Button button)
    {
        button.image.sprite = cogTexture;
        for (int i = 0; i < numButtons; i++)
        {
          //  buttonShowing[i] = false;
            GameObject.Find("Drop" + Convert.ToString(i + 1)).GetComponent<Image>().enabled = false;
        }
        buttonOn = Convert.ToInt32(button.name.Substring(button.name.Length-1, 1 ))-1;
        //buttonShowing[buttonOn] = true;
        GameObject.Find("Drop" + Convert.ToString(buttonOn+1)).GetComponent<Image>().enabled = true;
    }

    public void onDropClick(Button button)
    {
        button.image.sprite = EmptyTexture;
        GameObject.Find("Drop" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
    }

    public void dropItem(GameObject item)
    {

    }
}
