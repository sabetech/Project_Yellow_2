using UnityEngine;
using System.Collections;

public class CycleColors : MonoBehaviour {


	float startBound =0f, endBound =1f;
	public float cycleSpeed = 3f;

	public byte maxColorLimit = 255;
	public byte maxAlpha = 255;

	Color32 myCurrentColor;
	Color32 color32_R,color32_B,color32_Rl, color32_G,color32_Bl, color32_Gl;
	float colorLerpTime = 0f;
	Camera myCameraBackground;
	Material floorMaterial;

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
		if (gameObject.tag == "MainCamera"){
		
			myCameraBackground = GetComponent<Camera> ();
			myCurrentColor = myCameraBackground.backgroundColor;
		}

		if (gameObject.tag == "dance_floor") {
			floorMaterial = GetComponent<MeshRenderer>().material;
			myCurrentColor = renderer.material.GetColor("_ReflectColor");

		}




							   // R  G  B  A
		color32_Gl= new Color32 (maxColorLimit, 0, 0, maxAlpha);//green gets to lower
		color32_B = new Color32 (maxColorLimit, 0, maxColorLimit, maxAlpha);//Blue gets high
		color32_Rl = new Color32 (0, 0, maxColorLimit, maxAlpha);// red get to lower
		color32_G = new Color32 (0, maxColorLimit, maxColorLimit, maxAlpha);// green gets to upper
		color32_Bl = new Color32 (0, maxColorLimit, 0, maxAlpha);// blue gets to lower
		color32_R = new Color32 (maxColorLimit, maxColorLimit, 0, maxAlpha); // Red get upper

		currentState = SwitchColorStates.moveBlueUp;

	}
	
	// Update is called once per frame
	void Update () {

		//move blue up
		if (currentState == SwitchColorStates.moveBlueUp) {

			colorLerpTime += Time.deltaTime / cycleSpeed;
			myCurrentColor = Color32.Lerp(color32_R, color32_B, colorLerpTime);

			if (colorLerpTime >= 1f){

				colorLerpTime = 0f;
				currentState = SwitchColorStates.moveRedDown;

			}
			//Debug.Log("blue val up"+myCurrentColor.b);

		}

		//move red down
		if (currentState == SwitchColorStates.moveRedDown) {

			colorLerpTime += Time.deltaTime / cycleSpeed;
			myCurrentColor = Color32.Lerp(color32_B, color32_Rl, colorLerpTime);

			if (colorLerpTime >= 1f){

				colorLerpTime = 0f;
				currentState = SwitchColorStates.moveGreenUp;

			}
			//Debug.Log ("red val down"+myCurrentColor.r);

		}

		//move green up
		if (currentState ==  SwitchColorStates.moveGreenUp){

			colorLerpTime += Time.deltaTime / cycleSpeed;
			myCurrentColor = Color32.Lerp(color32_Rl, color32_G, colorLerpTime);

			if (colorLerpTime >= 1f){
				colorLerpTime = 0f;
				currentState = SwitchColorStates.moveBlueDown;
			}

		}


		//move blue down 
		if (currentState == SwitchColorStates.moveBlueDown){

			colorLerpTime += Time.deltaTime / cycleSpeed;
			myCurrentColor = Color32.Lerp(color32_G, color32_Bl,colorLerpTime);

			if (colorLerpTime >= 1f){
				colorLerpTime = 0f;
				currentState = SwitchColorStates.moveRedUp;
			}
				

		}


		//move red up
		if (currentState == SwitchColorStates.moveRedUp){

			colorLerpTime += Time.deltaTime / cycleSpeed;
			myCurrentColor = Color32.Lerp(color32_Bl, color32_R, colorLerpTime);

			if (colorLerpTime >= 1f){
				colorLerpTime = 0f;
				currentState =SwitchColorStates.moveGreenDown;
			}
		}


		//move green down 
		if (currentState == SwitchColorStates.moveGreenDown){

			colorLerpTime += Time.deltaTime / cycleSpeed;
			myCurrentColor = Color32.Lerp(color32_R, color32_Gl,colorLerpTime);

			if (colorLerpTime >= 1f){
				colorLerpTime = 0f;
				currentState = SwitchColorStates.moveBlueUp;
			}
		}

		if (gameObject.tag == "MainCamera"){
			myCameraBackground.backgroundColor = (Color)myCurrentColor;
		}

		if (gameObject.tag == "dance_floor") {

			renderer.material.SetColor("_ReflectColor", myCurrentColor);
			renderer.material.color = (Color)myCurrentColor;

		}
	}
	
}
