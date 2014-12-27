
using System.Collections.Generic;
using System.IO;

public class SearchForMp3Files{


	string[] mp3Files;
	//int count = 0;
	List<Audio_File> audio_files;

	public void searchDirectory(string persDataPath){
		//call DirSearch here with a starting directory

		audio_files = new List<Audio_File> ();
		DirSearch(persDataPath);//+"\\Music"
		//serialization goes here :D
		//after you get all mp3, create a tree with it and serialize the tree


	}


	void DirSearch(string sDir) 
	{
		try	
		{
			foreach (string d in Directory.GetDirectories(sDir)) 
			{
				//count++;//this can used as progress bar though
				foreach (string f in Directory.GetFiles(d)) 
				{
					if (!f.EndsWith(".mp3")){
						continue;
					}
					//check if its bitrate can be converted to an int and less than 128
					//check i get an int or long as length and do something
					//thank you Lord

					audio_files.Add(new Audio_File(f));
				}
				DirSearch(d);
			}
		}
		catch (System.Exception) 
		{

		}
	}

	//well in the future you can serialize after storing in list
	public List<Audio_File> getAudioFiles(){
		return this.audio_files;
	}
}
