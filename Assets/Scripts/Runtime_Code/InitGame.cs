using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InitGame : MonoBehaviour {

	public GameObject loading;
	// Use this for initialization
	void Start () {

		//this side has to change to... try to find the file that has my audio file.. if u fail ... get a new one

		//if (PlayerPrefs.GetInt ("has_audio_files", 0) == 0) {
		if (!File.Exists(Application.persistentDataPath + "/audio_files_ref.dat")){
			Instantiate (loading);
			StartCoroutine (searchForMp3s ());

		} else {
				
			StartCoroutine(goToMainMenu());

		}



	}
	
	List<Audio_File> audio_files;

	void serializeAudioFiles(){
		
		try{
			using(Stream audioFilesStream = File.Open (Application.persistentDataPath+"/audio_files_ref.dat", FileMode.Create))
			{
				BinaryFormatter binformat = new BinaryFormatter();
				binformat.Serialize(audioFilesStream, audio_files);
			}
			
		}catch(IOException){
			
		}catch(Exception){
			
		}
		
	}
	
	
	IEnumerator searchForMp3s() {
		
		SearchForMp3Files searchDir = new SearchForMp3Files ();
		string persDataPath = Application.persistentDataPath;
		string preferredDir = getParentLevel (4, persDataPath);
		
		searchDir.searchDirectory(preferredDir);
		
		audio_files = searchDir.getAudioFiles ();
		
		serializeAudioFiles ();
		
		yield return new WaitForEndOfFrame ();

		StartCoroutine(goToMainMenu());

	}
	
	string getParentLevel(int directoryLevel, string directoryPath){
		string tmpDirectoryPath = directoryPath;
		for (int i=0; i<directoryLevel; i++) {
			
			tmpDirectoryPath = Directory.GetParent(tmpDirectoryPath).FullName;
			
		}
		
		return tmpDirectoryPath;
	}

	IEnumerator goToMainMenu() {
		//then deserilize and go to main menu

		MusicCollection.GetMusicCollectionInstance ().fetchMp3Files ();

		yield return new WaitForSeconds (0.5f);

		Application.LoadLevel (1);

	}
}
