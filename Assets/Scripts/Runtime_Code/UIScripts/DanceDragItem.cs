using System.Collections;
using UnityEngine;
using System;

[AddComponentMenu("NGUI/Examples/Drag and Drop Item")]
public class DanceDragItem : MonoBehaviour
{
	
	public string dance;
	public bool isActiveDance = false;

	Transform mTrans;
	bool mIsDragging = false;
	bool mSticky = false;
	Transform mParent;

	private Animator dancerAnimator;
	AnimatorStateInfo myAnimatorStateInfo;
	int danceHash;

	public GameObject groundEffect;
	private Transform dancerTransform;
	GameObject theActivePlayer;


	void Start(){

		theActivePlayer = DanceGameManager.activeDancePlayer;

		try{
		//get information about the human player so these draggable dances can work on him/her
		dancerAnimator = theActivePlayer.GetComponent<Animator> ();
		dancerTransform = theActivePlayer.transform;

		danceHash = Animator.StringToHash (dance);
		myAnimatorStateInfo = dancerAnimator.GetCurrentAnimatorStateInfo(0);
		}catch(Exception){
				
		}
	
	}

	/// <summary>
	/// Update the table, if there is one.
	/// </summary>
	
	void UpdateTable ()
	{
		UITable table = NGUITools.FindInParents<UITable>(gameObject);
		if (table != null) table.repositionNow = true;
	}
	
	/// <summary>
	/// Drop the dragged object.
	/// </summary>
	
	void Drop ()
	{
		// Is there a droppable container?
		Collider col = UICamera.lastHit.collider;
		DragDropContainer container = (col != null) ? col.gameObject.GetComponent<DragDropContainer>() : null;
		
		if (container != null)
		{
			// Container found -- parent this object to the container
			mTrans.parent = container.transform;
			
			Vector3 pos = mTrans.localPosition;
			pos.z = 0f;
			mTrans.localPosition = pos;
		}
		else
		{
			// No valid container under the mouse -- revert the item's parent
			mTrans.parent = mParent;
		}
		
		// Notify the table of this change
		UpdateTable();
		
		// Make all widgets update their parents
		NGUITools.MarkParentAsChanged(gameObject);
	}
	
	/// <summary>
	/// Cache the transform.
	/// </summary>
	
	void Awake () { mTrans = transform; }
	
	/// <summary>
	/// Start the drag event and perform the dragging.
	/// </summary>
	
	void OnDrag (Vector2 delta)
	{
		if (enabled && UICamera.currentTouchID > -2)
		{
			if (!mIsDragging)
			{
				mIsDragging = true;
				mParent = mTrans.parent;
				mTrans.parent = DragDropRoot.root;
				
				Vector3 pos = mTrans.localPosition;
				pos.z = 0f;
				mTrans.localPosition = pos;
				
				NGUITools.MarkParentAsChanged(gameObject);
			}
			else
			{
				mTrans.localPosition += (Vector3)delta;
			}
		}
	}
	
	/// <summary>
	/// Start or stop the drag operation.
	/// </summary>
	
	void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (isPressed)
			{
				if (!UICamera.current.stickyPress)
				{
					mSticky = true;
					UICamera.current.stickyPress = true;
				}
			}
			else if (mSticky)
			{
				mSticky = false;
				UICamera.current.stickyPress = false;
			}
			
			mIsDragging = false;
			Collider col = collider;
			if (col != null) col.enabled = !isPressed;
			if (!isPressed) Drop();
		}
	}

	/// <summary>
	/// OnClick on a dance move logic starts here
	/// </summary>
	float currentTime;
	float prevTime;
	float timeDiff, prevTimeDiff;
	bool firstClick = true;
	float[] timeDiffs = new float[15];
	int currentTimeDiffIndex = 0;

	void OnClick(){
		float normalizedTime = myAnimatorStateInfo.normalizedTime;
		if (this.isActiveDance) {

			dancerAnimator.Play(danceHash,-1, normalizedTime);

			currentTime = Time.time;
			
			timeDiff = currentTime - prevTime;
			prevTime = currentTime;

			if (timeDiff >= (prevTimeDiff * 2)){

				Debug.Log ("u have rested u taps");	
				Array.Clear (timeDiffs,0,timeDiffs.Length);
				//clear the array to put in new values

			}

			prevTimeDiff = timeDiff;

			if (firstClick) {
				firstClick = false;
				return;		
			}
			
			timeDiffs [currentTimeDiffIndex % 15] = timeDiff;

			currentTimeDiffIndex++;
		
		}

	}

	//this must be a coroutine;;
	float calcStandardDeviation(){

		float avg = calcAvg ();
		float variance = calcVariance (avg);

		return (float)Math.Sqrt (variance);

	}

	float calcAvg(){

		//first check if timeDiff is not null first before you continue;
		float sum = 0f;
		for (int i=0; i < ((currentTimeDiffIndex <= 15) ? currentTimeDiffIndex : 15); i++) {
				
			sum += timeDiffs[i];

		}
		return sum / ((currentTimeDiffIndex <= 15) ? currentTimeDiffIndex : 15);

	}

	float calcVariance(float avg){

		float[] varianceNums = new float[15];
		float sum = 0f;
		for (int i=0; i < ((currentTimeDiffIndex <= 15) ? currentTimeDiffIndex : 15); i++) {
				
			varianceNums[i] = (float)Math.Pow((timeDiffs[i] - avg), 2);
			sum += varianceNums[i];

		}

		return sum / ((currentTimeDiffIndex <= 15) ? currentTimeDiffIndex : 15);
	}

	float timeDiffCheck;
	void Update(){

		if (this.isActiveDance) {
				
			if (!firstClick){

				timeDiffCheck = Time.time - prevTime;
				//use a range here so  you catch the person if he increases speed :D
				if (timeDiffCheck >= (prevTimeDiff * 2)){ //make this * 1.5f

					firstClick = true;

					//fire up a discrepancy check!!
					//after that show the awesome effect
					StartCoroutine(awesomeEffect());

				}

			}
		
		}

	}

	IEnumerator awesomeEffect(){

		//do score calculation here and determine how much to award the player
		float sd = calcStandardDeviation () * 2;
		showEffect(Mathf.Clamp(Mathf.CeilToInt(Mathf.Abs (22f - sd)) ,0,22));

		yield return null;


	}

	void showEffect(int score){

		DanceGameManager.activeDancePlayer.GetComponent<Dancer_Player> ().score += score;
		DanceGameManager.activeDancePlayer.GetComponent<Dancer_Player> ().showScoreTextMesh (score);

	}
}
