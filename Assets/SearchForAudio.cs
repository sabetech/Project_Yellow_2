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

		MusicCollection musCollection = MusicCollection.GetMusicCollectionInstance();

		List<Audio_File> searchResult = musCollection.search (searchString);
		int limit = 20;

		if (searchResult.Count < 20) {
		
			limit = searchResult.Count;
		
		}

		Debug.Log (searchResult.Count);

		PopulateMusicWindow.getMusicWindow ().populateMusicWindow (searchResult, 0, limit);

	}
}
