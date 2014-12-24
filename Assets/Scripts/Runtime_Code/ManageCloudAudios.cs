using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;


public class ManageCloudAudios : MonoBehaviour {

	private List<Audio_File> audioFiles;
	void OnEnable(){

		StartCoroutine (getOnlineMusicList ());

	}
	
	IEnumerator getOnlineMusicList(){
		//all of this can be done in a coroutine so it doesn't hog the game
		if (this.gameObject.transform.childCount == 0) {
			audioFiles = new List<Audio_File>();
			
			//there are no online music loaded so loadit
			string result = BoogieWebApi.boogieWebApi.getOnlineAudios();
			yield return result;
			if (result == "0"){
				
				CurrentAudioPlaying.audioStatus = "No Music, Internet not Connected";
				return false;
				
			}
			
			var jsonResult = JSON.Parse(result);
			
			//Debug.Log(jsonResult.Count);
			
			for(int i=0;i<jsonResult.Count;i++){
				string audioLink = jsonResult[i]["songsData"]["url"];
				
				//Debug.Log ("audio link " +audioLink);
				
				if (audioLink.EndsWith(".mp3"))
					audioFiles.Add(new Audio_File(jsonResult[i]["songsData"]["url"], jsonResult[i]["songsData"]["title"]));
				
				
			}
			
			//Debug.Log (jsonResult[0]["songsData"]["url"]);
			PopulateMusicWindow.musicWindowInstance.populateOnlineMusicWindow(audioFiles);
			
		}

	}

}
