using UnityEngine;
using System.Collections;

public class ResetSpeedSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isPressed){

		if (enabled) {
				
			if (!isPressed){

				Debug.Log ("r u released");

			}
		
		}

	}
}
