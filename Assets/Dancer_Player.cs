using UnityEngine;
using System.Collections;

public class Dancer_Player : MonoBehaviour {

	public int characterNumber;
	public int score = 0;
	public float energy_level = 1f;
	public float stamina = 0.2f;
	public float currentEnergyRate = 0.011f;
	public Animator dancerAnimator;
	public static Dancer_Player dancerPlayerInstance;

	bool isAI = false;

	private int thePreviousDanceHash = 0;

	void Awake(){

		dancerPlayerInstance = this;
		dancerAnimator = GetComponent<Animator> ();
	
	}

	// Use this for initialization
	void Start () {
	


	}

	public static Dancer_Player getDancerPlayerInstance(){

		return dancerPlayerInstance;

	}

	//The Charater logic goes here
	/*
	 * ON ENERGY
	 * Measure how much one move has been danced?
	 * Decrease energy by current rate of dancer
	 * Increase Decreasing rate as dance move is in progress
	 * 
	 * ON Creativity [the idea is to make the dance diverse]
	 * Everytime you dance a dance, attach a number a number to
	 * 	-At the end of the game, the dances should have equivalent numbers attached to them

	 * ON ACCURACY
	 * Time differences between the moves should be either *2 /2 *4 /4 etc... basically the timing of the music

	 * just brain dumping on here!

	 */

	// Update is called once per frame
	void Update () {
	


	}

	public void changeDance(int danceHash, float transitionSpeed){


		//if the animation that was dropped is not the same as the previous then reset decreasing rate

		//the value for rate at which your energy reduces is actually related to the energy cost of the dance
		//related to the stamina of the player or dancer
		if (danceHash != thePreviousDanceHash) {
			dancerAnimator.CrossFade (danceHash, transitionSpeed);
			currentEnergyRate = 0.01f;
			score += 10;
			
		}

		thePreviousDanceHash = danceHash;

	}
}
