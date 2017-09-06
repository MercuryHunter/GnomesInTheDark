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
    private GameObject[] holdingItems = new GameObject[3];
    public static GameObject item;
    private bool isFull = false;
    private int itemsInBag = 0;
    public GameObject player;
    public static bool inItemTrigger;

    public void Start()
    {
        inItemTrigger = false;
        for (int i = 0; i < numButtons; i++)
        {
            buttonShowing[i] = false;
            holdingItems[i] = null;
        }
        //holdingItems[0] = item;
    }

    public void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (isInInventory)
            {
                isInInventory = false;
                GetComponent<Canvas>().enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerCamera.rotationLock = false;
            }
            else{
                isInInventory = true;
                GetComponent<Canvas>().enabled = true;
                Cursor.lockState = CursorLockMode.None;
                PlayerCamera.rotationLock = true;
            }
            
        }
        if (Input.GetKeyDown("e"))
        {
            print("e pressed");
            if (inItemTrigger && !item.GetComponent<Item>().inInventory)
            {
                print("Trigger enter");
                if (!isFull)
                {
                    pickUpItem(item);
                }
            }
        }

       
    }

    public void onItemClick(Button button)
    {
        if (itemsInBag != 0)
        {
            int buttonClicked = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
            if (holdingItems[buttonClicked] != null)
            {
                for (int i = 0; i < numButtons; i++)
                {
                    buttonShowing[i] = false;
                    GameObject.Find("Drop" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
                    GameObject.Find("Equip" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;

                }
                buttonOn = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
                buttonShowing[buttonOn] = true;
                if (holdingItems[buttonOn].GetComponent<Item>().itemType == Item.ItemType.UTILITY)
                {
                    GameObject.Find("Equip" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = true;
                }
                GameObject.Find("Drop" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = true;
            }
        }
    }

    public void onDropClick(Button button)
    {
        int clickedButton = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
        if (buttonShowing[clickedButton])
        {
            print(buttonShowing[buttonOn]);
            itemsInBag--;
            button.image.sprite = EmptyTexture;
            holdingItems[buttonOn].transform.parent = null;
            GameObject.Find("Drop" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
            GameObject.Find("Equip" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
            print(holdingItems[buttonOn]);
            print(buttonOn);
            dropItem(holdingItems[buttonOn].GetComponent<Item>());
            
            holdingItems[buttonOn] = null;
            print(holdingItems[buttonOn]);
            buttonShowing[clickedButton] = false;

        }
    }

    public void dropItem(Item item)
    {

        item.setTransform(player.transform, true);
    }

    public void pickUpItem(GameObject gItem)
    {
        itemsInBag++;
        gItem.GetComponent<Item>().pickUp();
        for (int i = 0; i < numButtons; i++)
        {
            print("went throught" + i);
            if (holdingItems[i] == null)
            {
                
                holdingItems[i] = gItem;
                switch (i){
                    case 0:
                        cog1.image.sprite = cogTexture;
                        break;
                    case 1:
                        cog2.image.sprite = cogTexture;
                        break;
                    case 2:
                        pick1.image.sprite = cogTexture;
                        break;
                }
                //GameObject.Find("Slot" + Convert.ToString(i + 1));
                //button.image.sprite = cogTexture;
                break;
            }
            
        }
    }

    public void onEquipClick(Button button)
    {
        
        int clickedButton = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
        print(clickedButton);
        if (buttonShowing[clickedButton])
        {
            Item item = holdingItems[buttonOn].GetComponent<Item>();
            if (!item.checkHolding())
            {
                item.setTransform(player.transform, false);
                item.getItem().transform.parent = player.transform;
              //  GameObject.Find("Equip" + Convert.ToString(clickedButton + 1)).GetComponent<Image>().enabled = false;

            }
            else
            {
                item.getItem().transform.parent = null;
                item.gameObject.SetActive(false);
                item.setHolding(false);
            }

        }
    }

    
}
