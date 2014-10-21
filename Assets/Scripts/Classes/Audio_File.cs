using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class Audio_File {

	string filename;
	int tempo; //in BPM
	//in the future we will include file type ... ie if we start supporting other audio files

	public Audio_File(string filename){
		this.filename = filename;
	}

	public Audio_File(){

	}

	public string getAudioFileName(){
		return this.filename;
	}

	public void setAudioFileName(string filename){
		this.filename = filename;
	}

}
