using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimiteVision : MonoBehaviour {
	public bool seguir = true;
	public bool ignorar = false;
	public List<string> listaDeExcepcionesAlColisionar;

	void OnTriggerStay2D(Collider2D other){
		if (!esExcepcionColision(other.tag) && !GetIgnorar()){
			CancelInvoke();
			seguir = false;

			
		}		
	}

	void OnTriggerExit2D(Collider2D other){
		if (!esExcepcionColision(other.tag) && !GetIgnorar()){
			CancelInvoke();
			Invoke("SeguirCamara",0.1f);
		}	
	}
	void SeguirCamara(){
		seguir = true;
	}


	public bool GetSeguir(){
		return seguir;
	}

	public bool GetIgnorar(){
		return ignorar;
	}
	public void IsIgnorar(bool ignorar){
		this.ignorar = ignorar;
		if (ignorar)seguir = true;

	}

	public void SetSeguir(bool value){
		if (value){
			CancelInvoke();
			Invoke("SeguirCamara",0.5f);
		}
		else{
			this.seguir = value;
		}
	}
	/// <summary>
	/// Este metodo ira creciendo a medida que se agregen excepciones de colisiones.
	/// </summary>
	/// <returns><c>true</c>, if colision was excepcioned, <c>false</c> otherwise.</returns>
	/// <param name="tag">Tag.</param>
	private bool esExcepcionColision(string tag){
		foreach(string texto in listaDeExcepcionesAlColisionar){
			if (tag == texto)return true;
		}
		return false;
	}
}
