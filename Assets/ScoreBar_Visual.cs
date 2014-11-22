using UnityEngine;
using System.Collections;

public class ScoreBar_Visual : MonoBehaviour {

	private static ScoreBar_Visual scoreVisual;

	public static ScoreBar_Visual getScoreVisual(){

		return scoreVisual;

	}

	void Awake(){

		scoreVisual = this;
			
	}

	UILabel[] scoreLabels;
	Dancer_Player humanDancer;
	void Start(){

		scoreLabels = GetComponentsInChildren<UILabel> ();
		humanDancer = DanceGameManager.danceGameManager.humanGameObject.GetComponent<Dancer_Player> ();
	
	}

	public void hideScoreVisual(){

		this.gameObject.SetActive (false);
	}

	public void bringBackScoreVisual(){

		this.gameObject.SetActive (true);
	}

	void Update(){

		//show the current score of the player
		scoreLabels [1].text = humanDancer.score +"";
		//you can create some score text effect later ... not in the mood now


	}
	
}
