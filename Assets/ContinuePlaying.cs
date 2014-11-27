using UnityEngine;
using System.Collections;

public class ContinuePlaying : MonoBehaviour {

	public GameObject thisPauseWindow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		ResumeFromPause ();
		thisPauseWindow.SetActive (false);
	}

	void ResumeFromPause(){

		DanceGameManager.danceGameManager.resumeGame ();

	}
}
