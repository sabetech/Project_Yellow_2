using UnityEngine;
using System.Collections;

public class CycleColors : MonoBehaviour {

	Color32[] myDiscoColors = {Color.red, Color.yellow, Color.green, Color.blue, Color.black,Color.cyan};


	Color32 color1, color2;
	int currentInt = 0;
	Camera myCameraBackground;
	// Use this for initialization
	void Start () {

		myCameraBackground = GetComponent<Camera> ();
		color1 = myDiscoColors [0];
		color2 = myDiscoColors [1];

		//InvokeRepeating ("changeColor", 1f, 1f);

	}
	
	// Update is called once per frame
	void Update () {
	
		//myCameraBackground.backgroundColor = Color32.Lerp (color1, color2, 1.5f);

	}

	void changeColor(){

		currentInt++;
		color1 = myDiscoColors [currentInt % 6];
		color2 = myDiscoColors [(currentInt + 1) % 6];

	}
}
