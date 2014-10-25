using UnityEngine;
using System.Collections;

public class DanceOffClick : MonoBehaviour {
	public GameObject danceOffWin;
	private Animator mainMenuAnimator;
	private Animator danceOffWinAnimator;
	
	// Use this for initialization

	void Start () {

		mainMenuAnimator = GetComponentInParent<Animator> ();


	}

	// Update is called once per frame
	void Update () {



	}

	void OnClick(){


		//GameObject danceWin = Instantiate (danceOffWin) as GameObject;

		danceOffWin = NGUITools.AddChild (GetComponentInParent<UIAnchor> ().gameObject, danceOffWin);


		danceOffWinAnimator = danceOffWin.GetComponent<Animator> ();

		mainMenuAnimator.SetBool ("MoveMainMenu", false);
		danceOffWinAnimator.SetBool ("MoveDanceOffUp", true);


		StartCoroutine (disableThisWindow ());

	}

	IEnumerator disableThisWindow(){

		yield return new WaitForSeconds(0.8f);

		Destroy (mainMenuAnimator.gameObject);

	}


}
