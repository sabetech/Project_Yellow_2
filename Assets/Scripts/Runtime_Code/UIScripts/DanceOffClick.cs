using UnityEngine;
using System.Collections;

public class DanceOffClick : MonoBehaviour {
	
	// Use this for initialization
	public GameObject danceGrid;
	public GameObject panelParent;

	void Start () {

	}

	// Update is called once per frame
	void Update () {



	}

	void OnClick(){

		NGUITools.AddChild(panelParent, danceGrid);

	}

	IEnumerator disableThisWindow(){

		yield return new WaitForSeconds(0.8f);

	}

}
