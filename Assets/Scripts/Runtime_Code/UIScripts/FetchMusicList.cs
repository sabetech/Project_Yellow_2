using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class FetchMusicList : MonoBehaviour {

	public GameObject musicChoiceWindow;
	private Animator musicChoiceWin;
	public GameObject playNowButton;
	public GameObject contextLabel;

	public GameObject mainMenuWin;


	// Use this for initialization
	void Start () {

		musicChoiceWin = musicChoiceWindow.GetComponent<Animator> ();


	}
	
	void OnClick(){

		showWindow ();

	}

	void showWindow(){

		musicChoiceWin.SetBool ("MusicSlideInOut",true);
		this.gameObject.SetActive (false);
		playNowButton.SetActive (false);


		//also set the current main menu window to inactive
		mainMenuWin.SetActive (false);
		contextLabel.SetActive (false);

	}

}
