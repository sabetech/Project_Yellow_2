using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour {
	private Animator danceType_Anim;
	private Animator mainMenu_Anim;
	public GameObject mainMenu_win;

	// Use this for initialization
	void Start () {
		danceType_Anim = GetComponentInParent<Animator> ();
		mainMenu_Anim = mainMenu_win.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	//clicking back to main menu
	void OnClick(){

		mainMenu_Anim.SetBool ("DanceOff", false);
		danceType_Anim.SetBool ("MoveDanceOffUp", false);

	}
}
