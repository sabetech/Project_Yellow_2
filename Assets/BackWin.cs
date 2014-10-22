using UnityEngine;
using System.Collections;

public class BackWin : MonoBehaviour {

	private Animator thisWindow_Anim;
	public GameObject previousWin;
	private Animator previousWin_Anim;
	// Use this for initialization
	void Start () {
	
		thisWindow_Anim = GetComponentInParent<Animator> ();
		previousWin_Anim = previousWin.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick () {



	}

	void showPreviousWindow(){

		previousWin.SetActive (true);
		thisWindow_Anim.SetBool ("MoveDanceOffUp",false);
		previousWin_Anim.SetBool ("DanceWinSlide", false);

		StartCoroutine (disableThisWindow());

	}

	IEnumerator disableThisWindow(){

		yield return new WaitForSeconds (0.8f);
		thisWindow_Anim.gameObject.SetActive (false);

	}


}
