using UnityEngine;
using System.Collections;

public class DanceWin_Next_btn : MonoBehaviour {

	private Animator thisWinAnimator;
	private Animator nextWinAnimator;
	public GameObject nextWindow;
	// Use this for initialization
	void Start () {

		//get animator from parent
		thisWinAnimator = GetComponentInParent<Animator> ();


	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void OnClick () {

		showNextWindow ();

	}

	void showNextWindow(){


		nextWindow = NGUITools.AddChild (GetComponentInParent<UIAnchor>().gameObject, nextWindow);

		nextWinAnimator = nextWindow.GetComponent<Animator> ();

		thisWinAnimator.SetBool ("DanceWinSlide", true);
		nextWinAnimator.SetBool ("MoveDanceOffUp",true);


		StartCoroutine (disableThisWindow ());

	}

	IEnumerator disableThisWindow(){

		yield return new WaitForSeconds (0.8f);
		Destroy (thisWinAnimator.gameObject);

	}
}

