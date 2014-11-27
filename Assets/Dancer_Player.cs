using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Dancer_Player : MonoBehaviour {

	public int creativity_index;

	public int characterNumber;
	public int score = 0;
	public float energy_level = 1f;
	public float stamina = 0.2f;
	//public float currentEnergyRate = 0.011f;
	public Animator dancerAnimator;
	//public static Dancer_Player dancerPlayerInstance;

	bool isAI = false;

	//Dictionary to handle creativity index
	Dictionary<int, int> creativityIndex;

	private int thePreviousDanceHash = 0;
	int[] danceMoves;

	public float unitTimeDiff;
	//LinkedList<int> danceMoves;

	void Awake(){


		dancerAnimator = GetComponent<Animator> ();
		this.creativityIndex = new Dictionary<int, int> ();
		//this.danceMoves = new LinkedList<int> ();
		//actually the bound is the number of danceMove
		danceMoves = new int[5]{-1,-1,-1,-1,-1};

		unitTimeDiff = 60f / (float)TapForBPM.globalBpm; 
	
	}

	//The Charater logic goes here
	/*
	 * ON ENERGY
	 * This is just to measure how much the frequency of change of dances
	 * 
	 * ON Creativity [the idea is to make the dance diverse]
	 * Everytime you dance a dance, attach a number a number to
	 * 	-At the end of the game, the dances should have equivalent numbers attached to them

	 * ON ACCURACY
	 * Time differences between the moves should be either *2 /2 *4 /4 etc... basically the timing of the music

	 * just brain dumping on here!

	 */
	

	public int calculateCreativityIndex(){
		Debug.Log (calcStdDev () * 10f);

		creativity_index = (int)Math.Ceiling(calcStdDev () * 10f);
		Debug.Log ("Standard Dev " +calcStdDev ());

		return creativity_index;
	
	}

	float calcStdDev(){
		
		float avg = calcAvg ();
		float variance = calcVariance (avg);
		
		float stdD = (float)Math.Sqrt (variance);
		
		return stdD;
		
	}

	int[] danceMoveFrequency;
	//I need refactor this function to be generic so it can calculate any kinda Average
	float calcAvg(){
		danceMoveFrequency = new int[creativityIndex.Count];

		//loop through the dictionary and calculate average for number of dances
		int sum = 0;
		int i = 0;
		foreach (KeyValuePair<int,int> creativePair in creativityIndex) {
		
			danceMoveFrequency[i] = creativePair.Value;
			sum += danceMoveFrequency[i];
			i++;

		}

		if (danceMoveFrequency.Length > 0) {
			return sum / danceMoveFrequency.Length;		
		} else {
			return 0;				
		}

	}

	//you can pass the list of values as arguements instead of globals ... its nicer :-)
	float calcVariance(float avg){

		float[] varianceNums = new float[danceMoveFrequency.Length];

		float sum = 0f;

		for (int i=0; i < varianceNums.Length; i++) {
				
			varianceNums[i] = (float)Math.Pow((danceMoveFrequency[i] - avg), 2);
			sum += varianceNums[i];
		
		}
		if (varianceNums.Length > 0)
			return sum / varianceNums.Length;
		return 0;

	}



	protected virtual void onEnergyChange(EventArgs e){

		EventHandler handler = energyChange;

		if (handler != null)
			handler (this, e);
	
	}

	public event EventHandler energyChange;




	int currentDanceMoveIndex = 0;
	float prevTime = 0f;
	public void changeDance(int danceHash, float transitionSpeed){


		//if the animation that was dropped is not the same as the previous then reset decreasing rate

		//the value for rate at which your energy reduces is actually related to the energy cost of the dance
		//related to the stamina of the player or dancer
		if (danceHash != thePreviousDanceHash) {

			dancerAnimator.CrossFade (danceHash, transitionSpeed);
			//this.currentEnergyRate = 0.01f;


			//creativity index preparation starting here
			if (this.creativityIndex.ContainsKey(danceHash)){
					
				this.creativityIndex[danceHash]++;//add one to this dictionary of this dancehash anytime you choose to dance it

			}else{

				this.creativityIndex.Add (danceHash, 0);

			}

			//this is to check and score you low if make predictable repeated moves
			if (danceMoves[currentDanceMoveIndex % 5] == danceHash){

				this.score += 5;

			}else{

				this.score += 10;

			}

			danceMoves[currentDanceMoveIndex % 5] = danceHash;

			currentDanceMoveIndex++;

			//if the time difference is the same as expected constant time diff from bpm
			//Or x2 x4 x8 x16
			//so basically check if the number is power of 2
			float currentTime = Time.time;
			float timeDiff = currentTime - prevTime;

			//if this timeDiff value is a power of 2 score 20 for timing

			//if timeDiff is a muliple of the unit beat time
			if (timeDiff % unitTimeDiff <= (unitTimeDiff/4) || (timeDiff % unitTimeDiff >= (unitTimeDiff/4))){

				this.score += 7;

			}

			//if the time diff is the product of the unitTime and a power of 2! awesome

			//if (timeDiff/unitTimeDiff)
			if ((Math.Log(timeDiff/unitTimeDiff) / Math.Log(2)) % 1 <= 0.2){

				this.score += 10;//awesome you really good at this

			}





		}

		thePreviousDanceHash = danceHash;

	}
}
