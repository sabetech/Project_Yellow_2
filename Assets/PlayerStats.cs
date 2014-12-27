using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public string info = "";
	TextMesh myPlayerInfoDisplay;
	float lerpLimit, lerpTime = 0f;
	float lerpSpeed = 0.2f;
	Transform cachedTransform;
	Vector3 cachedNewPosition;
	const float SCALE_SIZE = 0.05f;
	const float END_POS = 1f;

	public static bool isBusy = false;
	// Use this for initialization
	void Start () {

		cachedTransform = transform;
		cachedNewPosition = new Vector3 (cachedTransform.localPosition.x,
		                                 cachedTransform.localPosition.y + END_POS,
		                                 cachedTransform.localPosition.z);

		myPlayerInfoDisplay = GetComponent<TextMesh> ();
		lerpLimit = (float)info.Length;

		//remember if the score is above 7, do cycle colors
		//else be normal
		isBusy = true;
	}

	int currentWord;
	bool lerpingIsDone = false;
	float lerpPosTime = 0;

	// Update is called once per frame
	void Update () {

		if (lerpingIsDone)
			return;

		//make sure the Y rotation of this prefab is facing the camera tilted a little
		lerpTime += Time.deltaTime / lerpSpeed;
		currentWord = Mathf.FloorToInt(Mathf.Lerp (0, lerpLimit, lerpTime));

		myPlayerInfoDisplay.text = info.Substring (0, currentWord);

		if (lerpTime >= 1f) {
			lerpingIsDone = true;
			smoothScaleOut();
			smoothMoveUp();
			destroyTextMesh ();
		}

	}

	void smoothScaleOut(){

		TweenScale.Begin (this.gameObject, 0.3f, new Vector3 (cachedTransform.localScale.x + SCALE_SIZE,
		                                                      cachedTransform.localScale.y + SCALE_SIZE,
		                                                      cachedTransform.localScale.z + SCALE_SIZE));
	}

	void smoothMoveUp(){

		TweenPosition.Begin(this.gameObject,3.5f,new Vector3(cachedTransform.localPosition.x,
		                                                     cachedTransform.localPosition.y+ END_POS,
		                                                     cachedTransform.localPosition.z));
	
	}

	void destroyTextMesh(){
		isBusy = false;
		Destroy (this.gameObject, 1.5f);

	}
	
}
