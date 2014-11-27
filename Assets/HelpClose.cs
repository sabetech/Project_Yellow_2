using UnityEngine;
using System.Collections;

public class HelpClose : MonoBehaviour {

	public GameObject helpWindow;
	public GameObject mainMenu;
	public GameObject musicWin;

	public GameObject[] showThese;

	void OnClick(){

		helpWindow.SetActive (false);

		foreach (var showThis in showThese) {
		
			showThis.SetActive(true);
		
		}

		mainMenu.SetActive (true);
		musicWin.SetActive (true);
	}
}
