using UnityEngine;
using System.Collections;

public class SetDanceTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		string previousDanceTime = PlayerPrefs.GetString ("danceTime", "2");

		int previousTime = int.Parse (previousDanceTime);
		float sliderVal = 0f;
		switch (previousTime) {
				
		case 2:
			sliderVal = 0f;
			break;

		case 4:
		sliderVal = 0.34f;
			break;
		
		case 6:
			sliderVal = 0.67f;
			break;

		case 8:
			sliderVal = 1f;
			break;
		
		}




		GetComponent<UISlider> ().sliderValue = sliderVal;

	
	}

}
