using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class letterController : MonoBehaviour, objectInteration {
    private bool inLetterTrigger;
    private GameObject player;
    private bool isHolding;
    private Image onImage;
    public int totalImages;
    public Sprite[] replacedImage;
    private BaseController controller;
    int imagePosition;
    Text interactText;

    private void Start() {
        inLetterTrigger = false;
        isHolding = false;
        imagePosition = 0;
    }

    public void interact(GameObject player) {
        // player.GetComponent<PlayerMovement>().lockPlayerMovement();
        transform.FindChild("Exclamation Mark").gameObject.SetActive(false);
        if (imagePosition == totalImages) {
            player.GetComponent<PlayerMovement>().allowMovement();
            imagePosition = 0;
            //print(isHolding);
            isHolding = false;
            onImage.gameObject.GetComponent<Image>().enabled = false;
            // gameObject.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
            return;
        }
        if (imagePosition == 0) {
            player.GetComponent<PlayerMovement>().disallowMovement();
            interactText.enabled = false;
        }
        //   print("E pressed");
        // print(player.gameObject.name);
        //Image images = player.transform.FindChild("Letterspace").gameObject.GetComponent<Image>();
        Image[] images = player.GetComponentsInChildren<Image>();
        // print(images.Length);
        for (int i = 0; i < images.Length; i++) {
            if (images[i].gameObject.name == "LetterSpace") {
                isHolding = true;
                images[i].gameObject.GetComponent<Image>().enabled = true;
                onImage = images[i];
                onImage.sprite = replacedImage[imagePosition];
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                inLetterTrigger = false;
            }
        }
        imagePosition++;

    }
    private void OnTriggerEnter(Collider other)
    {
        print("activated");
        Text[] allText = other.GetComponentsInChildren<Text>();
        for (int i = 0; i < allText.Length; i++)
        {
            if (allText[i].name == "InteractText")
            {
                BaseController controllerType = other.GetComponent<BaseController>();
                if (controllerType is KeyboardController)
                {
                    allText[i].text = "Click 'e' to interact";
                }
                else
                {
                    allText[i].text = "Click 'x' to interact";
                }
                allText[i].enabled = true;
                interactText = allText[i];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.enabled = false;
    }
 }