using UnityEngine;

[AddComponentMenu("NGUI/Examples/Drag and Drop Surface")]
public class DanceDropSurface : MonoBehaviour {

	public GameObject energyBar;
	private float currentEnergyRate = 0.011f;//modify this to the character specific
	private int thePreviousDanceHash = 0;
	private GameObject previousDroppedDance = null;
	public float stamina = 0.03f;
	private int score = 0;

	public UILabel theScore;

	UISlider energyLevel;
	Animator dancerAnimator;
	// Use this for initialization
	void Start () {
		energyLevel = energyBar.GetComponent<UISlider> ();
		dancerAnimator = GetComponentInParent<Animator> ();

	}


	void OnDrop (GameObject go)
	{
		DragDropItem ddo = go.GetComponent<DragDropItem> ();

		//this is where I get the item that was dropped in this collider

		string theDance = go.GetComponentInChildren<DanceDragItem> ().dance;

		int theDanceHash = Animator.StringToHash (theDance);
		dancerAnimator.CrossFade (theDanceHash, 0.05f);

		//if the animation that was dropped is not the same as the previous then reset decreasing rate
		if (theDanceHash != thePreviousDanceHash) {
		
			currentEnergyRate = 0.01f;
		
		}

		thePreviousDanceHash = theDanceHash;

		makeDraggedDanceActive (go);

		if ((previousDroppedDance != null) && (previousDroppedDance != go)) {
				
			previousDroppedDance.GetComponentInChildren<UISlicedSprite>().color = Color.white;
			previousDroppedDance.GetComponent<DanceDragItem>().isActiveDance = false;
			energyLevel.sliderValue += 0.3f;

		}
		previousDroppedDance = go;

		score++; //very not correct!!
		theScore.text = score + "";
	}


	void makeDraggedDanceActive(GameObject go){

		//make the dropped object active
		go.GetComponentInChildren<UISlicedSprite> ().color = Color.red;
		go.GetComponent<DanceDragItem> ().isActiveDance = true;

		dancerAnimator.speed = ((dancerAnimator.speed * (float)TapForBPM.globalBpm) / (float)TapForBPM.prevGlobalBpm); 

		TapForBPM.prevGlobalBpm = TapForBPM.globalBpm;
		Debug.Log (dancerAnimator.speed);
	}


	//The Charater logic goes here
	/*
	 * ON ENERGY
	 * Measure how much one move has been danced?
	 * Decrease energy by current rate of dancer
	 * Increase Decreasing rate as dance move is in progress
	 * 
	 * ON Creativity [the idea is to dance diverse]
	 * Everytime you dance a dance, attach a number a number to
	 * 	-At the end of the game, the dances should have equivalent numbers attached to them

	 * ON ACCURACY
	 * Display a probable move the dancer has to do ... its like a remembered move
	 * OR ... the move has to begin at the beginning or 
	 * So when you drag drop, what is the state of the beat at that current moment

	 * 


	 */
	// Update is called once per frame
	void Update () {

		energyLevel.sliderValue -= currentEnergyRate * Time.deltaTime;
		currentEnergyRate += stamina * Time.deltaTime;
	
	}
}

