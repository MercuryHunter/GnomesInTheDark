using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {
    // used to store the different items that are collected by each player
    // all the buttons and images that the images takes are set here
    public Button cog1;

    public Button cog2;
    public Button pick1;
    public Sprite cogTexture;
    public Sprite EmptyTexture;
    public EventSystem EventSystem;

    // number of buttons on each inventory system, so it can be change
    // TODO: Switch this to index system
    private int numButtons = 3;

    private bool[] buttonShowing = new bool[3];
    int buttonOn = 0;

    // TODO: Shouldn't be necessary
    // used to get player position and transform
    public GameObject player;

    int playerNumber;

    

    // TODO: NECESSARY
    private bool showingInventory;
    private PlayerCamera camera;
    private Canvas canvas;

    private int selectedIndex;
    private int equippedIndex;
    private GameObject[] holdingItems;

    private BaseController controller;

    public void Start() {
        // saves the player number so each player can interact with things differently
        playerNumber = Convert.ToInt32(transform.parent.transform.parent.gameObject.name.Substring(6, 1));
        
        // initialise everything to false
        for (int i = 0; i < numButtons; i++) {
            buttonShowing[i] = false;
        }

        // TODO: NECESSARY STUFF
        showingInventory = false;
        camera = transform.parent.parent.GetComponentInChildren<PlayerCamera>(); // Messy
        canvas = GetComponent<Canvas>();

        selectedIndex = -1;
        equippedIndex = -1;
        holdingItems = new GameObject[3];

        controller = GetComponentInParent<BaseController>();
    }

    public void Update() {
        if (controller.inventory()) {
            // Invert Inventory State
            if (showingInventory) hideInventory();
            else showInventory();
        }
        /*
            if (inItemTrigger && !item.GetComponent<Item>().inInventory &&
                (item.GetComponent<PickController>() == null || !item.GetComponent<PickController>().checkHolding())) {
        */
    }

    private void showInventory() {
        showingInventory = true;
        GetComponent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        camera.rotationLock = true;
    }

    private void hideInventory() {
        showingInventory = false;
        canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        camera.rotationLock = false;
    }

    // TODO: Refactor onItemClick()
    public void onItemClick(Button button) {
        // used when an item is clicked ( a button in the inventory is clicked)
        if (!IsEmpty()) {
            // get the number of the button to find it in the holding items array ( an array of items help in the poisiotn of the button
            int buttonClicked = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
            // if that item is not null it will proceed
            if (holdingItems[buttonClicked] != null) {
                for (int i = 0; i < numButtons; i++) {
                    // reset all equip and drop buttons to false
                    buttonShowing[i] = false;
                    GameObject.Find("Drop" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
                    GameObject.Find("Equip" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
                }
                // they are different as button on is the button that has been clicked and is used in drop but button clicked is onlt for this method
                buttonOn = buttonClicked;
                // sets that that button is clicked which means its drop and equip buttons can be dhown
                buttonShowing[buttonOn] = true;
                if (holdingItems[buttonOn].GetComponent<Item>().itemType == Item.ItemType.UTILITY) {
                    GameObject.Find("Equip" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = true;
                }
                GameObject.Find("Drop" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = true;
            }
        }
    }

    // TODO: Refacter onDropClick()
    public void onDropClick(Button button) {
        // when the drop button is clicked it finds which button it belongs to
        int clickedButton = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
        // if that button is showing and active then it will drop the item
        if (buttonShowing[clickedButton]) {
            button.image.sprite = EmptyTexture;
            holdingItems[buttonOn].transform.parent = null;
            GameObject.Find("Drop" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
            GameObject.Find("Equip" + Convert.ToString(buttonOn + 1)).GetComponent<Image>().enabled = false;
            //this actually drops the item
            dropItem(holdingItems[buttonOn].GetComponent<Item>());
            // sets the item to be active again
            holdingItems[buttonOn].SetActive(true);
            // sets its collider back on
            holdingItems[buttonOn].GetComponent<SphereCollider>().enabled = true;
            holdingItems[buttonOn] = null;
            buttonShowing[clickedButton] = false;
        }
    }
    
    // TODO: Refactor onEquipClick()
    public void onEquipClick(Button button) {
        // When an item is selected to equip
        int clickedButton = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;

        if (buttonShowing[clickedButton]) {
            // get the item being equiped
            Item item = holdingItems[buttonOn].GetComponent<Item>();
            // get the pick controller of that item
            PickController holdingPick = holdingItems[buttonOn].GetComponent<PickController>();
            if (!holdingPick.checkHolding()) {
                // if it is not already holding then set its transform to be held
                item.setTransform(player.transform, false);
                item.getItem().transform.parent = player.transform;
                item.GetComponent<PickController>().addPlayer(item.transform.parent.gameObject);
                item.GetComponent<SphereCollider>().enabled = false;
                //  GameObject.Find("Equip" + Convert.ToString(clickedButton + 1)).GetComponent<Image>().enabled = false;
                equippedIndex = clickedButton;
            }
            else {
                // if it is already held, make it not held anymore
                item.getItem().transform.parent = null;
                item.gameObject.SetActive(false);
                holdingPick.setHolding(false);
                equippedIndex = -1;
            }
        }
    }

    public void dropItem(Item item) {
        // resets its transform, with the drop set to true
        item.setTransform(player.transform, true);
    }

    /*
    public void pickUpItem(GameObject gItem) {
        // if the item is picked up add to the bag

        //sets the transform in the item class
        gItem.GetComponent<Item>().pickUp(transform.parent.gameObject);
        for (int i = 0; i < numButtons; i++) {
            // the first null item in holding items will be set to the item being picked up
            if (holdingItems[i] == null) {
                gItem.GetComponent<SphereCollider>().enabled = false;
                holdingItems[i] = gItem;
                if (gItem.GetComponent<Item>().itemType == Item.ItemType.UTILITY) {
                    // this is used to delete the item from the holding items when it is used to break a wall
                    gItem.GetComponent<PickController>().setHoldingPosition(i);
                }
                else {
                    // if it is a cog

                    // increase the number of cogs collected per player
                    int level = GameObject.Find("GameManager").GetComponent<GameManager>().getPlayerLevel(playerNumber);
                    GameObject.Find("Level" + level.ToString()).GetComponent<levelHolder>().addCog();
                    // update all players that are on that level
                    GameObject.Find("GameManager").GetComponent<GameManager>().updateAllPlayers(playerNumber);
                }

                switch (i) {
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
    */
    


    // TODO: Refactor getNextItem()
    public GameObject getNextItem(bool isLeverCheck) {
        // gets the next cog that can be added to the cog machine
        for (int i = 0; i < numButtons; i++) {
            // if there is an item in the holding position and that item a cog
            if (holdingItems[i] != null && holdingItems[i].GetComponent<Item>().itemType != Item.ItemType.UTILITY) {
                // get the item to add to the machine
                if (!isLeverCheck && holdingItems[i].GetComponent<Item>().itemType == Item.ItemType.LEVER) {
                    continue;
                }
                GameObject temp = holdingItems[i];
                holdingItems[i] = null;
                temp.SetActive(true);
                temp.transform.parent = null;
                buttonShowing[i] = false;
                ChangeImageToEmpty(i);

                return temp;
            }
        }
        // if no item in inventory then return null
        return null;
    }

    public void DestroyItem(int position) {
        Destroy(holdingItems[position]);
        holdingItems[position] = null;
        ChangeImageToEmpty(position);
        buttonShowing[position] = false;
    }

    public GameObject checkHasPick() {
        // gets the position of the first pick in the inventory or returns null
        for (int i = 0; i < numButtons; i++) {
            if (holdingItems[i] != null && holdingItems[i].GetComponent<Item>().itemType == Item.ItemType.UTILITY &&
                holdingItems[i].GetComponent<PickController>().checkHolding()) {
                return holdingItems[i];
            }
        }
        return null;
    }

    // TODO: And Jonathan begins refactoring

    public bool IsEmpty() {
        foreach (GameObject gameObject in holdingItems) {
            if (gameObject != null) return false;
        }
        return true;
    }
    
    public bool IsFull() {
        foreach (GameObject gameObject in holdingItems) {
            if (gameObject == null) return false;
        }
        return true;
    }
    
    private void ChangeImageToEmpty(int position) {
        // changed the item of the postion i to the image of no cog
        switch (position) {
            case 0:
                cog1.image.sprite = EmptyTexture;
                break;
            case 1:
                cog2.image.sprite = EmptyTexture;
                break;
            case 2:
                pick1.image.sprite = EmptyTexture;
                break;
        }
    }

    private void ChangeImageToFull(int position) {
        switch (position) {
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
    } 

    // TODO: Reset Buttons
    private void resetButtons() { }

    // TODO: Disable a button

    // TODO: Enable a button
    
    public void AddItem(GameObject item) {
        int index = FirstEmptySlotIndex();
        if (index == -1) return;

        Item itemComponent = item.GetComponent<Item>();
        itemComponent.pickUp(transform.parent.gameObject);
        holdingItems[index] = item;
        
        if (itemComponent.itemType == Item.ItemType.UTILITY) {
            // TODO: Remove PickController, PlayerInventory should handle the functionality
            // this is used to delete the item from the holding items when it is used to break a wall
            item.GetComponent<PickController>().setHoldingPosition(index);
        }
        else {
            // TODO: Check if this can be improved
            // if it is a cog

            // increase the number of cogs collected per player
            int level = GameObject.Find("GameManager").GetComponent<GameManager>().getPlayerLevel(playerNumber);
            GameObject.Find("Level" + level.ToString()).GetComponent<levelHolder>().addCog();
            // update all players that are on that level
            GameObject.Find("GameManager").GetComponent<GameManager>().updateAllPlayers(playerNumber);
        }
        
        ChangeImageToFull(index);
    }

    private int FirstEmptySlotIndex() {
        for (int i = 0; i < holdingItems.Length; i++) {
            if (holdingItems[i] == null) return i;
        }
        return -1;
    }

    // TODO: Remove item from inventory
    public GameObject TakeItem(int position) {
        GameObject item = holdingItems[position];

        holdingItems[position] = null;
        item.SetActive(true);
        item.transform.parent = null;
        buttonShowing[position] = false; // TODO: Call disable button method
        ChangeImageToEmpty(position);

        return item;
    }

    public bool HasEquippedPick() {
        // Hacky - "all utility items are picks"
        if (equippedIndex == -1) return false;
        if (holdingItems[equippedIndex] == null) return false;

        return holdingItems[equippedIndex].GetComponent<Item>().itemType == Item.ItemType.UTILITY;
    }

    public void UseEquippedPick(GameObject wall) {
        if (!HasEquippedPick()) return;

        Destroy(wall);
        DestroyItem(equippedIndex);
    }

    public bool HasCog() {
        foreach (GameObject currentItem in holdingItems) {
            if (currentItem == null) continue;
            if (currentItem.GetComponent<Item>().itemType == Item.ItemType.COG) {
                return true;
            }
        }
        return false;
    }

    public GameObject GetCogIfAvailable() {
        if (!HasCog()) return null;
        
        for (int i = 0; i < holdingItems.Length; i++) {
            if (holdingItems[i] == null) continue;
            if (holdingItems[i].GetComponent<Item>().itemType == Item.ItemType.COG) {
                return TakeItem(i);
            }
        }
        return null;
    }
}