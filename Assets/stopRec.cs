using UnityEngine;
using System.Collections;

public class stopRec : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		Kamcord.StopRecording ();
		Kamcord.ShowView ();

	}
}
