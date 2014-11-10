using UnityEngine;
using System.Collections;

public class DancerAI : MonoBehaviour {

	// Use this for initialization
	//This is the dancer AI ... this is my first attempt in life at AIs
	//be randomly be changing the dance moves in a timely accurate manner
	//probably be using a state machines
	//The states will be :
		//trying to beat creativity index
		//doing double moves to beat the current score
		//saving energy by switching moves in time

	//check whether it is the turn of the AI to dance

	enum aiDanceStates{

		enhanceCreativity, //when current creativity is higher than opponent
		beatScore,
		increaseEnergy

	}

	public string[] AvailableDances; //this will change once we have different Major dances
	int initialDanceMove;
	void Start () {
	
		initialDanceMove = Random.Range (0, 2);

	}
	
	// Update is called once per frame
	void Update () {
	


	}
}
