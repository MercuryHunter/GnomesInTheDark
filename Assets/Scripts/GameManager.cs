using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private int numberOfPlayers;
	public Transform[] spawnPoints;
	public GameObject[] playerPrefabs;

	// X, Y, W, H
	
	private Rect[][] cameraDetails = {
		// 1 Player
		new Rect[] {
			new Rect (0, 0, 1, 1),
			new Rect (-1, -1, -1, -1),
			new Rect (-1, -1, -1, -1),
			new Rect (-1, -1, -1, -1)
		},
		// 2 Player
		new Rect[] {
			new Rect (0, 0, 0.5f, 1),
			new Rect (0.5f, 0, 0.5f, 1),
			new Rect (-1, -1, -1, -1),
			new Rect (-1, -1, -1, -1)
		},
		// 3 Player
		new Rect[] {
			new Rect (0, 0, 0.5f, 1),
			new Rect (0.5f, 0, 0.5f, 0.5f),
			new Rect (0.5f, 0.5f, 0.5f, 0.5f),
			new Rect (-1, -1, -1, -1)
		},
		// 4 Player
		new Rect[] {
			new Rect (0, 0, 0.5f, 0.5f),
			new Rect (0, 0.5f, 0.5f, 0.5f),
			new Rect (0.5f, 0, 0.5f, 0.5f),
			new Rect (0.5f, 0.5f, 0.5f, 0.5f)
		}
	};

	public void Start() {
		initialisePlayers(3, null);
	}

	public void initialisePlayers(int numberOfPlayers, MonoBehaviour[] controllerInput) {
		this.numberOfPlayers = numberOfPlayers;

		if (numberOfPlayers > spawnPoints.Length) {
			Debug.Log("Too many players, too few spawn points");
			
			// Quit if spawn points undefined
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}
		
		// Spawn
		for (int i = 0; i < numberOfPlayers; i++) {
			// Create a player
			GameObject currentPlayer = Instantiate(playerPrefabs[i % playerPrefabs.Length], spawnPoints[i % spawnPoints.Length].position, spawnPoints[i % spawnPoints.Length].rotation);
			
			// TODO: Set control scheme - idea being: add script to player that is polled by other player scripts for input
			
			// Setup player camera.
			Camera playerCam = currentPlayer.GetComponentInChildren<Camera>();
			playerCam.rect = cameraDetails[numberOfPlayers - 1][i];
			
			// Set layer of player
			MoveToLayer(currentPlayer.transform, LayerMask.NameToLayer("Player" + (i + 1)));
			
			// Set player UI up like camera
			MoveToLayer(currentPlayer.transform.FindChild("UI").transform, 0);

			// TODO: Figure out audio
		}
		
	}
	
	void MoveToLayer(Transform root, int layer) {
		root.gameObject.layer = layer;
		foreach(Transform child in root)
			MoveToLayer(child, layer);
	}

}
