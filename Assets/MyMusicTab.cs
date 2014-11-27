using UnityEngine;
using System.Collections;

public class MyMusicTab : MonoBehaviour {

	public GameObject myMusicTab;
	public GameObject onlineMusic;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		myMusicTab.SetActive (true);
		onlineMusic.SetActive (false);

	}
}
