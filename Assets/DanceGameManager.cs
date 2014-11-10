using UnityEngine;
using System.Collections;

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

	public static int turn = 0;

	void Start () {
		//First First, check if the song is long enough to be danced to.
		//if the song is more than 1 min its ok

		//restart whatever song that is playing...
		myMp3Player = MP3_Player.getMp3Instance ();
		if (myMp3Player != null) {
			musicLength = myMp3Player.currentMp3Instance.getAudioLength ();

			Debug.Log ("The length is " + musicLength);
			Debug.Log ("The title name is " + myMp3Player.currentMp3Instance.getTitle ());
			Debug.Log ("The file name is " + myMp3Player.currentMp3Instance.getAudioFileName ());
			Debug.Log ("the bitrate is " + myMp3Player.currentMp3Instance.getBitrate ());
			Debug.Log ("What ID3Tag r u? ");
		}

		if (musicLength < 60L) {
			Debug.Log ("handle short songs or no length");
			//SO WE ARE NOT USING THE LENGTH OF THE MUSIC TO DETERMINE HOW LONG THE DANCE SHOULD TAKE!!
		}
		//get playerprefs infomation ie character, dance, AI or Not,

		Debug.Log ("Is verse AI? " + PlayerPrefs.GetInt ("isVerseAI", 0));
		if (PlayerPrefs.GetInt ("isVerseAI", 0) == 1) {
				
			Debug.Log ("We are playing we AI"); //so setup the dance floor to accomodate AI

		}

	}
	
	// Update is called once per frame
	void Update () {



	}
}
