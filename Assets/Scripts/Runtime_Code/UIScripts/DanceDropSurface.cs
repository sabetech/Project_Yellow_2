using UnityEngine;

[AddComponentMenu("NGUI/Examples/Drag and Drop Surface")]
public class DanceDropSurface : MonoBehaviour {

	//public GameObject energyBar;
	private GameObject previousDroppedDance = null;
	Dancer_Player myDancePlayer;
	//public UILabel theScore;

	//UISlider energyLevel;

	// Use this for initialization
	void Start () {

		//energyLevel = energyBar.GetComponent<UISlider> ();
		myDancePlayer = GetComponent<Dancer_Player> ();

	}
	

	void OnDrop (GameObject go)
	{
		//Debug.Log ("Hey whats up");
		if (go == null)
			return;

		DragDropItem ddo = go.GetComponent<DragDropItem> ();

		string theDance = go.GetComponentInChildren<DanceDragItem> ().dance;

		int theDanceHash = Animator.StringToHash (theDance);

		myDancePlayer.changeDance (theDanceHash, 0.05f);

		makeDraggedDanceActive (go);

		if ((previousDroppedDance != null) && (previousDroppedDance != go)) {
				
			previousDroppedDance.GetComponentInChildren<UISlicedSprite>().color = Color.white;
			previousDroppedDance.GetComponent<DanceDragItem>().isActiveDance = false;
			//energyLevel.sliderValue += 0.3f;

		}
		previousDroppedDance = go;

		//theScore.text = myDancePlayer.score + "";
	}


	void makeDraggedDanceActive(GameObject go){

		//make the dropped object active
		if (go != null) {
			go.GetComponentInChildren<UISlicedSprite> ().color = Color.red;
			go.GetComponent<DanceDragItem> ().isActiveDance = true;		
		}



	}
	
	// Update is called once per frame
	void Update () {

		//energyLevel.sliderValue -= myDancePlayer.currentEnergyRate * Time.deltaTime;
		//myDancePlayer.currentEnergyRate += myDancePlayer.stamina * Time.deltaTime;
	
	}
}

