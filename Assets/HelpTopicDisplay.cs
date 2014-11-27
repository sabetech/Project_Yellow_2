using UnityEngine;
using System.Collections;

public class HelpTopicDisplay : MonoBehaviour {

	public string whichTopic;
	public GameObject helpGrid;
	public GameObject overviewWin, howToWin, scoringWin, aboutWin;

	void OnClick(){

		if (whichTopic == "overview") {

			hideHelpGrid();
			overviewWin.SetActive(true);


		}
		if (whichTopic == "scoring") {

			hideHelpGrid();
			scoringWin.SetActive(true);

		}
		if (whichTopic == "howtoplay") {

			hideHelpGrid();
			howToWin.SetActive(true);

		}
		if (whichTopic == "about") {

			hideHelpGrid();
			aboutWin.SetActive(true);

		}

	}

	void hideHelpGrid(){

		helpGrid.SetActive (false);

	}
}
