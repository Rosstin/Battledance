using UnityEngine;
using System.Collections;

public class CreateBoxMesh : MonoBehaviour {
	
	public float width = 5f;
	public float height = 5f;
	public float depth = 5f;
	
	// Use this for initialization
	void Start () {
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;
		
		// Vertices
		Vector3[] vertices = new Vector3[8]
		{
			new Vector3 (0, 0, 0),
			new Vector3 (0, height, 0),
			new Vector3 (width, height, 0),
			new Vector3 (width, 0, 0),
			new Vector3 (width, 0, depth),
			new Vector3 (width, height, depth),
			new Vector3 (0, height, depth),
			new Vector3 (0, 0, depth)
		};
		
		// Triangles
		int[] tri = new int[36]{
			0, 1, 3, //   1: face arrière
			1, 2, 3,
			3, 2, 5, //   2: face droite
			3, 5, 4,
			5, 2, 1, //   3: face dessue
			5, 1, 6,
			3, 4, 7, //   4: face dessous
			3, 7, 0,
			0, 7, 6, //   5: face gauche
			0, 6, 1,
			4, 5, 6, //   6: face avant
			4, 6, 7
		};
		
		// Normals (only if you want to display the object.. yes!)
		/*
		Vector3[] normals = new Vector3[4]{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};
		*/
		
		// UVs (how textures are displayed)
		/*
		Vector2[] uv = new Vector2[4]{
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(0,1),
			new Vector2(1,1)
		};*/
		
		// Assign Arrays
		mesh.vertices = vertices;
		mesh.triangles = tri;
		//mesh.normals = normals;
		//mesh.uv = uv;
		
		mesh.RecalculateNormals ();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
