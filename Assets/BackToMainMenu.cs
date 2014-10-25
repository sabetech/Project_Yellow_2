using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour {
	private Animator danceType_Anim;
	private Animator mainMenu_Anim;
	public GameObject mainMenu_win;

	// Use this for initialization
	void Start () {
		danceType_Anim = GetComponentInParent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	//clicking back to main menu
	void OnClick(){

		showPreviousWindow ();

	}

	void showPreviousWindow(){


		mainMenu_win = NGUITools.AddChild (GetComponentInParent<UIAnchor>().gameObject, mainMenu_win);

		mainMenu_Anim = mainMenu_win.GetComponent<Animator> ();

		mainMenu_Anim.SetBool ("MoveMainMenu", true);

		danceType_Anim.SetBool ("MoveDanceOffUp", false);

		StartCoroutine (disableThisWindow ());

	}

	IEnumerator disableThisWindow(){

		yield return new WaitForSeconds (0.8f);

		Destroy (danceType_Anim.gameObject);


	}
}
