using UnityEngine;
using System.Collections;

public class PreviousWindow : MonoBehaviour {

	public GameObject[] previousWindows;
	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void OnClick(){

		previousWindows [NextWindow.currentWindow].SetActive (false);
		previousWindows [--NextWindow.currentWindow].SetActive (true);

		if (NextWindow.currentWindow < 1) {
			
			this.gameObject.SetActive(false);
			
		}

	}
}
