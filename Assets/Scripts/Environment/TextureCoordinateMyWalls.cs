using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCoordinateMyWalls : MonoBehaviour {

	public int slowCountTick = 1;
	public float yRepetitions = 1;
	
	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uvs = new Vector2[vertices.Length];

		int count = 0, slowCount = 0;
		for (int i = 0; i < vertices.Length; i++)
		{
			switch (count) {
				case 0:
					// Top Left
					uvs[i] = new Vector2(0, (1f / slowCountTick) * slowCount);
					break;
				case 1:
					// Top Right
					uvs[i] = new Vector2(0, (1f / slowCountTick) * (slowCount + 1));
					break;
				case 2:
					// Bottom Left
					uvs[i] = new Vector2(yRepetitions, (1f / slowCountTick) * slowCount);
					break;
				case 3:
					// Bottom Right
					uvs[i] = new Vector2(yRepetitions, (1f / slowCountTick) * (slowCount + 1));
					break;
			}
			if(i % slowCountTick == slowCountTick - 1)
				slowCount = (slowCount + 1) % slowCountTick;
			count = (count + 1) % 4;
		}
		
		mesh.uv = uvs;
		mesh.RecalculateNormals();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
