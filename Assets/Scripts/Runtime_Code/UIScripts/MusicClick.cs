using UnityEngine;
using System.Collections;

public class MusicClick : MonoBehaviour {

	//private string audioFileName;
	private Audio_File myAudioFile;
	public bool isLocalAudio;
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		play (myAudioFile);
		
	}

	//public void setAudioFileName(string audFilename){

	//	audioFileName = audFilename;

	//}

	public void setAudioFile(Audio_File audFile){
	
		myAudioFile = audFile;
	
	}

	void play(Audio_File audio_file){

		if (this.isLocalAudio)
			MP3_Player.getMp3Instance ().play_audio_file (audio_file);

		if (!this.isLocalAudio)
			MP3_Player.getMp3Instance ().playOnlineAudio (myAudioFile);

	}




}
