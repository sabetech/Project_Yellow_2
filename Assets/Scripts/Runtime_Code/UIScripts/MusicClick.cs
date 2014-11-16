using UnityEngine;
using System.Collections;

public class MusicClick : MonoBehaviour {

	private string audioFileName;
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		play (audioFileName);
		
	}

	public void setAudioFileName(string audFilename){

		audioFileName = audFilename;

	}

	void play(string audio_filename){

		MP3_Player.getMp3Instance ().play_audio_file (audio_filename);
		Debug.Log (audio_filename);

	}




}
