using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitDoor : MonoBehaviour, objectInteration {

	public void activateDoor()
    {
        transform.FindChild("warpDoor").gameObject.SetActive(true);
    }



    public void interact(GameObject player)
    {
        print("it interacted here");
        Destroy(player);
    }
}
