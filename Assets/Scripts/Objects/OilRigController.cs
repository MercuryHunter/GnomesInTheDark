using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRigController : MonoBehaviour, objectInteration
{

    public void interact(GameObject player)
    {
        print(player.gameObject.name);
        player.GetComponentInChildren<LanternFuel>().refillFuel();
    }
}
