using UnityEngine;
using System.Collections;

public class CloseErrMsg : MonoBehaviour {

	public GameObject errWindow;
	public static CloseErrMsg errMsgInstance;

	void Awake(){

		errMsgInstance = this;

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showMessage(){

		TweenPosition.Begin (errWindow, 0.3f, new Vector3 (0f, -234.56f, 0f));

	}

	void OnClick(){

		//hide error msg
		TweenPosition.Begin(errWindow,0.3f, new Vector3(0f,203.89f,0f));

	}
}
