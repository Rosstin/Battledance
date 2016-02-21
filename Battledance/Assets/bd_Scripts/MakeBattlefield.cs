using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeBattlefield : MonoBehaviour {

	public GameObject battlespace;
	public GameObject battlefield;
	//public float battlespaceResizeConstant = Constants.BATTLESPACE_RESIZE_CONSTANT; //try not to use this

	// Use this for initialization
	void Start () {
		Debug.Log ("draw a hex grid");

		Point gridSize = new Point (Constants.GRID_SPACING, Constants.GRID_SPACING);
		Point gridOrigin = new Point (0, 0);

		// get the battlespace list
		BattlefieldController battlefieldController = battlefield.GetComponent<BattlefieldController>();

		// initialize the grid then draw it
		HexLayout gameGrid = new HexLayout (HexLayout.flat, gridSize, gridOrigin);

		for (int i = -Constants.GRID_RADIUS; i < Constants.GRID_RADIUS; i++) {
			for (int j = -Constants.GRID_RADIUS; j < Constants.GRID_RADIUS; j++) {
				Hex givenHex = OffsetCoord.QoffsetToCube(0, new OffsetCoord(i,j));
				Point screenPoint = HexLayout.HexToPixel (gameGrid, givenHex);
				//Debug.Log ("given hex: " + givenHex.q + ", " + givenHex.r + ", " + givenHex.s + "... " + "given point for hex: "+screenPoint.x + ", " + screenPoint.y );
				// now draw these hexes

				battlespace.transform.position = new Vector3((float)screenPoint.x, 0.0f, (float)screenPoint.y);

				GameObject newBattlespace = Instantiate (battlespace);
				newBattlespace.GetComponent<BattlespaceController>().hex = givenHex;

				// put the prisms in an array
				battlefieldController.battlespaceControllerList.Add (battlespace);

				// then make them more special

			}
		}





	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
