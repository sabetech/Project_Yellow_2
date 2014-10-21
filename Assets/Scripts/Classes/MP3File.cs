using System.Collections;

public class MP3File : Audio_File{

	string title;
	long length;
	string artist;
	string year;
	string album;

	public MP3File(string mp3Filename, string title, 
	                  long length, 
	                  string artist, 
	                  string year, 
	                  string album
	              ):base(mp3Filename){
		
		this.title = title;
		this.length = length;
		this.artist = artist;
		this.year = year;
		this.album = album;
		
	}
	
	public MP3File() : base(){
		
	}
	
	//this is the constructor i will be using most often
	public MP3File(mp3info.mp3info mp3Information){
		this.length = mp3Information.length;
		
		if (mp3Information.hasID3v1) {
			this.title = mp3Information.id3v1.Title;
			this.artist = mp3Information.id3v1.Artist;
			this.year = mp3Information.id3v1.Year;
			this.album = mp3Information.id3v1.Album;
			
		} else {
			
			this.title = mp3Information.id3v2.Title;
			this.artist = mp3Information.id3v2.Artist;
			this.year = mp3Information.id3v2.Year;
			this.album = mp3Information.id3v2.Album;
		}
		
	}
	

	public string getTitle(){
		return this.title;
	}

	public string getMp3FileName(){
		return base.getAudioFileName ();
	}
}
