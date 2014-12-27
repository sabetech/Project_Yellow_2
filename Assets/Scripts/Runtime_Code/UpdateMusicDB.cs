using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UpdateMusicDB : MonoBehaviour {

	public static bool isSearching =false;

	public GameObject pleaseWait;
	void OnClick(){

		//GetComponentInChildren<UILabel>().text = "Please Wait...";
		pleaseWait.SetActive (true);

		asyncLookForMp3Files ();
	}

	void asyncLookForMp3Files(){

		StartCoroutine(lookformp3Files ());

	}

	IEnumerator lookformp3Files(){
		yield return new WaitForSeconds (1);
		yield return new WaitForEndOfFrame();
		SearchForMp3Files searchDir = new SearchForMp3Files ();
		
		string persDataPath = Application.persistentDataPath;
		string preferredDir = getParentLevel (4, persDataPath);
		
		searchDir.searchDirectory(preferredDir);

		audio_files = searchDir.getAudioFiles ();

		serializeAudioFiles ();

		MusicCollection.GetMusicCollectionInstance ().fetchMp3Files ();

		PopulateMusicWindow.musicWindowInstance.populateMusicWindow ();

		isSearching = false;
		GetComponentInChildren<UILabel>().text = "Tap To Update\nthe Music Database";
		pleaseWait.SetActive (false);
		yield return new WaitForEndOfFrame();


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

	string getParentLevel(int directoryLevel, string directoryPath){
		string tmpDirectoryPath = directoryPath;
		for (int i=0; i<directoryLevel; i++) {
			
			tmpDirectoryPath = Directory.GetParent(tmpDirectoryPath).FullName;
			
		}
		
		return tmpDirectoryPath;
	}
}
