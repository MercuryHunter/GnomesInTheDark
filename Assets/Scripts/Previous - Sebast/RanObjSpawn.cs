using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanObjSpawn : MonoBehaviour {


	public Material [] material;
	int randomNum;

	// Use this for initialization
	void Start () {

		randomNum = Random.Range (0, 4);

		gameObject.GetComponent<Renderer>().material =  material[randomNum];
	}
	

}
