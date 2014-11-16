using UnityEngine;
using System.Collections;
using System;

public class DisplayTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		MP3_Player.getMp3Instance ().audioPlayed += audioPlayed;

	}

	void audioPlayed(object sender, EventArgs e){
		string theTitle = MP3_Player.getMp3Instance ().currentMp3Instance.getTitle ();
		if (theTitle.Length >= 30) {

			GetComponent<UILabel> ().text = theTitle.Substring (0, 30)+" ...";		
		
		} else {
		
			GetComponent<UILabel> ().text = theTitle;

		}

		if (theTitle.Length == 0) {

			GetComponent<UILabel> ().text = "Unknown Title";

		}

		
	}


}
