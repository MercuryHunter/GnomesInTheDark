using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class letterController : MonoBehaviour
{
    private bool inLetterTrigger;
    private GameObject player;
    private bool isHolding;
    private Image onImage;
    public Sprite replacedImage;

    private void Start()
    {
        inLetterTrigger = false;
        isHolding = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
           // print(isHolding);
            if (!isHolding)
            {
                if (inLetterTrigger)
                {
                    print("E pressed");
                    print(player.gameObject.name);
                    Image images =   player.transform.FindChild("Letterspace").gameObject.GetComponent<Image>();
                    //Image[] images = player.GetComponentsInChildren<Image>();
                   // for (int i = 0; i < images.Length; i++)
                   // {
                       // if (images[i].gameObject.name == "Letterspace")
                       // {
                            print("Found letter space");
                            isHolding = true;
                            images.gameObject.GetComponent<Image>().enabled = true;
                            onImage = images;
                            onImage.sprite = replacedImage;
                            gameObject.GetComponent<MeshRenderer>().enabled = false;
                            gameObject.GetComponent<SphereCollider>().enabled = false;
                            inLetterTrigger = false;
                        //}
                   // }
                }
            }
            else
            {
                print(isHolding);
                isHolding = false;
                onImage.gameObject.GetComponent<Image>().enabled = false;
               // gameObject.SetActive(true);
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("In letter controller");
        print(other.gameObject.tag);
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            print("Trigger active");
            inLetterTrigger = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            inLetterTrigger = false;
            
            if (onImage != null)
            {
                
            }
        }
    }
}
