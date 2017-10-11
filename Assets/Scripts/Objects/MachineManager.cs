using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour {
    public bool inCogMachineTrigger;
    public int numCogs;
    private int collectedCogs;
    private GameObject[] cogs;
    public bool inInventory;
    public GameObject cogPosition;
    public GameObject player;

    public void Start()
    {
        cogs = new GameObject[numCogs];
        inCogMachineTrigger = false;
        collectedCogs = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (inCogMachineTrigger )
            {
                
                int cogNum;
                if (cogPosition.name.Length == 13)
                {
                 print("got in here and now it doesnt work");
                    cogNum = Convert.ToInt32(cogPosition.name.Substring(cogPosition.name.Length - 2, 2)) - 1;
                    print("cog num is " + cogNum + cogPosition);
                }
                else
                {
                    print("got in  and now it doesnt work");
                    cogNum = Convert.ToInt32(cogPosition.name.Substring(cogPosition.name.Length - 1, 1)) - 1;
                }
                print(cogPosition);
                print(cogNum);
                if (cogNum >= 25)
                {
                    if (GetComponent<CogManager>().allCollected())
                    {
                        addLever(cogPosition);
                    }
                        
                }
                else
                {

                    print(cogNum);
                    cogs[cogNum] = cogPosition;
                    addCog(cogNum);

                    inCogMachineTrigger = false;
                }

                
            }
        }
    }

    public void addCog(int position)
    {
       // collectedCogs++;
        GameObject replacement = player.GetComponentInChildren<Inventory>().getNextItem(false);
       // print(replacement);
        if (replacement != null && replacement.GetComponent<Item>().itemType != Item.ItemType.UTILITY)
        {            
            if (cogs[position].name.Contains("CogMainSlot"))
            {
                
                GameObject.Find("MainMachine").GetComponent<CogManager>().addMainCog(replacement, true);
                //Destroy(replacement);
            }else{
            
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

    public int getTotalCollectedCogs()
    {
        return collectedCogs;
    }

    public void addLever(GameObject lever)
    {
        GameObject replacement = player.GetComponentInChildren<Inventory>().getNextItem(true);
        Vector3 cogPosition = lever.transform.position;
        Destroy(lever);
        replacement.transform.position = cogPosition;
        replacement.GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("EscapeDoor").transform.GetChild(0).gameObject.GetComponent<exitDoor>().activateDoor();
    }

	
}
