using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

	public GameObject dancer; //dancer character to instantiate
	public static int selectedCharacter = 0;
	public Transform dancerPosition;

	private UICenterOnChild dancerCenter;
	private Vector3 myCurrentVector;

	void Start(){

		myCurrentVector = transform.localScale;

		dancerCenter = GetComponentInParent<UICenterOnChild> ();
	
	}

	bool instantiated = false;
	GameObject focusedCharacter;

	void Update (){

		if (this.gameObject == dancerCenter.centeredObject) {
				
			TweenScale.Begin (this.gameObject, 0.05f, new Vector3 (myCurrentVector.x + 0.1f,
			                                                   myCurrentVector.y + 0.1f,
			                                                   myCurrentVector.z + 0.1f));

			if (this.instantiated)
				return;

			focusedCharacter = Instantiate(dancer, dancerPosition.position, dancerPosition.localRotation) as GameObject;
			this.instantiated = true;
		
		} else {
				
			TweenScale.Begin(this.gameObject, 0.05f, new Vector3(myCurrentVector.x,
			                                                    myCurrentVector.y,
			                                                    myCurrentVector.z));		
			if (this.instantiated) {

				Destroy(focusedCharacter);
				this.instantiated = false;

			}


		}



	}

}
