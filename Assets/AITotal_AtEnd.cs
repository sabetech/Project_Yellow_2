using UnityEngine;
using System.Collections;

public class AITotal_AtEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){


	}

	public void calcAiTotal(){

		DancerAI aiScoreInfo = DanceGameManager.danceGameManager.aiGameObject.GetComponent<DancerAI> ();
		GetComponent<UILabel> ().text = (aiScoreInfo.score + aiScoreInfo.creativity_index) + "";

	}
}
