using UnityEngine;
using System.Collections;

public class Boogie : MonoBehaviour {

	public GameObject boogieWindow, nextBtn, previousBtn,homebtn, contextLabel, highScore;
	public GameObject mainWindow;
	public string whichButton;


	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("isVerseAI", 0);
		highScore.GetComponent<UILabel>().text = "HighScore: "+PlayerInfo.playerInfo.score +"";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick (){


		if (whichButton == "Boogie") {

			PlayerPrefs.SetInt ("isVerseAI", 0);
			contextLabel.GetComponent<UILabel>().text = "Boogie";
			highScore.GetComponent<UILabel>().text = "HighScore: "+PlayerInfo.playerInfo.score +"";


		
		} else {
				
			PlayerPrefs.SetInt ("isVerseAI", 1);
			contextLabel.GetComponent<UILabel>().text = "Boogie Battle";
			highScore.GetComponent<UILabel>().text = "Battle HighScore: "+PlayerInfo.playerInfo.vrsAIScore +"";
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
