using UnityEngine;
using System.Collections;

public class MusicClick : MonoBehaviour {


	public GameObject mp3PlayGameObject;
	private MP3_Player mp3PlayerScript;
	private string audioFileName;
	// Use this for initialization
	void Start () {

		//mp3Player = mp3PlayGameObject.GetComponent<AudioSource> ();
		mp3PlayerScript = mp3PlayGameObject.GetComponent<MP3_Player>();

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

		mp3PlayerScript.play_audio_file (audio_filename);

	}


}
