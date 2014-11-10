using UnityEngine;
using System.Collections;

public class CloseMusicWindow : MonoBehaviour {

	private Animator musicChoiceWin;
	public GameObject playNowBtn, chooseMusicBtn;

	public GameObject mainMenuWin;
	// Use this for initialization
	void Start () {

		musicChoiceWin = GetComponentInParent<Animator> ();

	}


	void OnClick(){

		closeWindow ();
	}

	void closeWindow(){
		
		musicChoiceWin.SetBool ("MusicSlideInOut",false);
		playNowBtn.SetActive (true);
		chooseMusicBtn.SetActive (true);
		StartCoroutine (showCurrentMenuWin ());

	}

	IEnumerator showCurrentMenuWin(){

		yield return new WaitForSeconds(0.7f);
		mainMenuWin.SetActive (true);

	}
}
