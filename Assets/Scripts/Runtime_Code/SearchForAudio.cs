using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchForAudio : MonoBehaviour {

	public GameObject musicGrid;
	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnSubmit(string searchString){
		//on submit can be for online music .. check for that
		if (PopulateMusicWindow.isLocalWindow) {

			MusicCollection musCollection = MusicCollection.GetMusicCollectionInstance ();

			List<Audio_File> searchResult = musCollection.search (searchString);
			int limit = 30;

			if (searchResult.Count < limit) {

				limit = searchResult.Count;

			}

			PopulateMusicWindow.getMusicWindow ().populateMusicWindow (searchResult, 0, limit);
		}

		//else search using api and get result
	}
}
