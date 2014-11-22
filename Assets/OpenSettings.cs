using UnityEngine;
using System.Collections;

public class OpenSettings : MonoBehaviour {

	public GameObject mainMenuWin;
	public GameObject settingsWindow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		//open settings
		settingsWindow.SetActive (true);
		hideWidgiesOnScreen ();

	}

	void hideWidgiesOnScreen(){

		mainMenuWin.SetActive (false);

	}
}
