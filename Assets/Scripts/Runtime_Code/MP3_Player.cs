using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent (typeof (AudioSource))]
public class MP3_Player : MonoBehaviour {

	public static MP3_Player mp3Instance = null;
	public string currentMp3File = "";
	public MP3File currentMp3Instance = null;

	public static MP3_Player getMp3Instance(){

		return mp3Instance;

	}

	void Awake (){ //singleton things

		if ((mp3Instance != null) && (mp3Instance != this)) {
				
			return;
		
		} else {

			mp3Instance = this;
		
		}
		DontDestroyOnLoad (this.gameObject);
	}

	private List<Audio_File> audLib;
	void Start () {

		audLib = MusicCollection.GetMusicCollectionInstance().audioLibrary;

		play_audio_file (audLib[UnityEngine.Random.Range (0, audLib.Count - 1)].getAudioFileName());
	
	}


	// Update is called once per frame
	void Update () {	
		
	}

	void play_audio(MP3File mp3file){

	}

	/// <summary>
	/// 	Play the current mp3 song currently in the audio clip in the audio source
	/// </summary>

	void play_audio(){

		audio.Play ();

	}

	public void restart_Audio(){

		stop_audio ();
		play_audio ();
	
	}

	public void play_audio_file(string filename){

		StartCoroutine (play_audio_coroutine(filename));

	}

	IEnumerator play_audio_coroutine(string audioFilename){
		if (audio.isPlaying) {
			audio.Stop (); //if the audio is playing, stop it...
		}

		this.currentMp3File = audioFilename;
		this.currentMp3Instance = getID3TagInfo (audioFilename);

		string url = "file:///" + audioFilename;

		WWW wwwAudioClip = new WWW (url);

		yield return wwwAudioClip;

		audio.clip = wwwAudioClip.GetAudioClip (true,true); //do this for local songs

		audio.Play ();

		//raise audio is playing custom event here
		onAudioPlayed (EventArgs.Empty);

	}

	IEnumerator play_audio_online(string url, bool stream){

		yield return null;//wait for rasheeda :-)

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
			Debug.Log ("I am ID3v1");
			
		}else if (mp3ID3TagInfo.hasID3v2){
			Debug.Log ("I am ID3v2");
			mp3file = new MP3File(mp3ID3TagInfo);
			
		}

		return mp3file;
	}

	protected virtual void onAudioPlayed(EventArgs e){

		EventHandler handler = audioPlayed;
		if (handler != null) {
				
			handler(this, e);

		}

	}

	public event EventHandler audioPlayed;
}
