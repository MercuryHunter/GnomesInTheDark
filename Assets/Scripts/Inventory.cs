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
    public Sprite pickTexture;
    public Sprite EmptyTexture;
    public EventSystem EventSystem;
    public Button[] slotButtons;
    public Button[] dropButtons;
    public Button[] equipButtons;

    // number of buttons on each inventory system, so it can be change
    private int numButtons = 3;

    // used to get player position and transform
    private GameObject player;
    int playerNumber;

    public bool showingInventory;
    private PlayerCamera camera;
    private Canvas canvas;

    private int selectedIndex;
    private int equippedIndex;
    private GameObject[] holdingItems;

    private BaseController controller;
    
    // Controller selection stuff:
    private bool left;
    private bool drop;

    public void Start() {
        // saves the player number so each player can interact with things differently
        playerNumber = Convert.ToInt32(transform.parent.transform.parent.gameObject.name.Substring(6, 1));
        player = transform.parent.transform.parent.gameObject;
        
        showingInventory = false;
        camera = transform.parent.parent.GetComponentInChildren<PlayerCamera>(); // Messy
        canvas = GetComponent<Canvas>();

        selectedIndex = -1;
        equippedIndex = -1;
        holdingItems = new GameObject[3];

        controller = GetComponentInParent<BaseController>();
        left = true;
        drop = true;
    }

    public void Update() {
        if (controller.inventory()) {
            // Invert Inventory State
            if (showingInventory) HideInventory();
            else ShowInventory();
        }
        
        // This is kinda bad, but it'll have to do - not a general system, but fine for this specialised one.
        if (showingInventory) {
            if (controller.up()) {
                if (left) {
                    selectedIndex = GetPreviousNonEmptySlot();
                    ResetButtons();
                    EnableButton(selectedIndex);
                    if(selectedIndex != -1) slotButtons[selectedIndex].Select();
                }
                else {
                    drop = true;
                    if (selectedIndex != -1) dropButtons[selectedIndex].Select();
                }
            }
            if (controller.down()) {
                if (left) {
                    selectedIndex = GetNextNonEmptySlot();
                    ResetButtons();
                    EnableButton(selectedIndex);
                    if(selectedIndex != -1) slotButtons[selectedIndex].Select();
                }
                else {
                    if (selectedIndex == -1) return;
                    if (holdingItems[selectedIndex].GetComponent<Item>().itemType == Item.ItemType.UTILITY) {
                        drop = false;
                        equipButtons[selectedIndex].Select();
                    }
                }
            }
            if (controller.left()) {
                if (!left) {
                    left = true;
                    drop = true;
                    if(selectedIndex != -1) slotButtons[selectedIndex].Select();
                }
            }
            if (controller.right()) {
                if (left) {
                    if (selectedIndex == -1) return;
                    left = false;
                    drop = true;
                    dropButtons[selectedIndex].Select();
                }
            }
            if (controller.interact()) {
                if (!left) {
                    if (drop) {
                        Drop(selectedIndex);
                    }
                    else {
                        Equip(selectedIndex);
                    }
                }
            }
        }
        else {
            left = true;
            drop = true;
        }
    }

    private void ShowInventory() {
        showingInventory = true;
        GetComponent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        camera.rotationLock = true;
    }

    private void HideInventory() {
        showingInventory = false;
        canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        camera.rotationLock = false;
        ResetButtons();
    }

    public void onItemClick(Button button) {
        int buttonClicked = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
        if (holdingItems[buttonClicked] == null) return;
        
        ResetButtons();
        selectedIndex = buttonClicked;
        EnableButton(selectedIndex);
    }

    public void onDropClick(Button button) {
        // when the drop button is clicked it finds which button it belongs to
        int buttonClicked = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
        
        Drop(buttonClicked);
    }

    private void Drop(int index) {
        DisableButton(index);
        ChangeImageToEmpty(index);

        Item item = holdingItems[index].GetComponent<Item>();
        if (index == equippedIndex) {
            item.Dequip();
        }
        item.Drop();
        holdingItems[index] = null;

        selectedIndex = -1;
    }
    
    public void onEquipClick(Button button) {
        // When an item is selected to equip
        int buttonClicked = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1)) - 1;
        
        Equip(buttonClicked);
    }

    private void Equip(int index) {
        Item item = holdingItems[index].GetComponent<Item>();

        if (item.isHolding) {
            // Unequip
            equippedIndex = -1;
            item.Dequip();
        }
        else {
            // Equip
            equippedIndex = index;
            item.Equip(player);
        }
        
        HideInventory();
    }

    public void DestroyItem(int position) {
        Destroy(holdingItems[position]);
        holdingItems[position] = null;
        ChangeImageToEmpty(position);
    }
    
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
        slotButtons[position].image.sprite = EmptyTexture;
    }

    private void ChangeImageToFull(int position) {
        if (holdingItems[position].GetComponent<Item>().itemType == Item.ItemType.UTILITY)
            slotButtons[position].image.sprite = pickTexture;
        else
            slotButtons[position].image.sprite = cogTexture;
    } 

    private void ResetButtons() {
        for (int i = 0; i < numButtons; i++) {
            DisableButton(i);
        }
    }

    private void DisableButton(int position) {
        // reset all equip and drop buttons to false
        dropButtons[position].GetComponent<Image>().enabled = false;
        equipButtons[position].GetComponent<Image>().enabled = false;
    }

    private void EnableButton(int position) {
        if (position == -1) return;
        if (holdingItems[position].GetComponent<Item>().itemType == Item.ItemType.UTILITY) {
            equipButtons[position].GetComponent<Image>().enabled = true;
        }
        dropButtons[position].GetComponent<Image>().enabled = true;
    }
    
    public void AddItem(GameObject item) {
        int index = FirstEmptySlotIndex();
        if (index == -1) return;

        Item itemComponent = item.GetComponent<Item>();
        itemComponent.PickUp(transform.parent.gameObject);
        holdingItems[index] = item;
        
        if (itemComponent.itemType == Item.ItemType.COG) {
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

    private int GetPreviousNonEmptySlot() {
        for (int i = selectedIndex - 1; i >= 0; i--) {
            if (holdingItems[i] != null) return i;
        }
        return selectedIndex;
    }

    private int GetNextNonEmptySlot() {
        for (int i = selectedIndex + 1; i < numButtons; i++) {
            if (holdingItems[i] != null) return i;
        }
        return selectedIndex;
    }

    private int FirstEmptySlotIndex() {
        for (int i = 0; i < holdingItems.Length; i++) {
            if (holdingItems[i] == null) return i;
        }
        return -1;
    }

    public GameObject TakeItem(int position) {
        GameObject item = holdingItems[position];

        holdingItems[position] = null;
        item.SetActive(true);
        item.transform.parent = null;
        DisableButton(position);
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
        DisableButton(equippedIndex);
        equippedIndex = -1;
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
    
    public bool HasLever() {
        foreach (GameObject currentItem in holdingItems) {
            if (currentItem == null) continue;
            if (currentItem.GetComponent<Item>().itemType == Item.ItemType.LEVER) {
                return true;
            }
        }
        return false;
    }

    public GameObject GetLeverIfAvailable() {
        if (!HasLever()) return null;
        
        for (int i = 0; i < holdingItems.Length; i++) {
            if (holdingItems[i] == null) continue;
            if (holdingItems[i].GetComponent<Item>().itemType == Item.ItemType.LEVER) {
                return TakeItem(i);
            }
        }
        return null;
    }

    
}