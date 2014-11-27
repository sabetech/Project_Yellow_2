using UnityEngine;
using System.Collections;

public class AudioStatus : MonoBehaviour {

	public UILabel audioStatus;

	void Update(){

		audioStatus.text = CurrentAudioPlaying.audioStatus;

	}


}
