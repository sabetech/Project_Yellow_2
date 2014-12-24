using UnityEngine;
using System.Collections;

public class SlowMo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Whats up");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress(bool released){

		Debug.Log ("ARE U pressed" +released);

	}

	void OnSelect (bool selected){

		Debug.Log ("Are u released" + selected);

	}

	void OnClick(){

		Debug.Log ("As for click dier");

	}

}
