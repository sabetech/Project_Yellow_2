using UnityEngine;
using System.Collections;
using System;

public class GetMusicChunk : MonoBehaviour {

	public string whichButton;
	private static int musicPageNum = 0;
	private int limit = 10;

	public GameObject nextPage, PrevPage;
	public static GetMusicChunk nextBtn, prevBtn;

	void Awake(){

		nextBtn  = nextPage.GetComponent<GetMusicChunk> ();
		prevBtn = PrevPage.GetComponent<GetMusicChunk> ();

	}

	// Use this for initialization
	void Start () {

		if (whichButton == "prev") {
				
			this.gameObject.SetActive(false);
		
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		if (whichButton == "next") {

			if (PopulateMusicWindow.musicWindowInstance.maxSongsReached){

				this.gameObject.SetActive(false);

			}
				
			musicPageNum = musicPageNum + limit;//just so you can read it ;D 'D :b							
			PopulateMusicWindow.getMusicWindow().populateMusicWindow(musicPageNum , musicPageNum + 10);

			if (musicPageNum >= limit){
				
				PrevPage.SetActive(true);
				
			}

		}

		if (whichButton == "prev") {

			musicPageNum = musicPageNum - limit;
			PopulateMusicWindow.getMusicWindow().populateMusicWindow(musicPageNum, musicPageNum + 10);

			if (musicPageNum < limit){
				
				PrevPage.SetActive(false);
				
			}

			nextPage.SetActive(true);
		
		}



	}



}
