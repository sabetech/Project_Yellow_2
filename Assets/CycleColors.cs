using UnityEngine;
using System.Collections;

public class CycleColors : MonoBehaviour {


	float startBound =0f, endBound =1f;

	Color myCurrentColor;
	public float currentBlueColorTest;
	Color32 color32_1,color32_2;

	Camera myCameraBackground;

	enum SwitchColorStates{

		moveBlueUp,
		moveRedDown,
		moveGreenUp,
		moveBlueDown,
		moveRedUp,
		moveGreenDown

	}

	SwitchColorStates currentState;
	
	// Use this for initialization
	void Start () {

		//get the necessary component that needs changing here
		myCameraBackground = GetComponent<Camera> ();
		myCurrentColor = myCameraBackground.backgroundColor;

		color32_1 = new Color32 (38, 0, 0, 5);
		color32_2 = new Color32 (38, 0, 38, 5);

		currentState = SwitchColorStates.moveBlueUp;

	}
	
	// Update is called once per frame
	void Update () {

		//move blue up
		if (currentState == SwitchColorStates.moveBlueUp) {

			//myCurrentColor.b = Mathf.Lerp (startBound, endBound, 0.2f);
			//Color32.
			//Debug.Log (myCurrentColor.b);
			//myCurrentColor.b = currentBlueColorTest;

			myCurrentColor = Color32.Lerp(color32_1, color32_2, 0.5f);//this works

			//if (myCurrentColor.b == 1){
			//	currentState = SwitchColorStates.moveRedDown;
			//	Debug.Log("Do you get here blue up!");
			//}
		}


		//move red down
		if (currentState == SwitchColorStates.moveRedDown) {

			myCurrentColor.r = (byte)Mathf.Lerp (endBound, startBound, Time.time);
			if (myCurrentColor.r == 0){
				currentState = SwitchColorStates.moveGreenUp;
				Debug.Log("Do you get here red down!");
			}
		}


		//move green up
		if (currentState ==  SwitchColorStates.moveGreenUp){

			myCurrentColor.g = (byte)Mathf.Lerp (startBound, endBound, Time.deltaTime * 5f);
			if (myCurrentColor.g == 1){
				currentState = SwitchColorStates.moveBlueDown;
				Debug.Log("Do you get here green up!");
			}
		}


		//move blue down 
		if (currentState == SwitchColorStates.moveBlueDown){

			myCurrentColor.b = (byte)Mathf.Lerp (endBound, startBound, Time.deltaTime * 5f);
			if (myCurrentColor.b == 0){
				currentState = SwitchColorStates.moveRedUp;
				Debug.Log("Do you get here Blue down!");
			}

		}


		//move red up
		if (currentState == SwitchColorStates.moveRedUp){

			myCurrentColor.r = (byte)Mathf.Lerp (startBound, endBound, Time.deltaTime * 5f);
			if(myCurrentColor.r == 1){
				currentState = SwitchColorStates.moveGreenDown;
				Debug.Log("Do you get here Red up!");
			}
		}


		//move green down 
		if (currentState == SwitchColorStates.moveGreenDown){

			myCurrentColor.g = (byte)Mathf.Lerp (endBound, startBound, Time.deltaTime * 5f);
			if (myCurrentColor.g == 0){
				currentState = SwitchColorStates.moveBlueUp;
				Debug.Log("Do you get here green down!");
			}
		}

		myCameraBackground.backgroundColor = myCurrentColor;
	
	}
	
}
