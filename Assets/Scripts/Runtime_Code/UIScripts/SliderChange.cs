using UnityEngine;
using System.Collections;
using System;

public class SliderChange : MonoBehaviour {

	UILabel danceTime;

	// Update is called once per frame
	void Update () {
		
	}

	void OnSliderChange(float stepValue){
		danceTime = GetComponent<UILabel> ();

		string dispStepValue = "";
		int discreteStepVal = (int)Math.Floor (stepValue * 4f);

		switch (discreteStepVal) {
		case 0:
			dispStepValue = "2";
			break;

		case 1:
			dispStepValue = "4";
			break;

		case 2:
			dispStepValue = "6";
			break;

		case 4:
			dispStepValue = "8";
			break;
		}

		PlayerPrefs.SetString ("danceTime", dispStepValue);
		Debug.Log ("do you set on load?");
		danceTime.text = dispStepValue +" mins";

	}
}
