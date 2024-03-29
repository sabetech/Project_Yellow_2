﻿using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class Audio_File {

	string filename;
	string webTitle;
	int tempo; //in BPM
	//in the future we will include file type ... ie if we start supporting other audio files

	public Audio_File(string filename){
		this.filename = filename;
	}

	public Audio_File(string filename, string webTitle){

		this.filename = filename;
		this.webTitle = webTitle;

	}

	public Audio_File(){

	}

	public void setWebTitle(string webtitle){
		this.webTitle = webtitle;
	}

	public string getWebTitle(){
		return this.webTitle;
	}

	public string getAudioFileName(){
		return this.filename;
	}

	public string getShortAudioFilename(){

		int backSlashIndex = this.filename.LastIndexOf (@"\");
		int forwardSlashIndex = this.filename.LastIndexOf (@"/");
		string shortFileName = "";

		if (backSlashIndex != -1) {
		
			shortFileName = this.filename.Substring(backSlashIndex+1);
			if (shortFileName.Length >= 20){

				shortFileName = shortFileName.Substring(0, 20)+"...";

			}

		}

		if (forwardSlashIndex != -1) {
				
			shortFileName = this.filename.Substring(forwardSlashIndex+1);
			if (shortFileName.Length >= 20){
				
				shortFileName = shortFileName.Substring(0, 20)+"...";
				
			}
		
		}

		return shortFileName;
	
	}

	public void setAudioFileName(string filename){
		this.filename = filename;
	}

	/*public decimal getTempo(){

		if (this.tempo == null) 
			return 0m;		
		
		return this.tempo;

	}*/ 

	public void setTempo(int tempo){

		this.tempo = tempo;

	}

}
