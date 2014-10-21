﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SetupButton : MonoBehaviour {

	public Animator closePopUp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//bool finished = false;
	List<Audio_File> audio_files;
	void OnClick(){

		if (PlayerPrefs.GetInt ("has_audio_files", 0) == 0) {
			searchForMp3s ();
			//serialize audio_files
			serializeAudioFiles ();
			//remember to put in an IEnumerator to mimic loading ...
			//After you are done setting up, load the next scene
			Application.LoadLevel (1);
		} else {
			Application.LoadLevel(1);				
		}

	}

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

	void OnGUI(){

		/*if (finished) {

			foreach (Audio_File audiofile in audio_files) {

				GUILayout.Label (audiofile.getAudioFileName()+" ");
			}
		} else {
			//GUILayout.Label("Searching...");		
		}*/

	}

	void searchForMp3s(){
		SearchForMp3Files searchDir = new SearchForMp3Files ();
		closePopUp.SetBool ("close", false);
		string persDataPath = Application.persistentDataPath;
		string preferredDir = getParentLevel (4, persDataPath);
		
		searchDir.searchDirectory(preferredDir);
		
		audio_files = searchDir.getAudioFiles ();
		


	}

	string getParentLevel(int directoryLevel, string directoryPath){
		string tmpDirectoryPath = directoryPath;
		for (int i=0; i<directoryLevel; i++) {

			tmpDirectoryPath = Directory.GetParent(tmpDirectoryPath).FullName;

		}

		return tmpDirectoryPath;
	}
}
