using UnityEngine;
using System.Collections;

public class MyMusicTab : MonoBehaviour {

	public GameObject myMusicTab;
	public GameObject onlineMusic;

	public static UILabel myMusicLabel;
	public static UISprite myMusicBackground;

	public static Color musicLabelColor;
	public static Color musicBackgroundColor;

	Color tempBackGround,tempLabel;

	void Awake(){

		myMusicLabel = GetComponentInChildren<UILabel> ();
		myMusicBackground = GetComponentInChildren<UISprite>();

		musicLabelColor = myMusicLabel.color;
		musicBackgroundColor = myMusicBackground.color;

	}
	

	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		if (myMusicTab.activeSelf)
			return;

		tempLabel = myMusicLabel.color;
		tempBackGround = myMusicBackground.color;

		myMusicLabel.color = musicLabelColor;
		myMusicBackground.color = musicBackgroundColor;

		OnlineMusic.onlineMusicBackground.color = tempBackGround;
		OnlineMusic.onlineMusicLabel.color = tempLabel;

		myMusicTab.SetActive (true);
		onlineMusic.SetActive (false);
		PopulateMusicWindow.isLocalWindow = true;
		CurrentAudioPlaying.audioStatus = "Now Playing";

	}
}
