using UnityEngine;
using System.Collections;

public class NextWindow : MonoBehaviour {

	public GameObject parentPanel;
	public GameObject[] gridies;
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int currentWindow = 0;
	void OnClick(){



		gridies [currentWindow].SetActive (false);
		gridies [++currentWindow].SetActive (true);

		if (currentWindow > 1) {

			this.gameObject.SetActive(false);		

		}
		Debug.Log ("go next "+currentWindow);
	}
}
