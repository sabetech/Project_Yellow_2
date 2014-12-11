using UnityEngine;
using System.Collections;
using System;

public class HideMovesWidgets : MonoBehaviour {

	private Transform movesListTransform;
	private Vector3 inititalPosition;
	private Vector3 destinationPosition;

	public static HideMovesWidgets movesWidgetInstance;

	public static HideMovesWidgets getMovesWidgetInstance(){

		return movesWidgetInstance;

	}

	void Awake(){

		movesWidgetInstance = this;
		movesListTransform = GetComponent<Transform> ();
		inititalPosition = movesListTransform.position;
		
		destinationPosition = new Vector3(movesListTransform.position.x,
		                                  movesListTransform.position.y - 140f,
		                                  movesListTransform.position.z);

	}

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hideMovesWidgets(){

		TweenPosition.Begin (this.gameObject, 0.2f, destinationPosition);

	}

	public void bringBackWidgets(){

		TweenPosition.Begin (this.gameObject, 0.2f, inititalPosition);

	}


}
