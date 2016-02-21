using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathBuilder : MonoBehaviour {

	public GameObject lineToRender;
	public GameObject player;
	public GameObject sceneGod;

	LineRenderer myLineRenderer;
	MakeBattlefield myMakeBattlefield;

	List<GameObject> path = new List<GameObject>();

	// Use this for initialization
	void Start () {
		myMakeBattlefield = sceneGod.GetComponent<MakeBattlefield> ();
		myLineRenderer = lineToRender.GetComponent<LineRenderer>();
		Instantiate (lineToRender, new Vector3(0,0,0), Quaternion.identity);
	}

	public void AddPathPoint ( GameObject battlespace ){
		if (path.Count > 0) {
			Hex last = path [path.Count - 1].GetComponent<BattlespaceController> ().hex;
			Hex newest = battlespace.GetComponent<BattlespaceController> ().hex;
			List<Hex> hexes = PathFinder.findPath (last, newest);
			for (int i = 1; i < hexes.Count; ++i) {
				path.Add (myMakeBattlefield.hexToObj [hexes [i]]);
			}
			/*
			int distance = Hex.Distance(newest, last);
			Debug.Log ("path:" + hexes.Count);
			Debug.Log ("Distance between" +last.q+","+last.r+","+last.s+" and "+newest.q+","+newest.r+","+newest.s+" = "+distance);
			if (distance != 1) {
				return;
			}
			*/
		} else {
			path.Add (battlespace);
		}
		DrawNewPath ();
	}

	public void DrawNewPath(){

		if (path.Count > 1) {

			GameObject lineStart = path[path.Count-2];
			GameObject lineEnd = path[path.Count-1];

			myLineRenderer.SetVertexCount(path.Count);
			for(int i = 0; i < path.Count; i++){ //todo this offset is messy
				myLineRenderer.SetPosition(i, path[i].transform.position +new Vector3(0,Constants.LINE_LAYER_Y_OFFSET,0));
			}

			//Debug.Log ("Draw line between... " + lineStart + ", " + lineEnd);

			//Debug.Log ("put king sprite at beginning of path");
			player.transform.position = path[0].transform.position+new Vector3(0,Constants.SPRITE_LAYER_Y_OFFSET,0);

		}


	}

	public void DestroyPath(){
		//Debug.Log ("Destroy path.");
		path = new List<GameObject>();
	}

	public void ExecutePath(){
		player.GetComponent<CharacterPositionController> ().TeleportPlayerToThisPosition(path[path.Count-1].transform.position+new Vector3(0,Constants.SPRITE_LAYER_Y_OFFSET,0));
		Debug.Log ("Execute path! Teleport player to: " + path[path.Count-1].transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
