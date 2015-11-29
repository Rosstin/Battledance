using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathBuilder : MonoBehaviour {

	public GameObject lineToRender;
	public GameObject player;

	LineRenderer myLineRenderer;

	List<Vector3> path = new List<Vector3>();

	// Use this for initialization
	void Start () {
		myLineRenderer = lineToRender.GetComponent<LineRenderer>();
		Instantiate (lineToRender, new Vector3(0,0,0), Quaternion.identity);
	}

	public void AddPathPoint ( Vector3 centerOfHex ){
		path.Add (centerOfHex);
		DrawNewPath ();
	}

	public void DrawNewPath(){

		if (path.Count > 1) {

			Vector3 lineStart = path[path.Count-2];
			Vector3 lineEnd = path[path.Count-1];


			myLineRenderer.SetVertexCount(path.Count);
			for(int i = 0; i < path.Count; i++){
				myLineRenderer.SetPosition(i, path[i]+new Vector3(0,Constants.LINE_LAYER_Y_OFFSET,0));
			}

			Debug.Log ("Draw line between... " + lineStart + ", " + lineEnd);

			Debug.Log ("put king sprite at beginning of path");
			player.transform.position = path[0]+new Vector3(0,Constants.SPRITE_LAYER_Y_OFFSET,0);

		}


	}

	public void DestroyPath(){
		Debug.Log ("Destroy path.");

		path = new List<Vector3>();
	}

	public void ExecutePath(){
		Debug.Log ("Execute path!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
