using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBaseController : MonoBehaviour {

    GameObject holdingPlayer;
    GameObject currentMonster;
    int playerNumber;
    private void Start()
    {
        holdingPlayer = null;
        currentMonster = null;
    }

    public void addPlayer(GameObject player, int playerNum, GameObject currentMon)
    {
        holdingPlayer = player;
        playerNumber = playerNum;
        currentMonster = currentMon;
    }

    public void releasePlayer()
    {
        //Transform newTransform = this.transform;
        holdingPlayer.transform.position = this.transform.position;
        holdingPlayer.transform.position += transform.forward * Time.deltaTime * 100;
        holdingPlayer.transform.eulerAngles = this.transform.eulerAngles;
        holdingPlayer.GetComponent<PlayerMovement>().allowMovement();
        holdingPlayer.transform.FindChild("Lantern").gameObject.SetActive(true);
        holdingPlayer = null;
        currentMonster.GetComponent<EnemyScript>().releasePlayerNum(playerNumber);
        currentMonster = null;
        print("the player has been released");
        //playerCamera.setBodyPointVector(newTransform);
        //holdingPlayer.transform.forward;
    }
}
