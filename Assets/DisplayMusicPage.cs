using UnityEngine;
using System.Collections;
using System;

public class DisplayMusicPage : MonoBehaviour {

	UILabel displayMusicPage;
	// Use this for initialization
	void Start () {

		PopulateMusicWindow.musicWindowInstance.pageChanged += pageChanged;

		displayMusicPage = GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void pageChanged(object sender, EventArgs e){

		displayMusicPage.text = PopulateMusicWindow.musicWindowInstance.currentStartNumber + " - " + PopulateMusicWindow.musicWindowInstance.currentFinishNumber;

	}
}
