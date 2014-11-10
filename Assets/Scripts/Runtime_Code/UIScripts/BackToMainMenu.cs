using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour {

	public GameObject mainMenu_win;
	public GameObject[] currentWindows;

	// Use this for initialization
	void Start () {

	}


	//clicking back to main menu
	void OnClick(){

		mainMenu_win.SetActive (true);
		currentWindows [NextWindow.currentWindow].SetActive (false);
		NextWindow.currentWindow = -1;

	}

}