using UnityEngine;
using System.Collections;

public class DanceAgain : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		//load to a loading level before you load to main menu to avoid hiccups

		DanceGameManager.danceGameManager.changeScene (3); //reload
		DanceGameManager.danceGameManager.restart ();
	}
}
