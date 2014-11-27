using UnityEngine;
using System.Collections;

public class OnlineMusic : MonoBehaviour {


	public GameObject onlineMusic;
	public GameObject myMusic;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		onlineMusic.SetActive (true);
		myMusic.SetActive (false);

		//fetch from rasheeda's api the audios

	}
}
