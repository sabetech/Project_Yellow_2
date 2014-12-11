using UnityEngine;
using System.Collections;
using System;

public class BoogieWebApi : MonoBehaviour {


	private static string url = "http://boogieloops.com/api/audios";
	public static BoogieWebApi boogieWebApi;

	WWW myBoogieWWW;

	void Awake(){

		boogieWebApi = this;

	}
	void Start(){


	
	}

	string apiResult;
	public static float downloadProgress = 0f;
	public string getOnlineAudios(){
		try{

			myBoogieWWW = new WWW (url);
			StartCoroutine (fetchOnlineAudios(myBoogieWWW));
			while (!myBoogieWWW.isDone) {
			}

			if (myBoogieWWW.error != null)
				throw new Exception();

			apiResult = myBoogieWWW.text;

		}catch(Exception){

			Debug.Log ("Do you catch");	
			apiResult = "0";//failed
		
		}
		return apiResult;

	}

	IEnumerator fetchOnlineAudios(WWW myBoogieWWW){

		yield return myBoogieWWW;
	
	}
}
