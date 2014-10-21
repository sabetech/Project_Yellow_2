using UnityEngine;
using System.Collections;

public class BeatTimerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	string touching = "";
	string touchCounts = "";
	void Update () {

		if (Input.touchCount > 0) {
			//Debug.Log ("got u?");		
			touchCounts = "touchCount"+Input.touchCount;  
		}

		if (Input.GetMouseButton (0)) {
			touching = "using mouse as touch";		
		}

	
	}

	void OnGUI(){

		GUILayout.Label ("Touching ... :"+touching);
		GUILayout.Label ("Touching ... :"+touchCounts);

	}
}
