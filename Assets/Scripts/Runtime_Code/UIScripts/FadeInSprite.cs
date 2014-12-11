using UnityEngine;
using System.Collections;

public class FadeInSprite : MonoBehaviour {

	UISprite[] widgetSprites;
	UILabel[] widgetLabels;
	public float fadeInTime = 1f;

	void Awake(){

		widgetSprites = GetComponentsInChildren<UISprite> ();
		widgetLabels = GetComponentsInChildren<UILabel> ();
		
		foreach (var widgySprite in widgetSprites) {
			
			widgySprite.alpha = 0f;
			
		}
		
		foreach(var widgyLabel in widgetLabels){
			
			widgyLabel.alpha = 0f;
			
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	void OnEnable(){

		foreach (var widgySprite in widgetSprites) {
		
			TweenAlpha.Begin(widgySprite.gameObject, fadeInTime, 1f);
	
		}

		foreach(var widgyLabel in widgetLabels){

			TweenAlpha.Begin(widgyLabel.gameObject, fadeInTime, 1f);

		}


	}


}
