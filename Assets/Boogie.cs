using UnityEngine;
using System.Collections;

public class Boogie : MonoBehaviour {

	public GameObject boogieWindow, nextBtn, previousBtn;
	public GameObject mainWindow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick (){

		Debug.Log ("You clicked on Boogie");
		boogieWindow.SetActive (true);
		mainWindow.SetActive (false);


	}

	void OnEnable(){

		nextBtn.SetActive (false);
		previousBtn.SetActive (false);

	}
}
