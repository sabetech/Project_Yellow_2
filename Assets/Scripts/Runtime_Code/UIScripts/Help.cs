using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour {

	public GameObject helpWindow;
	public GameObject[] hideAllThese;

	void OnClick(){

		foreach(var these in hideAllThese){

			these.SetActive(false);

		}

		helpWindow.SetActive (true);

	}
}
