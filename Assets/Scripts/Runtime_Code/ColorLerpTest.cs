using UnityEngine;
using System.Collections;

public class ColorLerpTest : MonoBehaviour {

	Color myRed;
	Color myGreen;

	float lerpValue= 0f;

	// Use this for initialization
	void Start () {

		//myRed = new Color32 (50,0,0,5);
		//myGreen = new Color32 (0,50,0,5);
		myRed = Color.red;
		myGreen = Color.green;
	}
	
	// Update is called once per frame
	void Update () {


		Color32 myNewColor = Color.Lerp (myRed,myGreen,lerpValue);

		if (lerpValue <= 0f) 

			lerpValue += Time.deltaTime * 0.3f;
				


		//Debug.Log ("Green "+myNewColor.g);
		//Debug.Log ("red "+myNewColor.r);

		GetComponent<Camera> ().backgroundColor = (Color)myNewColor;

		//Debug.Log (Mathf.Lerp (10f, 0f, 0.5f));

		//Debug.Log(Vector3.Lerp (new Vector3(10f,0f,0f), new Vector3(0f,0f,0f),Time.deltaTime).x);

	}
}
