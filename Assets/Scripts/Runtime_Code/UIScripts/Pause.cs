using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public GameObject pauseWin;
	public static Pause pauseInstance;


	void Awake(){

		pauseInstance = this;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		pauseWin.SetActive (true);
		pauseGame ();

	}

	void OnApplicationPause(bool pauseStatus){
		if (pauseStatus) {
			pauseWin.SetActive (true);
			pauseGame();
		}
	
	}

	public void hidePauseButton(){

		this.gameObject.SetActive (false);
	
	}


	void pauseGame (){

		DanceGameManager.danceGameManager.pauseGame ();

	}
}
