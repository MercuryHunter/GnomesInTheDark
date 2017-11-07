using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitDoor : MonoBehaviour, objectInteration {
    public void activateDoor() {
        transform.FindChild("warpDoor").gameObject.SetActive(true);
    }

    public void interact(GameObject player) {
        print("Player has successfully escaped");
        player.GetComponent<EndStateController>().Escape();
        player.transform.FindChild("Model").gameObject.SetActive(false);
        player.transform.position = new Vector3(0, -199, 0);
        player.GetComponent<Rigidbody>().useGravity = false;
        //Destroy(player);
    }
}