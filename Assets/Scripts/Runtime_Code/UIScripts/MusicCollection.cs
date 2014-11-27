using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;


public class MusicCollection : MonoBehaviour{

	public List<Audio_File> audioLibrary;
	public static MusicCollection musicCollectionInstance;
	//public GameObject mp3Player;
	//public bool isDoneLoadingMusic = false;

	void Awake(){

		DontDestroyOnLoad (this.gameObject);
		musicCollectionInstance = this;
	
	}


	public static MusicCollection GetMusicCollectionInstance(){

		return musicCollectionInstance;
	
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

	public List<Audio_File> search(string searchString){

		var searchResultList = (List<Audio_File>)audioLibrary.Where (aud =>
		                   			 aud.getAudioFileName().ToLower().Contains(
					searchString.ToLower())).ToList(); //

		return searchResultList;
	}
}
