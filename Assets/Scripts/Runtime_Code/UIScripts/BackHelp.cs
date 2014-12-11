using UnityEngine;
using System.Collections;

public class BackHelp : MonoBehaviour {

	public GameObject helpGrid;

	public GameObject[] hideOpenTopics;

	void OnClick(){

		foreach (var win in hideOpenTopics) {

			win.SetActive(false);
		
		}

		helpGrid.SetActive (true);

	}
}
