using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (AudioSource))]
public class MP3_Player : MonoBehaviour {

	public static MP3_Player mp3Instance = null;

	public static MP3_Player getMp3Instance(){

		return mp3Instance;

	}

	void Awake (){

		if ((mp3Instance != null) && (mp3Instance != this)) {
				
			return;
		
		} else {

			mp3Instance = this;
		
		}
		DontDestroyOnLoad (this.gameObject);
	}
	void Start () {


	}


	// Update is called once per frame
	void Update () {	
		
	}

	void getInstance(){



	}

	void play_audio(MP3File mp3file){

	}

	public void play_audio_file(string filename){

		StartCoroutine (play_audio_coroutine(filename));

	}

	IEnumerator play_audio_coroutine(string audioFilename){
		if (audio.isPlaying) {
			audio.Stop (); //if the audio is playing, stop it...
		}

		string url = "file:///" + audioFilename;
		WWW wwwAudioClip = new WWW (url);
		yield return wwwAudioClip;

		audio.clip = wwwAudioClip.GetAudioClip (true,true);

		audio.Play ();
	}

	public void stop_audio(){
		if (audio.isPlaying) {
			audio.Stop (); //if the audio is playing, stop it...
		}
	}

	public void volume_up(){
		if (audio.volume < 1) {
			audio.volume += 0.2f;
		}
	}

	void volume_down(){
		if (audio.volume > 0f) {
			audio.volume -= 0.2f;
		}
	}

	//Get information from Mp3 files
	//Supports Mp3 files only for now... 
	
	MP3File getID3TagInfo(string filename){
		//check for when any of the info has no value
		mp3info.mp3info mp3ID3TagInfo = new mp3info.mp3info (filename);
		
		mp3ID3TagInfo.ReadAll ();
		MP3File mp3file = null;
		
		if (mp3ID3TagInfo.hasID3v1) {
			
			mp3file = new MP3File(mp3ID3TagInfo);
			
		}else if (mp3ID3TagInfo.hasID3v2){
			
			mp3file = new MP3File(mp3ID3TagInfo);
			
		}
		
		return mp3file;
	}
}
