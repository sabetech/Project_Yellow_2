﻿using UnityEngine;
using System.Collections;

public class DancerWindow : MonoBehaviour {


	public GameObject nextBtn;
	public GameObject previousBtn;
	public GameObject backTomainMenu;
	public GameObject myHeading;
	public string theWindowHeading;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){

		nextBtn.SetActive (true);
		backTomainMenu.SetActive (true);

		if (NextWindow.currentWindow > 0) {
				
			previousBtn.SetActive (true);

		} else {
		
			if (NextWindow.currentWindow < 0){

				NextWindow.currentWindow = 0;

			}

		}


		myHeading.GetComponent<UILabel>().text = theWindowHeading;
		GetComponentInParent<UIDraggablePanel> ().scale.x = 1f;
		GetComponentInParent<UIDraggablePanel> ().scale.y = 0f;

	}
	
}
