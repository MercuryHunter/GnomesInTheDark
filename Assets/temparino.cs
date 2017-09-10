﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temparino : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uvs = new Vector2[vertices.Length];

		int count = 0, slowCount = 0;
		int slowCountTick = 3;
		for (int i = 0; i < vertices.Length; i++)
		{
			Debug.Log("Vertices[" + i + "]: " + vertices[i]);
			switch (count) {
				case 0:
					uvs[i] = new Vector2(0, (1f / slowCountTick) * slowCount);
					break;
				case 1:
					uvs[i] = new Vector2(0, (1f / slowCountTick) * (slowCount + 1));
					break;
				case 2:
					uvs[i] = new Vector2(3, (1f / slowCountTick) * slowCount);
					break;
				case 3:
					uvs[i] = new Vector2(3, (1f / slowCountTick) * (slowCount + 1));
					break;
			}
			if(i % slowCountTick == slowCountTick - 1)
				slowCount = (slowCount + 1) % slowCountTick;
			count = (count + 1) % 4;
		}
		
		mesh.uv = uvs;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
