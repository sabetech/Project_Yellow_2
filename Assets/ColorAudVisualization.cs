using UnityEngine;
using System.Collections;
using System;

//Audio visualization using the colors .... adapted from cycle colors	

public class ColorAudVisualization : MonoBehaviour {
	
	public byte maxColorLimit = 255;
	public byte maxAlpha = 255;
	
	Color32 myCurrentColor;
	Color32 color32_R,color32_B,color32_Rl, color32_G,color32_Bl, color32_Gl;
	//float colorLerpTime = 0f;
	Camera myCameraBackground;
	//Material floorMaterial;

	private float[] samples = new float[64];
	

	// Use this for initialization
	void Start () {

		TapForBPM.getGameTimerInstance ().gameFinished += onGameFinished;

		//get the necessary component that needs changing here
		if (gameObject.tag == "MainCamera"){
			
			myCameraBackground = GetComponent<Camera> ();
			myCurrentColor = myCameraBackground.backgroundColor;
		}
		
		if (gameObject.tag == "dance_floor") {
			//floorMaterial = GetComponent<MeshRenderer>().material;
			myCurrentColor = renderer.material.GetColor("_ReflectColor");
			
		}
		
		
		
		
		// R  G  B  A
		color32_Gl= new Color32 (maxColorLimit, 0, 0, maxAlpha);//green gets to lower
		color32_B = new Color32 (maxColorLimit, 0, maxColorLimit, maxAlpha);//Blue gets high
		color32_Rl = new Color32 (0, 0, maxColorLimit, maxAlpha);// red get to lower
		color32_G = new Color32 (0, maxColorLimit, maxColorLimit, maxAlpha);// green gets to upper
		color32_Bl = new Color32 (0, maxColorLimit, 0, maxAlpha);// blue gets to lower
		color32_R = new Color32 (maxColorLimit, maxColorLimit, 0, maxAlpha); // Red get upper

		
	}

	float audioLevel;
	// Update is called once per frame
	void Update () {

		//get audio frequencies here
		AudioListener.GetSpectrumData (samples,0, FFTWindow.Hamming); //find which of the FFT windowing algorithm is fastest

		audioLevel = samples [0] + samples [0] + samples [0] + samples [0] +samples [0] + samples [0] + samples [0];

		//Debug.Log (audioLevel);
		//move blue up
		if (audioLevel >= 0.01f) {

			myCurrentColor = color32_Rl;//very blue

		}

		if (audioLevel >= 0.1f) {
				
			myCurrentColor = color32_G;

		}


		//move red down
		if (audioLevel >= 0.3f) {

			myCurrentColor = color32_B;//less blue

		}
		
		//move green up
		if (audioLevel >= 0.5f){
			
			myCurrentColor = color32_Bl;//very green
			
		}
		
		
		//move blue down 
		if (audioLevel >= 1f){

			myCurrentColor = color32_R;//less green

		}
		
		
		//blue aud
		if (audioLevel >= 1.5f){

			myCurrentColor = color32_Bl;//less red

		}
		
		
		//Red aud visualization 
		if (audioLevel >= 2f){
		
			myCurrentColor = color32_Gl;//very red

		}
		
		if (gameObject.tag == "MainCamera"){
			myCameraBackground.backgroundColor = (Color)myCurrentColor;
		}
		
		if (gameObject.tag == "dance_floor") {
			
			renderer.material.SetColor("_ReflectColor", myCurrentColor);
			renderer.material.color = (Color)myCurrentColor;
			
		}
	}

	void onGameFinished(object sender, EventArgs e){

		this.enabled = false;
		GetComponent<CycleColors> ().enabled = true;

	}

}
		


