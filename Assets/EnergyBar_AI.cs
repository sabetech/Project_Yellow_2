using UnityEngine;
using System.Collections;

public class EnergyBar_AI : MonoBehaviour {

	private static EnergyBar_AI myEnergybarInstance;

	public static EnergyBar_AI getEnergyBarInstance(){
		
		return myEnergybarInstance;
		
	}
	
	
	void Awake(){
		
		myEnergybarInstance = this;

		
	}

	void Start(){
		this.gameObject.SetActive (false);
	}

	public void hideEnergyBar(){

		this.gameObject.SetActive (false);
	}

	public void bringBackEnergyBar(){

		this.gameObject.SetActive (true);
	}

}
