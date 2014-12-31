using UnityEngine;
using System.Collections;
using System.Collections;
using System.Linq;

public class HelpTopicDisplay : MonoBehaviour {
	

	public GameObject topicWindow, parent;
	GameObject currentTopicWindow;
	Color activeTopicColor = new Color(0.002f,0.95f,0f,1f);
	Color32 defaultColor = new Color32(255,255,255,255);

	void OnClick(){

		hidePreviousTopic ();
		NGUITools.AddChild (parent,topicWindow);

		//GetComponentInChildren<UISprite> ().color = TweenColor.Begin (this.gameObject, 0.2f, activeTopicColor);
		TweenColor.Begin (this.gameObject, 0.2f, activeTopicColor);
	}

	void hidePreviousTopic(){

		GameObject.FindGameObjectsWithTag ("helpButtons").ToList ().ForEach (helpButton => resetColor (helpButton));		

		Destroy(GameObject.FindGameObjectWithTag ("helpTopic"));

	}

	void resetColor(GameObject button){

		button.GetComponentInChildren<UISprite> ().color = defaultColor;



	}
}
