using UnityEngine;
using System.Collections;

public class CharacterPositionController : MonoBehaviour {

	SpriteRenderer mySpriteRenderer;

	bool fadingOut;
	bool fadingIn;
	bool fading;

	/*
	 * First, fade the player out. Then, fade them in at the new position.
	 **/
	void TeleportPlayerToThisPosition(){
		FadeOut ();
	}

	/* 
	 * Nicely translate the player to a new position
	 **/
	//void MovePlayerToThisPosition

	/*
	 * Fade self out
	 **/
	public void FadeOut () {
		fading = true;
		fadingOut = true;
	}

	/*
	 * Fade self in
	 **/
	public void FadeIn () {
		fading = true;
		fadingIn = true;
	}


	// Use this for initialization
	void Start () {
		mySpriteRenderer = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fading == true && fadingOut == true && mySpriteRenderer.color.a > 0) {
			FadeOutStep();
		}
		if (fading == true && fadingIn == true && mySpriteRenderer.color.a < 1) {
			FadeInStep();
		}

	}

	void FadeOutStep() { //TODO change this to be handled properly with timesteps
		mySpriteRenderer.color = new Color(1f, 1f, 1f, mySpriteRenderer.color.a - Constants.FADE_SPEED);
	}

	void FadeInStep() { //TODO change this to be handled properly with timesteps
		mySpriteRenderer.color = new Color(1f, 1f, 1f, mySpriteRenderer.color.a + Constants.FADE_SPEED);
	}


}
