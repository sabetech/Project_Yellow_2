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
		toggleWindow ();
	}

	void toggleWindow(){
		toggleDanceWin = !toggleDanceWin;

		if (toggleDanceWin) {
				
			showDanceOffWin();
			
		} else {
			
			StartCoroutine(closeDanceOffWin());
			
		}
	}

	void showDanceOffWin() {
		danceOffWin.SetActive (true);

		mainMenuAnimator.SetBool ("DanceOff", true);
		danceOffWinAnimator.SetBool ("MoveDanceOffUp", true);
	}

	IEnumerator closeDanceOffWin() {
		mainMenuAnimator.SetBool ("DanceOff", false);
		danceOffWinAnimator.SetBool ("MoveDanceOffUp", false);
		yield return new WaitForSeconds(1);
		danceOffWin.SetActive (false);
	}
}
