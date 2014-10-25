using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class MusicCollection : MonoBehaviour{

	public List<Audio_File> audioLibrary;
	public GameObject mp3Player;

	void Start (){

		fetchMp3Files ();
		mp3Player.GetComponent<MP3_Player> ().play_audio_file(audioLibrary[0].getAudioFileName());

	}

	public void fetchMp3Files(){
		//unserialize and populate that window
		try{
			using(Stream aud_stream = File.Open (Application.persistentDataPath+"/audio_files_ref.dat", FileMode.Open))
			{
				BinaryFormatter binFormat = new BinaryFormatter();
				audioLibrary = (List<Audio_File>)binFormat.Deserialize(aud_stream);

			}
		}catch(IOException ioex){
			Debug.Log (ioex);	
		}
	}
}
