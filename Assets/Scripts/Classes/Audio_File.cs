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

	public string getShortAudioFilename(){

		int backSlashIndex = this.filename.LastIndexOf (@"\");
		int forwardSlashIndex = this.filename.LastIndexOf (@"/");
		string shortFileName = "";

		if (backSlashIndex != -1) {
		
			shortFileName = this.filename.Substring(backSlashIndex);
			if (shortFileName.Length >= 20){

				shortFileName = shortFileName.Substring(0, 20)+"...";

			}

		}

		if (forwardSlashIndex != -1) {
				
			shortFileName = this.filename.Substring(backSlashIndex);
			if (shortFileName.Length >= 20){
				
				shortFileName = shortFileName.Substring(0, 20)+"...";
				
			}
		
		}

		return shortFileName;
	
	}

	public void setAudioFileName(string filename){
		this.filename = filename;
	}

}
