using UnityEngine;
using System.Collections;

public class EnergyBar_Visual : MonoBehaviour {

	private static EnergyBar_Visual myEnergybarInstance;

	public static EnergyBar_Visual getEnergyBarInstance(){

		return myEnergybarInstance;

	}


	void Awake(){

		myEnergybarInstance = this;

	}
	
	public void hideEnergyBar(){

		this.gameObject.SetActive (false);

	}

	public void bringBackEnergyBar(){

		this.gameObject.SetActive (true);

	}

	void energyChanged(){

		//NGUIMath.Lerp ();

	}

}
