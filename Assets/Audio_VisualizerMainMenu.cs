using UnityEngine;
using System.Collections;

public class Audio_VisualizerMainMenu : MonoBehaviour {

	private UISprite mainWindow;
	private float[] samples = new float[64];
	private int frequencyChannel = 0;
	public int signalAmplitude = 2;
	private float overAllSample;

	// Use this for initialization
	void Start () {

		mainWindow = GetComponentInChildren<UISprite> ();

	}
	
	// Update is called once per frame
	void Update () {

		//AudioListener.GetSpectrumData (samples,0, FFTWindow.Hamming);

		//overAllSample = samples [32] * 1000;

		//mainWindow.color = new Color(overAllSample, samples[13] * 1000, samples[54] * 1000, 1);

	
	}


}
