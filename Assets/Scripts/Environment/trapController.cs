using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapController : MonoBehaviour {
    public int damagePerSecond = 1;
    private GameObject player;
    private bool inSpikes;
    private double timeSinceLast;

    private void Start()
    {
        inSpikes = false;
        timeSinceLast = 0;
    }

    private void Update()
    {
        if (inSpikes)
        {
            timeSinceLast += Time.deltaTime;
            if (timeSinceLast > 2)
            {
                player.GetComponent<PlayerHealth>().Damage(damagePerSecond);
                timeSinceLast = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       // print("hit player with damage");
        
        if (other.gameObject.tag == "Player")
        {

            player = other.gameObject;
            player.GetComponent<PlayerHealth>().Damage(damagePerSecond);
            inSpikes = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeSinceLast = 0;
            inSpikes = false;
        }
    }
}