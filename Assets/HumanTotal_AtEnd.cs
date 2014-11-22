using UnityEngine;
using System.Collections;

public class HumanTotal_AtEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){

		Dancer_Player humanDanceinfo = DanceGameManager.danceGameManager.humanGameObject.GetComponent<Dancer_Player> ();
		int totalScore = humanDanceinfo.score + humanDanceinfo.creativity_index;
		GetComponent<UILabel> ().text = totalScore + "";
		//remember to check if the game is a vrs game or single playerREMEMBER

		if (DanceGameManager.danceGameManager.isVersusAI) {
				
			if (PlayerInfo.playerInfo.vrsAIScore < totalScore) {

				PlayerInfo.playerInfo.vrsAIScore = totalScore;
				//awesome effect about breaking high score!

			}

		} else {

			if (PlayerInfo.playerInfo.score < totalScore){

				PlayerInfo.playerInfo.score = totalScore;

			}

		}

		//probably ask this guy to check if the highscore for the human player is more or less and decide to save it a  highscore or not
		PlayerInfo.playerInfo.savePlayerInfo ();

	}
}
