using UnityEngine;
using System.Collections;

public class Boogie : MonoBehaviour {

	public GameObject boogieWindow, nextBtn, previousBtn,homebtn, contextLabel;
	public GameObject mainWindow;
	public string whichButton;

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("isVerseAI", 0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick (){


		if (whichButton == "Boogie") {

			PlayerPrefs.SetInt ("isVerseAI", 0);
			contextLabel.GetComponent<UILabel>().text = "Boogie";


		
		} else {
				
			PlayerPrefs.SetInt ("isVerseAI", 1);
			contextLabel.GetComponent<UILabel>().text = "Boogie Battle";

		}

		NextWindow.currentWindow = 0;

		boogieWindow.SetActive (true);
		mainWindow.SetActive (false);
		nextBtn.SetActive(true);
		previousBtn.SetActive (false);


	}

	void OnEnable(){

		nextBtn.SetActive (false);
		previousBtn.SetActive (false);
		homebtn.SetActive (false);

	}
}
