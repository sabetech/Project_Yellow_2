using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Linq;

public class PopulateMusicWindow : MonoBehaviour {

	public static PopulateMusicWindow musicWindowInstance;

	public GameObject gridItemParent;
	public GameObject audioFileGridItem;

	private MusicCollection musicCollection;
	public int currentStartNumber =0;
	public int currentFinishNumber = 20;

	void Awake(){

		musicWindowInstance = this;
	
	}

	public static PopulateMusicWindow getMusicWindow(){
	
		return musicWindowInstance;

	}

	// Use this for initialization
	void Start () {

		musicCollection = MusicCollection.GetMusicCollectionInstance ();
		populateMusicWindow ();

	}

	public void populateMusicWindow(){//nad advisable

		StartCoroutine(showMp3Files (musicCollection.audioLibrary));

	}

	//over loaded :D
	public void populateMusicWindow (List<Audio_File> audFiles, int start, int limit){

		StartCoroutine (showMp3Files (audFiles, start, limit));

	}

	public void populateMusicWindow(int start, int limit) {

		StartCoroutine(showMp3Files (musicCollection.audioLibrary, start, limit));

	}

	//fix this function(); i am about to do so now :D
	IEnumerator showMp3Files(List<Audio_File> audiofiles, int start=0, int finish=10){

		GameObject[] music_items = GameObject.FindGameObjectsWithTag ("music_item");

		List<GameObject> musicitems = music_items.ToList ();
		if (musicitems.Count > 0) {

			musicitems.ForEach (music => Destroy (music));		
		
		}

		yield return new WaitForEndOfFrame ();

		//some safety check
		if ((start + finish) > audiofiles.Count) {
				
			finish = audiofiles.Count - start;
		
		}

		Debug.Log ("Start "+start);
		Debug.Log ("finsih" + finish);

		for (int i=start; i < finish; i++) {

			GameObject gridItem = NGUITools.AddChild (gridItemParent, audioFileGridItem);
			mp3info.mp3info mp3information = new mp3info.mp3info (audiofiles [i].getAudioFileName ());

			MusicClick mc = gridItem.GetComponent<MusicClick> ();
			mc.setAudioFileName (audiofiles [i].getAudioFileName ());

			try {
				mp3information.ReadAll ();

				if (mp3information.hasID3v1) {

					//if the length of the title is more than 20 cut it short
					if (mp3information.id3v1.Title.Trim().Length >= 20) {
	
						gridItem.GetComponentInChildren<UILabel> ().text = mp3information.id3v1.Title.Substring (0, 20)+"...";
	
					} else {
						if (mp3information.id3v1.Title.Trim ().Length <= 1) {
							if (audiofiles[i].getShortAudioFilename ().Length <= 20)
								//check this part
								gridItem.GetComponentInChildren<UILabel> ().text = audiofiles[i].getShortAudioFilename();
							else {
								string fakeTitle = audiofiles[i].getShortAudioFilename ().Substring (0, 20)+"...";
								gridItem.GetComponentInChildren<UILabel> ().text = fakeTitle;
							}
		
						} else {
	
							gridItem.GetComponentInChildren<UILabel> ().text = mp3information.id3v1.Title;
	
						}
					}
		
				} else if (mp3information.hasID3v2) {
	
					if (mp3information.id3v2.Title.Length >= 20){

						gridItem.GetComponentInChildren<UILabel> ().text = mp3information.id3v2.Title.Substring (0, 20)+"...";
					
					}else{
						if (mp3information.id3v2.Title.Length < 1){

							gridItem.GetComponentInChildren<UILabel> ().text = (audiofiles[i].getShortAudioFilename().Length > 20) ? audiofiles[i].getShortAudioFilename()+"...":audiofiles[i].getShortAudioFilename();

						}
						gridItem.GetComponentInChildren<UILabel> ().text = mp3information.id3v2.Title;

					}
						
	
				}
			} catch (Exception e) {
				Debug.Log (e);//EI I can even get a system out of memory exception ... hmmm what can i do
				string filename = audiofiles [i].getShortAudioFilename();

				if (filename.Length == 0){
					gridItem.GetComponentInChildren<UILabel>().text = "Unknown Title";
				}else{
					gridItem.GetComponentInChildren<UILabel>().text = filename;
				}


			}
			yield return null;

			if (gridItem.GetComponentInChildren<UILabel>().text == ""){

				gridItem.GetComponentInChildren<UILabel>().text = "Unknown Title";

			}

			gridItemParent.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
			gridItemParent.GetComponent<UIGrid> ().Reposition ();

		}

		yield return new WaitForEndOfFrame ();
		gridItemParent.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		gridItemParent.GetComponent<UIGrid> ().Reposition ();

	}
	
}
