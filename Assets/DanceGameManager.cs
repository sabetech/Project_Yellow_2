using UnityEngine;
using System.Collections;
using System.Linq;
using System;

//Thank you God for the work going on in this class and for all the interesting fixes 
public class DanceGameManager : MonoBehaviour {

	/*this is supposed to determine what happens at the start of Gameplay
	 *like the stopping the song and starting
	 *determinig the length of the dance and handling short songs
	 *single player against verse AI
	 *determine who goes first AI or human
	 *determine the scripts the characters get attached to them
	 *Instantiate the music visualizer on the stage with color cycle

	 *instantiating the right dancers
	 *instantiating the right dance moves
	 *handle BPM in it own space
	 *remember to show the record buttons on screen
	 */

	// Use this for initialization
	public static DanceGameManager danceGameManager = null;

	private MP3_Player myMp3Player;
	long musicLength;

	public GameObject[] danceCharacters;
	public Transform humanPos, aiPos;
	public static GameObject activeDancePlayer = null;
	
	private string[] aiDancemoves = {"Alkaeda", "pushDowns", "grabLeftnRight", "legShuffleLeftnRight", "pushUps"};//this should be more dynamic as AI becomes more complex and more dances are made

	public static int turn = 0; //used for determining whose turn it is to dance
	public static bool isGamePlaying = true;
	public bool isVersusAI = false;
	public static float danceSpeed = 0f;

	GameObject mainCamera;
	Transform trackingBodyPart;

	public static Animator activeAnimator; //helps modify the speed of a dancer from tapBPM class
	
	public GameObject ai_energyBar, ai_score;//visuals to monitor AI vitals
	public GameObject humanGameObject = null;
	public GameObject aiGameObject = null;
	
	public GameObject endGameWindow_singlePlayer;
	public GameObject endGameWindow_vrsAI;
	GameObject myAudioSource;

	string kamcordVideoTitle = "";

	void Awake(){
		if (danceGameManager == null) {

			danceGameManager = this;		
		
		} else {
			if(danceGameManager != this){

				Destroy(gameObject);//destroy the new one

			}
		}


		//First First, check if the song is long enough to be danced to.
		//if the song is more than 1 min its ok

		myMp3Player = MP3_Player.getMp3Instance ();

		TapForBPM.getGameTimerInstance ().turnChanged += turnChanged;
		TapForBPM.getGameTimerInstance ().gameFinished += danceFinished;



		//Find Main Camera
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		if (myMp3Player != null) {
			//restart whatever song that is playing...
			myMp3Player.stopFetchingIfGameStarted();
			myMp3Player.restart_Audio ();


		
			//Debug.Log ("The title name is " + myMp3Player.currentMp3Instance.getTitle ());
			//Debug.Log ("The file name is " + myMp3Player.currentMp3Instance.getAudioFileName ());
			//Debug.Log ("the bitrate is " + myMp3Player.currentMp3Instance.getBitrate ());

			//find the audio source
			//bring it to where the main camera is
			//make it a child of the main camera

			myAudioSource = GameObject.Find("AudioPlayerSource");
			myAudioSource.transform.position = mainCamera.transform.position;
			myAudioSource.transform.parent = mainCamera.transform;
			//This is done just so you don't hear very wierd sounds because of the position of the camera
			//and the audio source object
		}


		//get playerprefs infomation ie character, dance, AI or Not,

		int selectedCharacter = PlayerPrefs.GetInt("chosenPlayer", 0);

		if (PlayerPrefs.GetInt ("isVerseAI", 0) == 1) {//PlayerPrefs.GetInt ("isVerseAI", 0) == 1
			isVersusAI = true;
			kamcordVideoTitle = "Dancing with the Boogie Loops Azonto AI";
			//instantiate player and AI
			humanGameObject = Instantiate(danceCharacters[selectedCharacter], humanPos.transform.position, Quaternion.identity) as GameObject;
			
			aiGameObject = selectAI_character(selectedCharacter);

			//actually add the necessary components
			humanGameObject.AddComponent<DanceDropSurface>();
			aiGameObject.AddComponent<DancerAI>().AvailableDances = aiDancemoves;
						
		} else {
			kamcordVideoTitle = "Great Azonto Moves with Boogie Loops";
			//instantiate the only player
			humanGameObject = Instantiate(danceCharacters[selectedCharacter], humanPos.transform.position, Quaternion.identity) as GameObject;
			humanGameObject.AddComponent<DanceDropSurface>();
		}
		
		activeDancePlayer = humanGameObject;
		activeAnimator = activeDancePlayer.GetComponent<Animator> ();

		positionCamera_reset (humanGameObject);

	}

	GameObject selectAI_character(int selectedCharacter){

		//try and make sure AI and play do not select same character
		int aiCharacter = UnityEngine.Random.Range(0, danceCharacters.Length);
		if (aiCharacter == selectedCharacter){

			aiCharacter = (selectedCharacter + 1) % (danceCharacters.Length-1);

		}
		
		return Instantiate(danceCharacters[aiCharacter], aiPos.transform.position, Quaternion.identity) as GameObject;

	}

	void positionCamera_reset(GameObject focusedCharacter){
		//position camera
		trackingBodyPart = FindInAllChildren(focusedCharacter.transform,"Spine").transform;
		
		Vector3 cameraInitialPosition = new Vector3 (trackingBodyPart.position.x,
		                                             trackingBodyPart.position.y,
		                                             trackingBodyPart.position.z + 3.5f);
		
		TweenPosition.Begin (mainCamera, 2f, cameraInitialPosition);
	
	}

	void Start(){

		turn = 0;
		isGamePlaying = true;
		Kamcord.StartRecording ();
	}

	//game manager is going to take care of the camera movements and such
	// Update is called once per frame
	void Update () {

		if (activeDancePlayer != null)
			mainCamera.transform.LookAt (trackingBodyPart);

		//be checking the length of the audio ... if audio length is up restart


	}

	public Transform FindInAllChildren(Transform root, string name){

		foreach (Transform t in root.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == name)) {
			return t;		
		}
		//if you dont find the name
		return null;
	}


	//this is called by the dance length timer
	void changeTurn(){

		//change turn

		if (isVersusAI) {

			if (turn == 0){
				//give the human player's speed to AI;
				aiGameObject.GetComponent<Animator>().enabled = true;
				aiGameObject.GetComponent<Animator> ().speed = activeAnimator.speed;
				//hide the dance moves widgies
				hideMovesWidgies();
				//hideBpmInfo();
				hidePlayerVitals();

				showAIVitals();

				//change camera focus
				positionCamera_reset (aiGameObject);
				humanGameObject.GetComponent<Animator>().enabled = false;
				turn = 1;

			}else{

				hideAIVitals();

				showMovesWidgies();

				showPlayerVitals();
				humanGameObject.GetComponent<Animator>().enabled = true;
				aiGameObject.GetComponent<Animator>().enabled = false;
				//change camera focus
				positionCamera_reset(humanGameObject);

				turn = 0;

			}
				
		}
	}

	void hideAllUIWidgies(){

		hideMovesWidgies ();
		hidePlayerVitals ();
		hideAIVitals ();

		Pause.pauseInstance.hidePauseButton ();

	}

	public void hideMovesWidgies(){
		//hide the dance moves widgies
		HideMovesWidgets.getMovesWidgetInstance().hideMovesWidgets();

	}

	public void showMovesWidgies(){

		HideMovesWidgets.getMovesWidgetInstance ().bringBackWidgets ();
	
	}

	void hideBpmInfo(){

		TapForBPM.getGameTimerInstance ().hideBPMInfo ();

	}

	void showBpmInfo(){

		TapForBPM.getGameTimerInstance ().bringBackBPMInfo ();

	}

	void hidePlayerVitals() {
		//Energy info will be in version 2
		//EnergyBar_Visual.getEnergyBarInstance ().hideEnergyBar ();
		ScoreBar_Visual.getScoreVisual ().hideScoreVisual ();
	
	}

	void showPlayerVitals(){
		//version 2
		//EnergyBar_Visual.getEnergyBarInstance ().bringBackEnergyBar ();
		ScoreBar_Visual.getScoreVisual ().bringBackScoreVisual ();
	
	}

	void hideAIVitals(){

		//EnergyBar_AI.getEnergyBarInstance ().hideEnergyBar ();
		ScoreBar_AI.getScoreVisual ().hideScoreVisual ();
	
	}

	void showAIVitals(){

		//EnergyBar_AI.getEnergyBarInstance ().bringBackEnergyBar ();
		ScoreBar_AI.getScoreVisual ().bringBackScoreVisual ();

	}
	

	//this is an event handler
	void turnChanged(object sender, EventArgs e){

		changeTurn ();

	}

	void danceFinished(object sender, EventArgs e){

		Kamcord.Snapshot ("screenShot.png");

		endGame ();

	}
	


	void SnapshotReadyAtFilePath(string filepath){

		Debug.Log (filepath);
	
	}

	void endGame(){
		Kamcord.SetVideoTitle (kamcordVideoTitle);

		//disabling dancers
		humanGameObject.GetComponent<Animator> ().enabled = false;
		if(aiGameObject != null)//that is saying if i played with AI
			aiGameObject.GetComponent<Animator> ().enabled = false;

		isGamePlaying = false;
		//hide screen controls etc
		hideAllUIWidgies ();

		if (!isVersusAI) {

			endGameWindow_singlePlayer.SetActive (true);
			//showing the end game modal so set something active [Fade In trick]
		} else {

			endGameWindow_vrsAI.SetActive(true);

		}

		TapForBPM.getGameTimerInstance ().turnChanged -= turnChanged;
		TapForBPM.getGameTimerInstance ().gameFinished -= danceFinished;

		Kamcord.StopRecording ();

	}

	public void pauseGame(){
		MP3_Player.mp3Instance.pause_audio ();
		hideMovesWidgies ();

		//disabling dancers
		humanGameObject.GetComponent<Animator> ().enabled = false;
		if(aiGameObject != null)//that is saying if i played with AI
			aiGameObject.GetComponent<Animator> ().enabled = false;

		TapForBPM.getGameTimerInstance ().bpmAnimation.enabled = false;
		TapForBPM.getGameTimerInstance ().pausePlaying = true;

		Kamcord.Pause ();
		Time.timeScale = 0f;
	
	}

	void OnApplicationPause(){

		pauseGame ();

	}

	public void resumeGame(){
		MP3_Player.mp3Instance.play_Paused_audio ();
		showMovesWidgies ();

		//disabling dancers
		humanGameObject.GetComponent<Animator> ().enabled = true;
		if(aiGameObject != null)//that is saying if i played with AI
			aiGameObject.GetComponent<Animator> ().enabled = true;

		TapForBPM.getGameTimerInstance ().bpmAnimation.enabled = true;
		TapForBPM.getGameTimerInstance ().pausePlaying = false;

		Kamcord.Resume ();
		Time.timeScale = 1f;
	
	}

	public static void changeSpeedOfActiveDance(float danceSpeed){

		activeAnimator.speed = danceSpeed;

	}

	public void changeScene(int sceneNum){
		myAudioSource.transform.parent = null;

		restart (); 

		Application.LoadLevel (sceneNum);

	}

	public void restart(){
		MP3_Player.mp3Instance.play_Paused_audio ();
		//enabling dancers
		humanGameObject.GetComponent<Animator> ().enabled = true;
		if(aiGameObject != null)//that is saying if i played with AI
			aiGameObject.GetComponent<Animator> ().enabled = true;
		
		TapForBPM.getGameTimerInstance ().bpmAnimation.enabled = true;
		TapForBPM.getGameTimerInstance ().pausePlaying = false;
		Time.timeScale = 1f;

	}
}
