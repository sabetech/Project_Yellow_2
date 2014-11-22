using UnityEngine;
using System.Collections;

public class ScoreBar_AI : MonoBehaviour {

	private static ScoreBar_AI score_AI_Visual;
	
	public static ScoreBar_AI getScoreVisual(){
		
		return score_AI_Visual;
		
	}
	
	void Awake(){
		
		score_AI_Visual = this;

	}

	UILabel[] scoreLabels;
	DancerAI ai_player;
	// Use this for initialization
	void Start () {


		this.gameObject.SetActive (false);

	}


	// Update is called once per frame
	void Update () {

		if (ai_player != null)
			scoreLabels [1].text = ai_player.score + "";

	}
	
	public void hideScoreVisual(){

		this.gameObject.SetActive (false);
		
	}
	
	public void bringBackScoreVisual(){

		this.gameObject.SetActive (true);
	}

	void OnEnable(){


		if (DanceGameManager.danceGameManager.isVersusAI) {
			scoreLabels = GetComponentsInChildren<UILabel> ();
			ai_player = DanceGameManager.danceGameManager.aiGameObject.GetComponent<DancerAI> ();

		}
	}

}
