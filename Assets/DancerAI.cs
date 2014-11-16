using UnityEngine;
using System.Collections;

public class DancerAI : Dancer_Player {

	// Use this for initialization
	//This is the dancer AI ... this is my first attempt in life at AIs
	//be randomly be changing the dance moves in a timely accurate manner
	//probably be using a state machines
	//The states will be :
		//trying to beat creativity index
		//doing double moves to beat the current score
		//saving energy by switching moves in time

	//check whether it is the turn of the AI to dance

	enum aiDanceStates{

		enhanceCreativity, //when current creativity is higher than opponent
		beatScore,
		increaseEnergy

	}

	public string[] AvailableDances; //this will change once we have different Major dances
	private int[] danceHashes; //this for speed

	int initialDanceMove;
	float theMusicBPM;
	float timeDifference;
	int numOfMoves = 4;

	void Start () {
		danceHashes = new int[AvailableDances.Length];
		initialDanceMove = Random.Range (0, 2);
	
		//get time unit time difference for the music
		timeDifference = 60f / (float)TapForBPM.globalBpm; 

		for (int i=0; i<AvailableDances.Length; i++) {
				
			danceHashes[i] = Animator.StringToHash(AvailableDances[i]);
		
		}

	}

	float prevTimeTime = 0f;
	float currentTimeDiff,currentTime = 0f;
	int currentBeatWaitTime = 0;
	int preferredBeatWaitTime = 4;

	// Update is called once per frame
	void Update () {

		currentTime = Time.time;
		currentTimeDiff = currentTime - prevTimeTime;

		//this must be happening in a current state;
		//and is running every unit beat time
		if (currentTimeDiff >= timeDifference){

			//in this loop you decide to change move or not
			currentBeatWaitTime++;
			if ( currentBeatWaitTime == preferredBeatWaitTime ){

				//change the dance move of AI
				int nextDance = Random.Range(0, danceHashes.Length-1);
				Debug.Log (AvailableDances[nextDance]);
				this.changeDance(danceHashes[nextDance], 0.1f);

				currentBeatWaitTime = 0;

			}

			prevTimeTime = Time.time;
		}



	}
}
