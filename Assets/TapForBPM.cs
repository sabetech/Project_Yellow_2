using UnityEngine;
using System.Collections;

public class TapForBPM : MonoBehaviour {

	private UILabel currentBpm;
	// Use this for initialization
	void Start () {

		currentBpm = GetComponentInChildren<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float prevTime = 0f;
	float currentTime;
	float timeDiff;

	//beat detection by tapping
	bool isFirstClick = true;
	void OnClick(){


		currentTime = Time.time;

		timeDiff = currentTime - prevTime;

		prevTime = currentTime;

		//I need at least two taps to calculate bpm
		if (isFirstClick) {
			isFirstClick = false;
			return;
		}

		currentBpm.text = (60f / timeDiff ) + " ";



	}
}
