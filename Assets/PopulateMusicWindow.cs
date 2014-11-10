using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class PopulateMusicWindow : MonoBehaviour {

	public GameObject audioLibrary;
	public Transform gridItemParent;
	public GameObject audioFileGridItem;

	// Use this for initialization
	void Start () {
	
		StartCoroutine(fetchMp3Files ());

	}

	IEnumerator fetchMp3Files(){
		
		//now populate audio window with audio files
		showMp3Files (audioLibrary.GetComponent<MusicCollection>().audioLibrary);
		yield return new WaitForEndOfFrame ();
		
	}

	//fix this function();
	void showMp3Files(List<Audio_File> audiofiles){
		
		foreach(Audio_File audFile in audiofiles){
			
			GameObject gridItem = NGUITools.AddChild(gridItemParent.gameObject, audioFileGridItem);
			
			mp3info.mp3info mp3information = new mp3info.mp3info(audFile.getAudioFileName());
			MusicClick mc = gridItem.GetComponent<MusicClick>();
			
			mc.setAudioFileName(audFile.getAudioFileName());
			
			try{
				mp3information.ReadAll();
				
				if (mp3information.hasID3v1){
					
					if (mp3information.id3v1.Title.Length >= 20){
						
						gridItem.GetComponentInChildren<UILabel>().text = mp3information.id3v1.Title.Substring(0,20);
						
					} else {
						if (mp3information.id3v1.Title.Trim().Length <= 1){
							if (audFile.getAudioFileName().Length <= 20)
								
								gridItem.GetComponentInChildren<UILabel>().text = audFile.getAudioFileName();
							
							else{
								string fakeTitle = audFile.getAudioFileName().Substring(0, 20);
								gridItem.GetComponentInChildren<UILabel>().text = fakeTitle;
							}
							
							
						}else{
							
							gridItem.GetComponentInChildren<UILabel>().text = mp3information.id3v1.Title;
							
						}
					}
					
					
				}else if(mp3information.hasID3v2){
					
					if (mp3information.id3v2.Title.Length >= 20)
						gridItem.GetComponentInChildren<UILabel>().text = mp3information.id3v2.Title.Substring(0,20);
					else
						gridItem.GetComponentInChildren<UILabel>().text = mp3information.id3v2.Title;
					
				}
			}catch(Exception){
				string filename = audFile.getAudioFileName();
				
				filename = filename.Substring(filename.LastIndexOf("\\")+1);
				if (filename.Length >= 20)
					filename = filename.Substring(filename.LastIndexOf("\\")+1, 20);
				
				//gridItem.GetComponentInChildren<UILabel>().text = filename;
				
			}
		}
		
		gridItemParent.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		gridItemParent.GetComponent<UIGrid> ().Reposition ();
	}
	
}
