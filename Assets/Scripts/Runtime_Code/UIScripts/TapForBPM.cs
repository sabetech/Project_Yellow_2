using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

public class TapForBPM : MonoBehaviour {

	private UILabel currentBpm;
	public Animator bpmAnimation;
	private int bpmAnimationHash;
	public static decimal globalBpm = 125.000m;
	public static decimal prevGlobalBpm = 125.000m;
	private float timeleft;//total time to be used currently at 45 secs for test
	private float totalTime;
	private float swapTimer;

	public UISprite timeLeftSprite;//set this in settings :D 2mins left

	private static TapForBPM gameTimerInstance = null;

	void Awake(){

		gameTimerInstance = this;
		string danceTime = PlayerPrefs.GetString("danceTime", "2");
		try{

			totalTime = float.Parse (danceTime, CultureInfo.InvariantCulture.NumberFormat) * 60f;
			timeleft =  float.Parse (danceTime, CultureInfo.InvariantCulture.NumberFormat) * 60f;
			swapTimer = float.Parse (danceTime, CultureInfo.InvariantCulture.NumberFormat) * 60f;
			//Debug.Log (danceTime);

			//Debug.Log ("lenght of time amdancing "+ totalTime);

		}catch(Exception){

			totalTime = 120f;
			timeleft = 120f;
			swapTimer = 120f;
		
		}

	}

	public static TapForBPM getGameTimerInstance(){

		return gameTimerInstance;
	
	}
	
	// Use this for initialization
	void Start () {

		currentBpm = GetComponentInChildren<UILabel> ();
		bpmAnimation = GetComponentInChildren<Animator> ();
		bpmAnimationHash = Animator.StringToHash ("BeatRythmLoop");
		globalBpm = 125.000m;
		prevGlobalBpm = 125.000m;

		try{
			currentBpm.text = globalBpm +"";
			/*decimal audTempo = MP3_Player.getMp3Instance ().currentMp3Instance.getTempo ();

			if (audTempo == 0m) {
				
				currentBpm.text = globalBpm + "";
				
			} else {
				
				currentBpm.text = audTempo + "";
				
			}*/

		}catch(Exception){
			Debug.Log ("yet to get heree");		
		}

	}

	float previousTimetime = 0f;
	bool stopPlaying = false;
	public bool pausePlaying = false;
	// Update is called once per frame
	void Update () {

		if (stopPlaying)
			return;

		if (pausePlaying)
			return;

		if (previousTimetime <= 0f) {

			previousTimetime = Time.time;
			return;
		}

		timeleft = Math.Abs ((Time.time - previousTimetime) - totalTime); 

		timeLeftSprite.fillAmount = (1 * timeleft) / totalTime;

		if (timeleft <= 0.1f) {
			stopPlaying = true;

			//throw an event to stop the game
			//disable the animator for the tempo visual
			GetComponentInChildren<Animator>().enabled = false;
			hideBPMInfo();
			onGameFinished(EventArgs.Empty);
			
		}

		//over here, check the time left is 60 seconds already
		//if so try and swap players ie if it is versus AI

		if ((swapTimer - timeleft) >=  60f){//make this 1 min

			swapTimer = timeleft;
			//throw an event to swap player
			onTurnChanged(EventArgs.Empty);
		}

		//remember slowly reduce the volume of the song when it is coming to end

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

		//Mathf.clamp the bpm animation speed here
		instantBpm = Mathf.Clamp((60f / timeDiff ), 40f, 159f); //max and min bpm so u don't get crazy dance speeds


		decimalValue = Math.Round((decimal)instantBpm, 3);


		Debug.Log (decimalValue);

		pushIntoBpmArray (decimalValue);

		decimal avgOfBpm = calcAvg ();
		globalBpm = Math.Round (avgOfBpm, 3);
		currentBpm.text =  globalBpm+ " ";



		bpmAnimation.speed = ((bpmAnimation.speed * (float)TapForBPM.globalBpm) / (float)TapForBPM.prevGlobalBpm);


		//Debug.Log (bpmAnimation.speed);

		float dancerAnimatorSpeed = DanceGameManager.activeAnimator.speed;

		//modify dance speed here!!!; here here
		dancerAnimatorSpeed = ((dancerAnimatorSpeed * (float)TapForBPM.globalBpm) / (float)TapForBPM.prevGlobalBpm);

		DanceGameManager.changeSpeedOfActiveDance (dancerAnimatorSpeed);

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

	public void hideBPMInfo(){

		this.gameObject.SetActive (false);
	
	}

	public void bringBackBPMInfo(){

		this.gameObject.SetActive (true);

	}


	protected virtual void onTurnChanged(EventArgs e){

		EventHandler handler = turnChanged;
		if (handler != null) {
				
			handler(this, e);

		}

	}

	protected virtual void onGameFinished(EventArgs e){

		EventHandler handler = gameFinished;
		if (handler != null) {
				
			handler(this, e);
		
		}

	}
	
	public event EventHandler turnChanged;
	public event EventHandler gameFinished;
	
}
