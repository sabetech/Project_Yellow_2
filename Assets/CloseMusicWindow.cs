using UnityEngine;
using System.Collections;

public class CloseMusicWindow : MonoBehaviour {
	
	public GameObject playNowBtn, chooseMusicBtn, lblContextLabel;

	public GameObject mainMenuWin;
	public GameObject musicChoiceWin;
	// Use this for initialization
	void Start () {



	}


	void OnClick(){

		closeWindow ();
	}

	void closeWindow(){
		
		TweenPosition.Begin (musicChoiceWin, 0.5f, new Vector3 (351.5482f, 406.3738f, 0f));
	
		playNowBtn.SetActive (true);
		chooseMusicBtn.SetActive (true);
		lblContextLabel.SetActive (true);
		StartCoroutine (showCurrentMenuWin ());

	}

	IEnumerator showCurrentMenuWin(){

		yield return new WaitForSeconds(0.7f);
		mainMenuWin.SetActive (true);

	}
}
