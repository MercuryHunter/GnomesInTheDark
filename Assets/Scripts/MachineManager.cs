using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour {
    public bool inCogMachineTrigger;
    public int numCogs;
    private GameObject[] cogs;
    public bool inInventory;
    public GameObject cogPosition;

    public void Start()
    {
        cogs = new GameObject[numCogs];
        inCogMachineTrigger = false;
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
                    cogNum = Convert.ToInt32(cogPosition.name.Substring(cogPosition.name.Length - 1, 1)) - 2;
                }
                else
                {
                    cogNum = Convert.ToInt32(cogPosition.name.Substring(cogPosition.name.Length - 1, 1)) - 1;
                }
                
                cogs[cogNum] = cogPosition;
                addCog(cogNum);
                inCogMachineTrigger = false;
            }
        }
    }

    public void addCog(int position)
    {
        GameObject replacement = GameObject.Find("Backpack").GetComponent<Inventory>().getNextItem();
        if (replacement != null)
        {
            Vector3 cogPosition = cogs[position].transform.position;
            Destroy(cogs[position]);
            replacement.transform.position = cogPosition;
            replacement.GetComponent<BoxCollider>().enabled = true;
            cogs[position] = replacement;
            if (cogs[position].name.Contains("CogMainSlot"))
            {
                print("dont get in there");

                GameObject.Find("MainMachine").GetComponent<CogManager>().addMainCog(replacement, true);
                //Destroy(replacement);
            }else{
                print("get in there");
                
                
                GameObject.Find("MainMachine").GetComponent<CogManager>().addMainCog(replacement, false);
            }

        }
    }


	
}
