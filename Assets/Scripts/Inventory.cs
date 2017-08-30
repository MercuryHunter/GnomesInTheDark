using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public Button cog1;
    public Button cog2;
    public Button pick1;
    public Sprite cogTexture;
    public Sprite EmptyTexture;
    public EventSystem EventSystem;
    private bool isInInventory = false;

    public void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (isInInventory)
            {
                isInInventory = false;
                GetComponent<Canvas>().enabled = false;
            }
            else{
                isInInventory = true;
                GetComponent<Canvas>().enabled = true;
            }
            
        }

        
    }

    public void onClick(Button button)
    {
        print("it clicked");
        button.image.sprite = cogTexture;
    }
}
