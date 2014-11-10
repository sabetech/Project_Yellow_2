using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


public class MusicCollection : MonoBehaviour{

	public List<Audio_File> audioLibrary;
	public GameObject mp3Player;

	void Start (){
		DontDestroyOnLoad (this.gameObject);

		try{
			//this part has to finish loading before any music related stuff happens
			fetchMp3Files ();
			int audioIndex = UnityEngine.Random.Range (0, audioLibrary.Count - 1);
			mp3Player.GetComponent<MP3_Player> ().play_audio_file(audioLibrary[audioIndex].getAudioFileName());

		}catch(Exception){

		}

	}

	public void fetchMp3Files(){
		//unserialize and populate the audioLibrary;
		try{
			using(Stream aud_stream = File.Open (Application.persistentDataPath+"/audio_files_ref.dat", FileMode.Open))
			{
				BinaryFormatter binFormat = new BinaryFormatter();
				audioLibrary = (List<Audio_File>)binFormat.Deserialize(aud_stream);

			}
		}catch(IOException ioex){
			Debug.Log (ioex);
			//remember if the user doesn't have a song, use a default song
		}
	}
}
