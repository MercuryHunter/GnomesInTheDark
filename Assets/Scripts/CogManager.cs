using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogManager : MonoBehaviour {
    private int cogCount;
    private const int TOTALCOGS = 25;

    public GameObject cogPrefab;

    public void Start()
    {
        {
            cogCount = 0;
        }
    }
    public void addMainCog(GameObject newCog, bool mainMachine)
    {
        cogCount++;
        if (!mainMachine)
        {
            GameObject temp = GameObject.Find("CogMainSlot" + cogCount.ToString());
            GameObject cloneCog = Instantiate(cogPrefab, temp.transform.position, temp.transform.rotation);
            //newCog.transform.position = temp.transform.position;
            Destroy(temp);
        }
        else
        {

        }
    }
}
