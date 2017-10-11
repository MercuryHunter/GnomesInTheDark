using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitDoor : MonoBehaviour {

	public void activateDoor()
    {
        transform.FindChild("warpDoor").gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
