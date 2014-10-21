using UnityEngine;
using System.Collections;

public class Dance_GrdItem : MonoBehaviour {

	private UICenterOnChild uiGrid_controller;
	private string danceType;

	// Use this for initialization
	void Start () {
	
		uiGrid_controller = GetComponentInParent<UICenterOnChild> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (this.gameObject == uiGrid_controller.centeredObject){

			danceType = this.name;

		}

	}

	void saveGameChoiceData(){

		PlayerPrefs.SetString ("danceType", danceType);
		PlayerPrefs.Save ();

	}

}

