
using System.Collections.Generic;
using System.IO;

public class SearchForMp3Files{


	string[] mp3Files;
	int count = 0;
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
					audio_files.Add(new Audio_File(f));
				}
				DirSearch(d);
			}
		}
		catch (System.Exception excpt) 
		{

		}
	}

	//well in the future you can serialize after storing in arraylist
	public List<Audio_File> getAudioFiles(){
		return this.audio_files;
	}
}
