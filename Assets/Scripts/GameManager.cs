using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private int numberOfPlayers;
	public Transform[] spawnPoints;
	public GameObject[] playerPrefabs;

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
	

	public void Start() {
		initialisePlayers(2);
	}

	public void initialisePlayers(int numberOfPlayers) {
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
		int controllerNumber = 1;
		bool keyboard = Input.GetJoystickNames().Length < numberOfPlayers;
		for (int i = 0; i < numberOfPlayers; i++) {
			// Create a player
			GameObject currentPlayer = Instantiate(playerPrefabs[i % playerPrefabs.Length], spawnPoints[i % spawnPoints.Length].position, spawnPoints[i % spawnPoints.Length].rotation);
			currentPlayer.SetActive(false);
			
			// Set up controller
			if (keyboard) {
				// We need a keyboard if there aren't enough controllers
				KeyboardController controller = currentPlayer.AddComponent<KeyboardController>();
				keyboard = false;
			}
			else if (Application.platform == RuntimePlatform.OSXPlayer ||
			         Application.platform == RuntimePlatform.OSXDashboardPlayer ||
			         Application.platform == RuntimePlatform.OSXEditor) {
				MacController controller = currentPlayer.AddComponent<MacController>();
				controller.setJoyStickNumber(controllerNumber++);
			}
			else if (Application.platform == RuntimePlatform.WindowsPlayer ||
			         Application.platform == RuntimePlatform.WindowsEditor) {
				WindowsController controller = currentPlayer.AddComponent<WindowsController>();
				controller.setJoyStickNumber(controllerNumber++);
			}

			// Setup player camera.
			Camera playerCam = currentPlayer.GetComponentInChildren<Camera>();
			playerCam.rect = cameraDetailsHorizontal[numberOfPlayers - 1, i];
			
			// Set layer of player model to cull
			int layer = LayerMask.NameToLayer("Player" + (i + 1));
			currentPlayer.layer = layer;
			Transform model = currentPlayer.transform.FindChild("Model");
			MoveToLayer(model, layer);

			// TODO: Figure out audio
			currentPlayer.SetActive(true);
		}
		
	}
	
	void MoveToLayer(Transform root, int layer) {
		root.gameObject.layer = layer;
		foreach(Transform child in root)
			MoveToLayer(child, layer);
	}

}
