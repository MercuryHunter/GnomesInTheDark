using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    int numberPlayers = 1;

    public Button buttonOne;

    public void Start() {
        buttonOne.Select();
    }

    public void onClick(Button button)
    {
        numberPlayers = Convert.ToInt32(button.name.Substring(button.name.Length - 1, 1));
    }
    public void onClickStart(Button button)
    {
        GameManager.numPlayersToInstatiate = numberPlayers;
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestLevel");
    }

}
