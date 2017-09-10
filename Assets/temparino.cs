using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temparino : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uvs = new Vector2[vertices.Length];

		for (int i = 0; i < vertices.Length; i++)
		{
			Debug.Log("Vertices[" + i + "]: " + vertices[i]);

			if (i > 0 && vertices[i].z - vertices[i - 1].z != 0 && vertices[i].x - vertices[i - 1].x == 0) {
				if(vertices[i].y - vertices[i - 1].y == 0) 
					uvs[i] = new Vector2(vertices[i].z, vertices[i].y * 10);
				else
					uvs[i] = new Vector2(vertices[i].y * 10, vertices[i].z);
			}
			else
				uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
		}
		
		mesh.uv = uvs;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
