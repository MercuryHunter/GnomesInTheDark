
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum LEVELS { LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5, LEVEL6, LEVEL7, LEVEL8 };
    public LEVELS[] currentLevel;
    private int[] numCogsPerLevel = { 3, 3, 3, 4, 3, 4, 4 };
    private int numberOfPlayers;
    public Transform[] spawnPoints;
    public GameObject[] playerPrefabs;
    private int[] playerLevels;

    // X, Y, W, H

    private Rect[,] cameraDetailsVertical = new Rect[4, 4] {
		// 1 Player
		{
            new Rect (0, 0, 1, 1),
            new Rect (-1, -1, -1, -1),
            new Rect (-1, -1, -1, -1),
            new Rect (-1, -1, -1, -1)
        },
		// 2 Player
		{
            new Rect (0, 0, 0.5f, 1),
            new Rect (0.5f, 0, 0.5f, 1),
            new Rect (-1, -1, -1, -1),
            new Rect (-1, -1, -1, -1)
        },
		// 3 Player
		{
            new Rect (0, 0, 0.5f, 1),
            new Rect (0.5f, 0, 0.5f, 0.5f),
            new Rect (0.5f, 0.5f, 0.5f, 0.5f),
            new Rect (-1, -1, -1, -1)
        },
		// 4 Player
		{
            new Rect (0, 0, 0.5f, 0.5f),
            new Rect (0, 0.5f, 0.5f, 0.5f),
            new Rect (0.5f, 0, 0.5f, 0.5f),
            new Rect (0.5f, 0.5f, 0.5f, 0.5f)
        }
    };

    private Rect[,] cameraDetailsHorizontal = new Rect[4, 4] {
		// 1 Player
		{
            new Rect (0, 0, 1, 1),
            new Rect (-1, -1, -1, -1),
            new Rect (-1, -1, -1, -1),
            new Rect (-1, -1, -1, -1)
        },
		// 2 Player
		{
            new Rect (0, 0, 1, 0.5f),
            new Rect (0, 0.5f, 1, 0.5f),
            new Rect (-1, -1, -1, -1),
            new Rect (-1, -1, -1, -1)
        },
		// 3 Player
		{
            new Rect (0, 0, 1, 0.5f),
            new Rect (0, 0.5f, 0.5f, 0.5f),
            new Rect (0.5f, 0.5f, 0.5f, 0.5f),
            new Rect (-1, -1, -1, -1)
        },
		// 4 Player
		{
            new Rect (0, 0, 0.5f, 0.5f),
            new Rect (0, 0.5f, 0.5f, 0.5f),
            new Rect (0.5f, 0, 0.5f, 0.5f),
            new Rect (0.5f, 0.5f, 0.5f, 0.5f)
        }
    };


    public void Start()
    {
        initialisePlayers(4, null);
        playerLevels = new int[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerLevels[i] = 1;
        }
    }

    public void initialisePlayers(int numberOfPlayers, MonoBehaviour[] controllerInput)
    {
        this.numberOfPlayers = numberOfPlayers;

        if (numberOfPlayers > spawnPoints.Length)
        {
            Debug.Log("Too many players, too few spawn points");

            // Quit if spawn points undefined
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
        }

        // Spawn
        for (int i = 0; i < numberOfPlayers; i++)
        {
            // Create a player
            GameObject currentPlayer = Instantiate(playerPrefabs[i % playerPrefabs.Length], spawnPoints[i % spawnPoints.Length].position, spawnPoints[i % spawnPoints.Length].rotation);
            currentPlayer.name = "player" + (i+1).ToString();
            // TODO: Set control scheme - idea being: add script to player that is polled by other player scripts for input

            // Setup player camera.
            Camera playerCam = currentPlayer.GetComponentInChildren<Camera>();
            playerCam.rect = cameraDetailsHorizontal[numberOfPlayers - 1, i];

            // Set layer of player model to cull
            int layer = LayerMask.NameToLayer("Player" + (i + 1));
            currentPlayer.layer = layer;
            Transform model = currentPlayer.transform.FindChild("Model");
            MoveToLayer(model, layer);

            // TODO: Modify lantern and object positions

            // TODO: Figure out audio
        }

    }

    void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }

    public int getPlayerLevel(int playerNum)
    {
        return playerLevels[playerNum - 1];
    }

    public void changeLevel(int newLevel, int playerNum)
    {
        print("level number is" + newLevel + " plus player number" + playerNum);
        int collectedCogs = GameObject.Find("Level" + newLevel.ToString()).GetComponent<levelHolder>().getTotalCollected();
        Text[] UITexts = GameObject.Find("player" + playerNum.ToString()).GetComponentsInChildren<Text>();
        //Text[] UITexts = playerPrefabs[playerNum].GetComponentsInChildren<Text>();
        playerLevels[playerNum - 1] = newLevel;
        for (int i = 0; i < UITexts.Length; i++)
        {
            if (UITexts[i].gameObject.name == "cogNumber")
            {
                print("Chaning the text to the next level");
                UITexts[i].text = collectedCogs.ToString() + "/" + numCogsPerLevel[newLevel-1].ToString();
            }
        }
    }

    public void updateAllPlayers(int playerOnLevel)
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if ( playerLevels[i] ==playerLevels[ playerOnLevel-1])
            {
                changeLevel(playerLevels[playerOnLevel - 1], i + 1);
            }
        }
    }

}
