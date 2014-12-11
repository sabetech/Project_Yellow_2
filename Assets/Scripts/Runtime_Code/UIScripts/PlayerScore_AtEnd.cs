using UnityEngine;
using System.Collections;

public class PlayerScore_AtEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){

		GetComponent<UILabel> ().text = DanceGameManager.danceGameManager.humanGameObject.GetComponent<Dancer_Player> ().score + "";

	}
}
