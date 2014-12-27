using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class FetchMusicList : MonoBehaviour {

	public GameObject musicChoiceWindow;
	//private Transform musicChoiceWindowTransform;

	public GameObject mainMenuWin;


	// Use this for initialization
	void Start () {

		//musicChoiceWindowTransform = musicChoiceWindow.transform;

	}
	
	void OnClick(){

		showWindow ();

	}

	void showWindow(){

		TweenPosition.Begin(musicChoiceWindow,0.5f, new Vector3(-809.9696f, 406.3738f, 0f)).method = UITweener.Method.EaseInOut;//this is bad I know ... will be fixed later

		//also set the current main menu window to inactive
		mainMenuWin.SetActive (false);


	}

}
