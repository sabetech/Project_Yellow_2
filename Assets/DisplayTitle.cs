using UnityEngine;
using System.Collections;
using System;

public class DisplayTitle : MonoBehaviour {

	UILabel songTitle;
	// Use this for initialization
	void Start () {
	
		//MP3_Player.getMp3Instance ().audioPlayed += audioPlayed;
		songTitle = GetComponent<UILabel> ();
	}


	void Update(){
		if (CurrentAudioPlaying.currentAudioPlaying != null)
			songTitle.text = CurrentAudioPlaying.currentAudioPlaying;
	
	}

	/*void audioPlayed(object sender, EventArgs e){
		string theTitle = MP3_Player.getMp3Instance ().currentMp3File;

		if (theTitle.Length >= 30) {

			GetComponent<UILabel> ().text = theTitle.Substring (0, 30)+" ...";		
		
		} else {
		
			GetComponent<UILabel> ().text = theTitle;

		}

		if (theTitle.Length == 0) {

			GetComponent<UILabel> ().text = "Unknown Title";

		}

		
	}*/


}
