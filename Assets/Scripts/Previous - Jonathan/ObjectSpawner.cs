using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

	public Object[] spawnableObjects;
	public Material material1, material2, material3;
	public int amountToSpawn;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < amountToSpawn; i++) {
			Vector3 randomPosition = new Vector3 (Random.Range(-24, 25), 2, Random.Range(-24, 25));
			if (randomPosition.x < 0.5 && randomPosition.x > -0.5)
				randomPosition.x += 3;
			if (randomPosition.y < 0.5 && randomPosition.y > -0.5)
				randomPosition.y += 3;
			
			GameObject o = (GameObject)Instantiate (spawnableObjects [Random.Range (0, spawnableObjects.Length)], randomPosition, Quaternion.identity);
			if (o.tag == "NeedsTexture") {
				int random = Random.Range (1, 4);
				if(random == 1)
					o.GetComponent<MeshRenderer> ().material = new Material (material1);
				if(random == 2)
					o.GetComponent<MeshRenderer> ().material = new Material (material2);
				if(random == 3)
					o.GetComponent<MeshRenderer> ().material = new Material (material3);
			}
		}
	}
	
}
