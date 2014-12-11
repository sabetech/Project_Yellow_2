using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	public static GameSettings preferences;

	string danceTime;
	bool streamOnline;
	bool prefetchOnline;


	void Awake(){

			
		DontDestroyOnLoad (gameObject);

	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
