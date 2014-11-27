using UnityEngine;
using System.Collections;

public class CloseSettings : MonoBehaviour {

	public GameObject mainMenuWin;

	public GameObject settingsWindow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		if (UpdateMusicDB.isSearching)
			return;

		settingsWindow.SetActive (false);
		showAllWidgies ();

	}

	void showAllWidgies(){

		mainMenuWin.SetActive (true);

	}
}
