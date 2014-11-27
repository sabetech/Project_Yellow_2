using UnityEngine;
using System.Collections;

public class PlayerCreativity_AtEnd : MonoBehaviour {

	public GameObject totals;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){

		GetComponent<UILabel>().text = (50 - DanceGameManager.danceGameManager.humanGameObject.GetComponent<Dancer_Player> ().calculateCreativityIndex ())+"/50";

		//call a function to add the totals once you have the values
		totals.GetComponent<HumanTotal_AtEnd> ().calculateTotalScore ();
	}
}
