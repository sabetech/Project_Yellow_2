using UnityEngine;
using System.Collections;

public class DanceWin_Next_btn : MonoBehaviour {

	private Animator danceOffWinAnimator;
	private Animator nextWinAnimator;
	public GameObject nextWindow;
	// Use this for initialization
	void Start () {
		//get animator from parent
		danceOffWinAnimator = GetComponentInParent<Animator> ();
		nextWinAnimator = nextWindow.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void OnClick () {

		danceOffWinAnimator.SetBool ("DanceWinSlide", true);
		nextWinAnimator.SetBool ("MoveDanceOffUp",true);

	}
}
