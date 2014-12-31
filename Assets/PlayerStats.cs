using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public string info = "";
	TextMesh myPlayerInfoDisplay;
	float lerpLimit, lerpTime = 0f;
	public float lerpSpeed = 0.2f;
	Transform cachedTransform;
	//Vector3 cachedNewPosition;
	const float SCALE_SIZE = 0.05f;
	const float END_POS = 1f;

	public bool alive = false;
	public bool isCompliment = true;

	public bool notInGame = false;
	// Use this for initialization
	void Start () {

		cachedTransform = transform;
		//cachedNewPosition = new Vector3 (cachedTransform.localPosition.x,
		  //                               cachedTransform.localPosition.y + END_POS,
		//                                 cachedTransform.localPosition.z);

		myPlayerInfoDisplay = GetComponent<TextMesh> ();
		lerpLimit = (float)info.Length;

		alive = true;
	}

	int currentWord;
	bool lerpingIsDone = false;
	//float lerpPosTime = 0;

	// Update is called once per frame
	void Update () {

		if (lerpingIsDone)
			return;

		//make sure the Y rotation of this prefab is facing the camera tilted a little
		lerpTime += Time.deltaTime / lerpSpeed;
		currentWord = Mathf.FloorToInt(Mathf.Lerp (0, lerpLimit, lerpTime));

		myPlayerInfoDisplay.text = info.Substring (0, currentWord);

		if ((lerpTime >= 1f) && (this.isCompliment)) {

			lerpingIsDone = true;
			smoothScaleOut ();
			smoothMoveUp ();
			destroyTextMesh ();

		} else {
			if (lerpTime >= 1f){
				lerpingIsDone = true;

			}
		}

	}

	public void resetWord(){

		lerpingIsDone = false;
		//lerpTime = 0f;

	}

	public void showText(){

		lerpTime = 0f;
		lerpingIsDone = false;

	
	}

	public void setScale(){

		transform.localScale.Set (transform.localScale.x + SCALE_SIZE,
		                          transform.localScale.y + SCALE_SIZE,
		                          transform.localScale.z + SCALE_SIZE);
	
	}

	void smoothScaleOut(){

		TweenScale.Begin (this.gameObject, 0.15f, new Vector3 (cachedTransform.localScale.x + SCALE_SIZE,
		                                                      cachedTransform.localScale.y + SCALE_SIZE,
		                                                      cachedTransform.localScale.z + SCALE_SIZE));
	}

	void smoothMoveUp(){

		TweenPosition.Begin(this.gameObject,3.5f,new Vector3(cachedTransform.localPosition.x,
		                                                     cachedTransform.localPosition.y+ END_POS,
		                                                     cachedTransform.localPosition.z));
	
	}

	public void destroyTextMesh(float deadTime = 0.9f){

		alive = false;
		Destroy (this.gameObject, deadTime);

	}

	void OnDestroy(){
		//Debug.Log ("Object is destroyed");

		if ((!this.isCompliment) && (!notInGame))
			DanceGameManager.activeDancePlayer.GetComponent<Dancer_Player> ().timeLeftTxtMeshInstantiated = false;

	}
	
}
