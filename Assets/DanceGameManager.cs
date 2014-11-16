using UnityEngine;
using System.Collections;
using System.Linq;

public class DanceGameManager : MonoBehaviour {

	//this is supposed to determine what happens at the start of Gameplay
	//like the stopping the song and starting
	//determinig the length of the dance and handling short songs
	//single player against verse AI
	//determine who goes first AI or human
	//determine the scripts the characters get attached to them
	//instantiate the music visualizer on the stage with color cycle
	//
	//instantiating the right dancers
	//instantiating the right dance moves
	//handle BPM in it own space
	//remember to show the record buttons on screen
	//

	// Use this for initialization

	private MP3_Player myMp3Player;
	long musicLength;
	private bool isVerseAI = false;
	public GameObject[] danceCharacters;
	public Transform humanPos, aiPos;
	public static GameObject activeDancePlayer = null;

	private string[] aiDancemoves = {"Alkaeda", "pushDowns", "grabLeftnRight", "legShuffleLeftnRight"};

	public static int turn = 0; //used for determining whose turn it is to dance
	GameObject mainCamera;
	Transform trackingBodyPart;

	void Awake(){

		//First First, check if the song is long enough to be danced to.
		//if the song is more than 1 min its ok
		
		//restart whatever song that is playing...
		myMp3Player = MP3_Player.getMp3Instance ();
		//myMp3Player.restart_Audio ();
		
		
		if (myMp3Player != null) {
			musicLength = myMp3Player.currentMp3Instance.getAudioLength ();
			
			Debug.Log ("The length is " + musicLength);
			Debug.Log ("The title name is " + myMp3Player.currentMp3Instance.getTitle ());
			Debug.Log ("The file name is " + myMp3Player.currentMp3Instance.getAudioFileName ());
			Debug.Log ("the bitrate is " + myMp3Player.currentMp3Instance.getBitrate ());
		}
		
		//if (musicLength < 60L) {
		//	Debug.Log ("handle short songs or no length");
		//SO WE ARE NOT USING THE LENGTH OF THE MUSIC TO DETERMINE HOW LONG THE DANCE SHOULD TAKE!!
		//}
		//get playerprefs infomation ie character, dance, AI or Not,
		
		//Get Audio Length
		Debug.Log ("How long are your dancing for "+PlayerPrefs.GetInt ("dance_length",0));
		
		Debug.Log ("Is verse AI? " + PlayerPrefs.GetInt ("isVerseAI", 1));
		Debug.Log ("What Dancer? " + PlayerPrefs.GetInt ("Dancer" ,0));
		
		GameObject humanGameObject = null;
		GameObject aiGameObject = null;
		
		int selectedCharacter = PlayerPrefs.GetInt("chosenPlayer",3);
		
		if (true) {//PlayerPrefs.GetInt ("isVerseAI", 1) == 1
			isVerseAI = true;
			
			//instantiate player and AI
			humanGameObject = Instantiate(danceCharacters[selectedCharacter], humanPos.transform.position, Quaternion.identity) as GameObject;
			
			aiGameObject = selectAI_character(selectedCharacter);
			
			//actually add the necessary components
			humanGameObject.AddComponent<DanceDropSurface>();
			aiGameObject.AddComponent<DancerAI>().AvailableDances = aiDancemoves;

			
		} else {
			
			//instantiate the only player
			humanGameObject = Instantiate(danceCharacters[selectedCharacter], humanPos.transform.position, Quaternion.identity) as GameObject;
			humanGameObject.AddComponent<DanceDropSurface>();
		}
		
		activeDancePlayer = humanGameObject;
		
		//position camera 
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		trackingBodyPart = FindInAllChildren(humanGameObject.transform,"Spine").transform;

		Vector3 cameraInitialPosition = new Vector3 (trackingBodyPart.position.x,
		                                             trackingBodyPart.position.y,
		                                             trackingBodyPart.position.z + 3.5f);
		
		TweenPosition.Begin (mainCamera, 2f, cameraInitialPosition);

	}

	GameObject selectAI_character(int selectedCharacter){

		//try and make sure AI and play do not select same character
		int aiCharacter = Random.Range(0, danceCharacters.Length);
		if (aiCharacter == selectedCharacter){
			aiCharacter = (selectedCharacter + 1) % (danceCharacters.Length-1);
		}
		
		return Instantiate(danceCharacters[aiCharacter], aiPos.transform.position, Quaternion.identity) as GameObject;

	}

	void Start () {


	}

	//game manager is going to take care of the camera movements and such
	// Update is called once per frame
	void Update () {

		if (activeDancePlayer != null)
			mainCamera.transform.LookAt (trackingBodyPart);

	}

	public Transform FindInAllChildren(Transform root, string name){

		foreach (Transform t in root.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == name)) {
			return t;		
		}
		//if you dont find the name
		return null;
	}
}
