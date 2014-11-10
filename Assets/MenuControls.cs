using UnityEngine;
using System.Collections;

public class MenuControls : MonoBehaviour {

	public GameObject nextBtn, previousBtn, HomeBtn;
	public string whichWindow;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		if (whichWindow == "dancer") {

			nextBtn.SetActive (true);
			previousBtn.SetActive (false);
			HomeBtn.SetActive (true);
		
		}

		if (whichWindow == "dancetype") {
				
			nextBtn.SetActive (false);
			previousBtn.SetActive (true);
			HomeBtn.SetActive (true);

		}


	}

}
