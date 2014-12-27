using UnityEngine;
using System.Collections;

public class GoMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		DanceGameManager.danceGameManager.changeScene (1);
		DanceGameManager.danceGameManager.restart ();
	}
}
