using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour {
    public bool inCogMachineTrigger; // Trying to refactor out
    
    public int numCogs;            // Number of cogs to collect
    private int collectedCogs;     // Number of collected cogs
    
    private GameObject[] cogs;        //?
    public bool inInventory;          //?
    public GameObject cogPosition;    //? 
    public GameObject player;         //?

    private CogManager mainMachine;

    public void Start() {
        // May be unnecesary
        cogs = new GameObject[numCogs];
        inCogMachineTrigger = false;
        // End
        
        collectedCogs = 0;
        mainMachine = GameObject.Find("MainMachine").GetComponent<CogManager>();
    }

    /*
    public void Update() {
        // This should not be here.
        if (Input.GetKeyDown("e")) {
            if (inCogMachineTrigger) {
                int cogNum;
                if (cogPosition.name.Length == 13) {
                    print("got in here and now it doesnt work");
                    cogNum = Convert.ToInt32(cogPosition.name.Substring(cogPosition.name.Length - 2, 2)) - 1;
                    print("cog num is " + cogNum + cogPosition);
                }
                else {
                    print("got in  and now it doesnt work");
                    cogNum = Convert.ToInt32(cogPosition.name.Substring(cogPosition.name.Length - 1, 1)) - 1;
                }
                print(cogPosition);
                print(cogNum);
                if (cogNum >= 25) {
                    if (GetComponent<CogManager>().allCollected()) {
                        addLever(cogPosition);
                    }
                }
                else {
                    print(cogNum);
                    cogs[cogNum] = cogPosition;
                    addCog(cogNum);

                    inCogMachineTrigger = false;
                }
            }
        }
    }
    */

    public void AddCogIntoSlot(GameObject cog, GameObject slot) {
        if (cog == null || slot == null) return;
        
        collectedCogs++; // this machine tracks the number of cogs it has collected (is this necessary?)
        mainMachine.incrementNumberOfCogs();
        
        cog.transform.position = slot.transform.position;
        Destroy(slot);
        if (!slot.name.Contains("CogMainSlot")) {
            // Add to the main machine as well if this isn't it
            mainMachine.addMainCog(cog); // TODO: Probably refactor that method
        }
    }
    
    /*
    // Why is this stuff done in here? The inventory must give the item.
    public void addCog(int position) {
        // collectedCogs++;
        GameObject replacement = player.GetComponentInChildren<Inventory>().getNextItem(false);
        // print(replacement);
        if (replacement != null && replacement.GetComponent<Item>().itemType != Item.ItemType.UTILITY) {
            if (cogs[position].name.Contains("CogMainSlot")) {
                GameObject.Find("MainMachine").GetComponent<CogManager>().addMainCog(replacement, true);
                //Destroy(replacement);
            }
            else {
                Vector3 cogPosition = cogs[position].transform.position;
                Destroy(cogs[position]);
                replacement.transform.position = cogPosition;
                replacement.GetComponent<BoxCollider>().enabled = true;
                cogs[position] = replacement;
                print(cogs[position]);

                GameObject.Find("MainMachine").GetComponent<CogManager>().addMainCog(replacement, false);
            }

            replacement.transform.parent = null;
        }
    }
    */

    public int getTotalCollectedCogs() {
        return collectedCogs;
    }

    public void addLever(GameObject lever) {
        GameObject replacement = player.GetComponentInChildren<Inventory>().getNextItem(true);
        Vector3 cogPosition = lever.transform.position;
        Destroy(lever);
        replacement.transform.position = cogPosition;
        replacement.GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("EscapeDoor").transform.GetChild(0).gameObject.GetComponent<exitDoor>().activateDoor();
    }
}
