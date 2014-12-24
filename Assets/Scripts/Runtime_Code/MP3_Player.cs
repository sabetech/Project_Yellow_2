using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

[RequireComponent (typeof (AudioSource))]
public class MP3_Player : MonoBehaviour {

	public static MP3_Player mp3Instance = null;
	public string currentMp3File = "";
	public string currentMp3Instance = null;
	public Audio_File currentAudioFile = null;
	public AudioClip defaultMp3File;
	bool isDownloadingOnlineAudio = false;

	public static MP3_Player getMp3Instance(){

		return mp3Instance;

	}

	void Awake (){ //singleton things

		if ((mp3Instance != null) && (mp3Instance != this)) {
			Destroy (this.gameObject);
			return;
		
		} else {

			mp3Instance = this;
		
		}
		DontDestroyOnLoad (this.gameObject);

	

	}

	private List<Audio_File> audLib;
	void Start () {

		audLib = MusicCollection.GetMusicCollectionInstance().audioLibrary;
		play_random_next ();

	}


	// Update is called once per frame
	void Update () {	
		if (isDownloadingOnlineAudio) {
			if (wwwOnlineAudioClip != null)
				CurrentAudioPlaying.audioStatus = "Loading Online Music - Please Wait " + Math.Truncate((wwwOnlineAudioClip.progress / 1f) * 100f) + "%";
		}

	}


	public void play_random_next(){
		//StartCoroutine(play_audio_online("", true));

		if (audLib == null) 
			audLib = MusicCollection.GetMusicCollectionInstance ().audioLibrary;

		if (audLib.Count < 1)
			audLib = MusicCollection.GetMusicCollectionInstance ().audioLibrary;

		if (audLib.Count > 0)
			play_audio_file (audLib [UnityEngine.Random.Range (0, audLib.Count - 1)]);
		else
			play_audio (defaultMp3File);//play default track
	
	}

	void play_audio(AudioClip defAudio){

		audio.clip = defAudio;
		StartCoroutine (play_audio());
	
	}

	void monitorAudioProgress(){

		Invoke ("restart_Audio", audio.clip.length); //replay audio when it has ended

	}

	void play_audio(MP3File mp3file){

	}

	/// <summary>
	/// 	Play the current mp3 song currently in the audio clip in the audio source
	/// </summary>

	IEnumerator play_audio(){

		if (audio.isPlaying)
			audio.Stop ();

		yield return new WaitForEndOfFrame ();
		audio.Play ();

		monitorAudioProgress ();//monitor when audio has ended

	}

	public void restart_Audio(){

		CancelInvoke ();
		stop_audio ();
		StartCoroutine (play_audio ());
	
	}

	public void play_audio_file(Audio_File filename){

		StartCoroutine (play_audio_coroutine(filename));

	}

	IEnumerator play_audio_coroutine(Audio_File myAudFile){
		currentAudioFile = myAudFile;

		string audioFilename = myAudFile.getAudioFileName ();

		//check if file name exists before you continue koraa
		if (!File.Exists (audioFilename)) {
				
			//CloseErrMsg.errMsgInstance.showMessage();
			return false;
		}

		if (audio.isPlaying) {
			audio.Stop (); //if the audio is playing, stop it...
		}

		this.currentMp3File = audioFilename;

		this.currentMp3Instance = myAudFile.getShortAudioFilename ();

		string url = "file:///" + audioFilename;

		WWW wwwAudioClip = new WWW (url);

		yield return wwwAudioClip;

		audio.clip = wwwAudioClip.GetAudioClip (false, true); //do this for local songs

		audio.Play ();

		CancelInvoke ();
		monitorAudioProgress ();//monitor when audio has ended

		CurrentAudioPlaying.currentAudioPlaying = myAudFile.getShortAudioFilename ();;

		//raise audio is playing custom event here
		//onAudioPlayed (EventArgs.Empty);

	}

	public void playOnlineAudio(Audio_File link){

		//StartCoroutine(play_audio_online(link.getAudioFileName(), true));
		StartCoroutine ("play_audio_online", link.getAudioFileName());

		CurrentAudioPlaying.currentAudioPlaying = link.getWebTitle();

	}


	WWW wwwOnlineAudioClip;
	IEnumerator play_audio_online(string urlWeb){

		wwwOnlineAudioClip = new WWW(urlWeb);

		isDownloadingOnlineAudio = true;
		yield return wwwOnlineAudioClip;//thanks rasheeda :-)
		isDownloadingOnlineAudio = false;

		audio.clip = wwwOnlineAudioClip.GetAudioClip(false,true);
		if (audio.isPlaying)
			audio.Stop ();

		audio.Play();
		CurrentAudioPlaying.audioStatus = "Now Playing";

		CancelInvoke ();
		monitorAudioProgress ();//restart audio when its ends
	}



	public void stop_audio(){

		if (audio.isPlaying) {
			audio.Stop (); //if the audio is playing, stop it...
		}

	}

	public void stopFetchingIfGameStarted(){

		StopCoroutine ("play_audio_online");

	
	}

	public void pause_audio(){

		if (audio.isPlaying) {
			audio.Pause();		
		}
	
	}

	public void play_Paused_audio() {

		if (!audio.isPlaying)
			audio.Play ();
	
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

		if (!File.Exists (filename)) {
			//Debug.Log("Throw Similar error");
			return null;
		}

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
