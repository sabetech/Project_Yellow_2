using UnityEngine;
using System.Collections;

public class AICreativity_AtEnd : MonoBehaviour {

	public GameObject aiTotals;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){

		GetComponent<UILabel> ().text = (50 - DanceGameManager.danceGameManager.aiGameObject.GetComponent<DancerAI> ().calculateCreativityIndex()) + "/50";          
		aiTotals.GetComponent<AITotal_AtEnd> ().calcAiTotal ();

	}
}
