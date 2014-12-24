using UnityEngine;
using System;
using System.Collections;

public class MusicClick : MonoBehaviour {

	//private string audioFileName;
	private Audio_File myAudioFile;
	public bool isLocalAudio;
	UISprite musicIcon;
	// Use this for initialization
	void Start () {

		musicClickedEvent += musicClicked;

		musicIcon = GetComponentInChildren<UISprite> ();

		//check what audio file is playing and highlight it
		if (this.myAudioFile == MP3_Player.mp3Instance.currentAudioFile) {

			musicIcon.color = new Color32 (118,255,238,255);	

		}

	}

	void OnClick(){

		play (myAudioFile);
		musicIcon.color = new Color32 (118,255,238,255); //color green
		onMusicClicked (EventArgs.Empty);

	}

	//check what audio file is play in highlight it
	void OnEnable(){

		if (this.myAudioFile == MP3_Player.mp3Instance.currentAudioFile) {

			musicIcon.color = new Color32 (118,255,238,255);	

		}
			
	}

	void musicClicked(object sender, EventArgs e){

		resetColor ();
	
	}

	void resetColor(){

		if (this.myAudioFile != MP3_Player.mp3Instance.currentAudioFile) {
				
			musicIcon.color = new Color32(255,255,255,255);
		
		}

	}

	void OnDestroy() {

		musicClickedEvent -= musicClicked;
	
	}

	public void setAudioFile(Audio_File audFile){
	
		myAudioFile = audFile;
	
	}

	void play(Audio_File audio_file){

		if (this.isLocalAudio)
			MP3_Player.getMp3Instance ().play_audio_file (audio_file);

		if (!this.isLocalAudio)
			MP3_Player.getMp3Instance ().playOnlineAudio (myAudioFile);

	}

	public static event EventHandler musicClickedEvent;
	protected virtual void onMusicClicked(EventArgs e){
		
		EventHandler handler = musicClickedEvent;
		if (handler != null) {
			
			handler(this, e);
			
		}
		
	}

}
