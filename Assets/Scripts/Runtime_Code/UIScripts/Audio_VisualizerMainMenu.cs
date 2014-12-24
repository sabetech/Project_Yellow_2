using UnityEngine;
using System.Collections;

public class Audio_VisualizerMainMenu : MonoBehaviour {

	private UISprite mainWindow;
	private float[] samples = new float[64];
	private int frequencyChannel = 0;
	public int signalAmplitude = 2;
	private float overAllSample;
	private UISprite audioLevel;

	// Use this for initialization
	void Start () {

		audioLevel = GetComponent<UISprite> ();

	}
	
	// Update is called once per frame
	void Update () {

		AudioListener.GetSpectrumData (samples,0, FFTWindow.Hamming); //find which of the FFT windowing algorithm is fastest
	
		audioLevel.fillAmount = samples [0] + samples [0] + samples [0] + samples [0] +samples [0] + samples [0] + samples [0];

	}


}
