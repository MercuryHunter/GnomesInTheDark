using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    int numberPlayers = -1;

    public void onClick(Button button)
    {
        numberPlayers = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1));
        GameObject.Find("Start").GetComponentInChildren<Text>().text = "Start";
    }
    public void onClickStart(Button button)
    {
        if (numberPlayers < 0)
        {
            button.GetComponentInChildren<Text>().text = "Click number of players first";
        }
        else
        {
            GameManager.numPlayersToInstatiate = numberPlayers;
            UnityEngine.SceneManagement.SceneManager.LoadScene("TestLevel");
        }
    }

}
