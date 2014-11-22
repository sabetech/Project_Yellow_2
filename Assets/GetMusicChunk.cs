using UnityEngine;
using System.Collections;

public class GetMusicChunk : MonoBehaviour {

	public string whichButton;
	private static int musicPageNum = 0;
	private int limit = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		if (whichButton == "next") {

			musicPageNum = musicPageNum + limit;//just so you can read it ;D 'D :b							
			PopulateMusicWindow.getMusicWindow().populateMusicWindow(musicPageNum , musicPageNum + 10);

		}

		if (whichButton == "prev") {
				
			musicPageNum = musicPageNum - limit;
			PopulateMusicWindow.getMusicWindow().populateMusicWindow(musicPageNum, musicPageNum+10);
		
		}

	}
}
