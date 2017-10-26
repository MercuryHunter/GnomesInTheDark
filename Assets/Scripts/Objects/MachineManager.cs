using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour {
    
    public int numCogs;            // Number of cogs to collect
    private int collectedCogs;     // Number of collected cogs

    private CogManager mainMachine;

    public void Start() {
        collectedCogs = 0;
        mainMachine = GameObject.Find("MainMachine").GetComponent<CogManager>();
    }

    public void AddCogIntoSlot(GameObject cog, GameObject slot) {
        if (cog == null || slot == null) return;
        
        collectedCogs++; // this machine tracks the number of cogs it has collected (is this necessary?)
        mainMachine.incrementNumberOfCogs();
        
        cog.transform.position = slot.transform.position;
        Destroy(slot);
        if (!slot.name.Contains("CogMainSlot")) {
            // Add to the main machine as well if this isn't it
            int slotNumber = Int32.Parse(slot.name.Substring(slot.name.Length - 2));
            mainMachine.addMainCog(cog, slotNumber);
        }
    }

    public int getTotalCollectedCogs() {
        return collectedCogs;
    }

    // TODO: Lever
    public void addLever(GameObject lever) {
        GameObject replacement = null; //player.GetComponentInChildren<Inventory>().getNextItem(true);
        Vector3 cogPosition = lever.transform.position;
        Destroy(lever);
        replacement.transform.position = cogPosition;
        replacement.GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("EscapeDoor").transform.GetChild(0).gameObject.GetComponent<exitDoor>().activateDoor();
    }
}
