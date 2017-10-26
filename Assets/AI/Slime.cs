using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour {
    private bool hasPlayer;
    private GameObject capturedPlayers;
    public float hurtTimer;

    private void Start()
    {
        hasPlayer = false;
        capturedPlayers = null;
        hurtTimer = 3f;
    }

    private void Update()
    {
        if (hasPlayer && capturedPlayers != null)
        {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer < 0)
            {
                capturedPlayers.GetComponent<PlayerHealth>().Damage(10);
                hurtTimer = 3f;
            }
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!hasPlayer)
            {
                capturedPlayers = other.gameObject;
                other.transform.parent = this.transform;
                hasPlayer = true;
                capturedPlayers.GetComponent<PlayerMovement>().lockPlayerMovement();
                capturedPlayers.transform.FindChild("Lantern").gameObject.SetActive(false);
                capturedPlayers.transform.position = transform.position;
                //capturedPlayers.GetComponentInChildren<BoxCollider>().enabled = false;
                //other.transform.FindChild("slimeCover").gameObject.GetComponent<Image>().enabled = true;
                Image[] images = capturedPlayers.GetComponentsInChildren<Image>();
                // print(images.Length);
                for (int i = 0; i < images.Length; i++)
                {
                    if (images[i].gameObject.name == "SlimeCover")
                    {
                        images[i].gameObject.GetComponent<Image>().enabled = true;
                        
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
