using UnityEngine;
using System.Collections;

public class BattlespaceController : MonoBehaviour {

	public GameObject pathBuilder;
	public PathBuilder pathBuilderScript;

	public Hex hex;

	// Use this for initialization
	void Start () {
		pathBuilderScript = pathBuilder.GetComponent<PathBuilder>();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if ( Input.GetMouseButtonDown(0)){

			RaycastHit hit = new RaycastHit();

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Transform select = GameObject.FindWithTag("select").transform;
			if(Physics.Raycast(ray, hit, 100.0f)){
				select.tag = "none";
				hit.collider.transform.tag = "select";
			}
		}
		*/
	}


	// PATH RELATED CODE
	void OnMouseDown() {
		pathBuilderScript.AddPathPoint (this.gameObject);
		Debug.Log ("You clicked me. I'm the start of a path... I'm " + this.transform.position.x + ", " + this.transform.position.y);
	}

	void OnMouseEnter() {
		//Debug.Log ("OnMouseEnter... I'm " +  this.transform.position.x + ", " + this.transform.position.y);
		if (Input.GetMouseButton(0)) {
			pathBuilderScript.AddPathPoint (this.gameObject);
			Debug.Log ("you entered me and you're clicking... I'm " + this.transform.position.x + ", " + this.transform.position.y);
		}
	}

	void OnMouseExit() {
		//Debug.Log ("OnMouseExit... I'm " +  this.transform.position.x + ", " + this.transform.position.y);
		if (Input.GetMouseButton(0)) {
			Debug.Log ("you left me and you're clicking... I'm " + this.transform.position.x + ", " + this.transform.position.y);
		}
	}

	void OnMouseUp(){
		Debug.Log ("you let go. I'm the end of a path... I'm " + this.transform.position.x + ", " + this.transform.position.y);
		pathBuilderScript.ExecutePath ();
		pathBuilderScript.DestroyPath ();
	}

}
