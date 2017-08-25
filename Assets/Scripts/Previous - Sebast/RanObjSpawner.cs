using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanObjSpawner : MonoBehaviour {

	public GameObject[] objects;
	
	public static int maxSpawnRadius = 14;
	public int MaxObjects = 12;

	private int radius = 0;
	private int ranObj = 0;


	void Start () {

		for (int i = 0; i < MaxObjects; i++) {
			radius = Random.Range (1, maxSpawnRadius);//Random ranges
			ranObj = Random.Range(0, 3);

			Vector3 spawnPos = transform.position;

			//spawnPos += new Vector3 (Mathf.Sin ((Mathf.PI * 2 / MaxObjects) * i), 0 , Mathf.Cos (Mathf.PI * 2 / MaxObjects)) * radius;
			spawnPos += new Vector3 (Mathf.Sin ((Mathf.PI * 2 / MaxObjects) * i), 0 , Mathf.Cos ((Mathf.PI * 2 / MaxObjects) * i)) * radius;

			//Make Object
			Instantiate(objects[ranObj], spawnPos, transform.rotation);

		}

	}
	

}
