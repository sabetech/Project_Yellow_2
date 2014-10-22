using UnityEngine;
using System.Collections;

public class DanceOffClick : MonoBehaviour {
	public GameObject danceOffWin;
	private Animator mainMenuAnimator;
	private Animator danceOffWinAnimator;
	private bool toggleDanceWin = false;
	// Use this for initialization

	void Start () {

		mainMenuAnimator = GetComponentInParent<Animator> ();
		danceOffWinAnimator = danceOffWin.GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {



	}

	void OnClick(){

		danceOffWin.SetActive (true);
		mainMenuAnimator.SetBool ("DanceOff", true);
		danceOffWinAnimator.SetBool ("MoveDanceOffUp", true);

		StartCoroutine (disableThisWindow ());

	}

	IEnumerator disableThisWindow(){

		yield return new WaitForSeconds(0.8f);
		mainMenuAnimator.gameObject.SetActive (false);

	}


}
