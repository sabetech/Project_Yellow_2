#pragma strict

//Loading effect
var loading : boolean = false;
var loadingTexture : Texture;
var size : float = 70.0;
private var rotAngle : float = 0.0;
var rotSpeed : float = 300.0;
private var scrHeight : int;
private var scrWidth : int;

//add some string to display what you are loading...

function Start(){
	
	scrHeight = Screen.height;
	scrWidth = Screen.width;

}

function Update () {
	if (loading){
		rotAngle += rotSpeed * Time.deltaTime;
	}	
}

function OnGUI() {
	if(loading){
		var pivot : Vector2 = Vector2(scrWidth/2, scrHeight/2);
		GUIUtility.RotateAroundPivot(rotAngle%360,pivot);
		GUI.DrawTexture(Rect ((scrWidth -size)/2 , (scrHeight - size)/2, size, size), loadingTexture); 
	}
}
