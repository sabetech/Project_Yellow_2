using UnityEngine;
using System.Collections;
using System;

public class TapForBPM : MonoBehaviour {

	private UILabel currentBpm;
	private Animator bpmAnimation;
	private int bpmAnimationHash;
	public static decimal globalBpm = 125.000m;
	public static decimal prevGlobalBpm = 125.000m;
	private float timeleft = 45f;//total time to be used currently at 45 mins for test

	public UISprite timeLeftSprite;//set this in settings :D 2mins left

	// Use this for initialization
	void Start () {

		currentBpm = GetComponentInChildren<UILabel> ();
		bpmAnimation = GetComponentInChildren<Animator> ();
		bpmAnimationHash = Animator.StringToHash ("BeatRythmLoop");

	}

	float previousTimetime = 0f;
	bool stopPlaying = false;
	// Update is called once per frame
	void Update () {

		if (stopPlaying)
			return;

		if (previousTimetime <= 0f) {

			previousTimetime = Time.time;
			return;
		}

		timeleft = Math.Abs ((Time.time - previousTimetime) - timeleft); 

		timeLeftSprite.fillAmount = (1 * timeleft) / timeleft;


		if (timeleft <= 2f) {
			stopPlaying = true;
			//Kamcord.StopRecording();		
			//Kamcord.ShowView();

		}

	}

	float prevTime = 0f;
	float currentTime;
	float timeDiff;
	float prevTimeDiff = 0f;

	//beat detection by tapping
	bool isFirstClick = true;
	float instantBpm = 0f;
	decimal decimalValue;
	int currentIndex = 0;
	decimal[] bpmValues = new decimal[15];

	void OnClick(){

		bpmAnimation.Play (bpmAnimationHash, -1, 0f);

		currentTime = Time.time;

		timeDiff = currentTime - prevTime;

		prevTime = currentTime;

		//if the current time difference and the previous time difference r very wide
		if (timeDiff >= (prevTimeDiff * 2)) {
			Debug.Log ("u have rested ur taps");	
			isFirstClick = true;
		}

		prevTimeDiff = timeDiff;

		//I need at least two taps to calculate bpm
		if (isFirstClick) {
			isFirstClick = false;
			return;
		}

		instantBpm = (60f / timeDiff );
		decimalValue = Math.Round((decimal)instantBpm, 3);

		pushIntoBpmArray (decimalValue);

		decimal avgOfBpm = calcAvg ();
		globalBpm = Math.Round (avgOfBpm, 3);
		currentBpm.text =  globalBpm+ " ";

		bpmAnimation.speed = ((bpmAnimation.speed * (float)TapForBPM.globalBpm) / (float)TapForBPM.prevGlobalBpm);
		float dancerAnimatorSpeed = Dancer_Player.getDancerPlayerInstance ().dancerAnimator.speed;

		//modify dance speed here!!!;
		dancerAnimatorSpeed = ((dancerAnimatorSpeed * (float)TapForBPM.globalBpm) / (float)TapForBPM.prevGlobalBpm);

		TapForBPM.prevGlobalBpm = TapForBPM.globalBpm;

	}

	void pushIntoBpmArray(decimal bpmValue){

		bpmValues [currentIndex % 15] = bpmValue;
		currentIndex++;

	}

	decimal calcAvg(){
		decimal sum = 0;

		for (int i=0; i < ((currentIndex <= 15) ? currentIndex : 15); i++) {
				
			sum += bpmValues[i];
		
		}

		return sum/((currentIndex <= 15) ? currentIndex : 15);

	}
	
}
