using UnityEngine;
using System.Collections;

public class BackWin : MonoBehaviour {

	private Animator thisWindow_Anim;
	public GameObject previousWin;
	private Animator previousWin_Anim;
	// Use this for initialization
	void Start () {
	
		thisWindow_Anim = GetComponentInParent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick () {

		showPreviousWindow ();

	}

	void showPreviousWindow(){


		previousWin = NGUITools.AddChild (GetComponentInParent<UIAnchor>().gameObject, previousWin);

		previousWin_Anim = previousWin.GetComponent<Animator> ();

		thisWindow_Anim.SetBool ("MoveDanceOffUp", false);
		previousWin_Anim.SetBool ("DanceWinSlide", true);

		StartCoroutine (disableThisWindow());

	}

	IEnumerator disableThisWindow(){

		yield return new WaitForSeconds (0.8f);
		Destroy (thisWindow_Anim.gameObject);

	}


}
