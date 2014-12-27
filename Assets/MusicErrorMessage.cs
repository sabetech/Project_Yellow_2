using UnityEngine;
using System.Collections;

public class MusicErrorMessage : MonoBehaviour {

	public static MusicErrorMessage musicErrMsg;
	public GameObject errorMsgWindow;

	void Awake(){

		musicErrMsg = this;
	
	}

	void Start(){

	}

	public void showMessage(){

		errorMsgWindow.SetActive (true);

	}

	public void hideMessage(){

		errorMsgWindow.SetActive (false);

	}
}
