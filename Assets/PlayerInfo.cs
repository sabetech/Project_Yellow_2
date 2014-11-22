using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerInfo : MonoBehaviour {

	public static PlayerInfo playerInfo;
	public int score;
	public int vrsAIScore;

	void Awake(){

		if (playerInfo == null) {
				
			DontDestroyOnLoad (gameObject);	
			playerInfo = this;

			loadPlayerInfo();
	
		} else if (playerInfo != null) {

			Destroy(gameObject);

		}
	
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void savePlayerInfo(){

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath +"/playerInfo.dat");

		MyPlayerInfo info = new MyPlayerInfo (score, vrsAIScore);
		bf.Serialize (file, info);
		file.Close ();

	}

	public void loadPlayerInfo(){

		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
				
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			MyPlayerInfo info = (MyPlayerInfo)bf.Deserialize (file);

			file.Close ();
			score = info.highscore;
			vrsAIScore = info.vrsAIHighScore;
		} else {

			score = 0;
			vrsAIScore = 0;

		}

	}
}

[Serializable]
class MyPlayerInfo{

	public int highscore;
	public int vrsAIHighScore;

	public MyPlayerInfo(int score, int vrsAIScore){
	
		this.highscore = score;
		this.vrsAIHighScore = vrsAIScore; 
	
	}


}
