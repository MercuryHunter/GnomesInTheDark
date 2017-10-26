using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogManager : MonoBehaviour {
    private int collectedCogs;
    private const int TOTALCOGS = 25;

    public void Start() {
        collectedCogs = 0;
    }

    public void addMainCog(GameObject newCog, int slotNumber) {
        // Machine manager already replaces the slot. This is only if it's not to this machine.

        GameObject slot = GameObject.Find("CogMainSlot" + slotNumber.ToString("00"));// collectedCogs.ToString());
        Instantiate(newCog, slot.transform.position, slot.transform.rotation);
        Destroy(slot);
    }
    
    public void incrementNumberOfCogs() {
        collectedCogs++;
    }

    public bool allCollected() {
        return collectedCogs == TOTALCOGS;
    }
}
