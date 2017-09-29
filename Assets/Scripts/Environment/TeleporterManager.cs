using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterManager : MonoBehaviour {

	public Collider teleporter1, teleporter2;

	public GameObject getOtherTeleporter(Collider entered) {
		return teleporter1.Equals(entered) ? teleporter2.gameObject : teleporter1.gameObject;
	}
}
