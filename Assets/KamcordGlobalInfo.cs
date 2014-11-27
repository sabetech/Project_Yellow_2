using UnityEngine;
using System.Collections;

public class KamcordGlobalInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Kamcord.SetYouTubeSettings ("This is my dancing experience with Boogie Loops", "Dance, Azonto, GH, games, Animax, android games");
		Kamcord.SetDefaultTweet ("Hey watch my dance video with Boogie Loops");
		Kamcord.SetTwitterDescription ("Breaking up some cools Azonto Moves");
		Kamcord.SetFacebookSettings ("Boogie Loops Dance","Boogie Loops","Check me out as dance off of stage with Boogie Loops");


		//Kamcord.SetShareTargets (Kamcord.ShareTarget.Twitter,
		//                        Kamcord.ShareTarget.Facebook,
		//                        Kamcord.ShareTarget.YouTube, null);

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
