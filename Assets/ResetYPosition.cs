using UnityEngine;
using System.Collections;

public class ResetYPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {

		transform.position = new Vector2 (transform.position.x, 0f);

	}
	
}
