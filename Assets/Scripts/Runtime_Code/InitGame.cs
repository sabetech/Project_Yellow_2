using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//check if user is new [if playerprefs new user is true/1 etc]
		//Or check if contents on the sdcard has changed
		//By checking hashing the content of the disk previously to see if any new content has been added
		//if so, pop up window asking the user to search device for music and build index for them

		//GameObject popUpWindow = Instantiate (pop_up_window, transform.position, Quaternion.identity) as GameObject;
		//pop_up_win_animator = popUpWindow.GetComponentInChildren<Animator> ();

		//StartCoroutine (myCoroutine());


	}

	IEnumerator myCoroutine(){
		yield return new WaitForSeconds (1);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
