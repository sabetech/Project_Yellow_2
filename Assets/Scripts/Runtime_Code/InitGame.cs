using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InitGame : MonoBehaviour {
	//This initialization for the game ...


	public GameObject loading;
	public GameObject warningMsg;
	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

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

	bool ready = false;
	IEnumerator goToMainMenu() {
		//then deserilize and go to main menu

		MusicCollection.GetMusicCollectionInstance ().fetchMp3Files ();
		PlayerStats myPlayerStats = warningMsg.GetComponent<PlayerStats> ();

		myPlayerStats.info = "Warning\nThis Game Contains flashing " +
			"lights which may be a health harzard to\nPhotosensitive epileptic patients. " +
				"Please be sure you do not have ANY health issues with\nflashing lights before continuing\n\n\n" +
				"Tap To Coninue"; //warning info goes here

		Instantiate (warningMsg, transform.position, Quaternion.identity);

		ready = true;


		yield return null;

		//Application.LoadLevel (1);

	}

	void OnGUI(){

		if (!ready)
			return;

		if (Input.GetMouseButton (0)) {
				
			Application.LoadLevel(1);
		
		}

	}
}
