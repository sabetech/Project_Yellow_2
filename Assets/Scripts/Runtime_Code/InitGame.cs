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

		Debug.Log ("PlaerPref "+PlayerPrefs.GetInt ("has_audio_files", 0));
		if (PlayerPrefs.GetInt ("has_audio_files", 0) == 0) {
		
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
			
			PlayerPrefs.SetInt ("has_audio_files",1);
			
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

		Application.LoadLevel (1);

	}
	
	string getParentLevel(int directoryLevel, string directoryPath){
		string tmpDirectoryPath = directoryPath;
		for (int i=0; i<directoryLevel; i++) {
			
			tmpDirectoryPath = Directory.GetParent(tmpDirectoryPath).FullName;
			
		}
		
		return tmpDirectoryPath;
	}

	IEnumerator goToMainMenu(){
		//splash screen ... :D
		yield return new WaitForSeconds (1);

		Application.LoadLevel (1);

	}
}
