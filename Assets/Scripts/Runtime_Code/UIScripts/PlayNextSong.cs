using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayNextSong : MonoBehaviour {

	void OnClick(){

		MP3_Player.mp3Instance.play_random_next ();

	}

}
