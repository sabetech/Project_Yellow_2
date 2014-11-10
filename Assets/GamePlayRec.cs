using UnityEngine;
using System.Collections;

public class GamePlayRec : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Kamcord.StartRecording ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool recording = false;
	void OnClick(){

		if (!recording)
			Kamcord.StartRecording ();
		else {
			Kamcord.StopRecording();
			Kamcord.ShowView();
		}

	}
}
