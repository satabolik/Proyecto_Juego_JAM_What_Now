using UnityEngine;
using System.Collections;

public class ControladorLinterna : MonoBehaviour {
	private GameObject luzADesactivar;
	private bool seDesactiva = false;
	void Start(){
		luzADesactivar = GameObject.Find("luz");
	}
	void OnEnable(){
		if (seDesactiva){
			luzADesactivar.SetActive(false);
		}

	}
	void OnDisable(){
		if (!seDesactiva){
			seDesactiva = true;
		}
		else{
			luzADesactivar.SetActive(true);
		}
	}
}
