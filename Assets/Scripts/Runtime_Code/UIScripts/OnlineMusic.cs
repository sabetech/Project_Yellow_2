using UnityEngine;
using System.Collections;

public class OnlineMusic : MonoBehaviour {


	public GameObject onlineMusic;
	public GameObject myMusic;

	public static UILabel onlineMusicLabel;
	public static UISprite onlineMusicBackground;

	Color tempBackgroundColor, tempLabelColor;

	// Use this for initialization
	void Awake () {

		onlineMusicLabel = GetComponentInChildren<UILabel> ();
		onlineMusicBackground = GetComponentInChildren<UISprite> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){

		if (onlineMusic.activeSelf)
			return;

		tempBackgroundColor = onlineMusicBackground.color;
		tempLabelColor = onlineMusicLabel.color;

		onlineMusicLabel.color = MyMusicTab.musicLabelColor;
		onlineMusicBackground.color = MyMusicTab.musicBackgroundColor;

		MyMusicTab.myMusicBackground.color = tempBackgroundColor;
		MyMusicTab.myMusicLabel.color = tempLabelColor;

		onlineMusic.SetActive (true);
		myMusic.SetActive (false);
		PopulateMusicWindow.isLocalWindow = false;

		//fetch from rasheeda's api the audios

	}
}
