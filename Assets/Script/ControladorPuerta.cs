using UnityEngine;
using System.Collections;

public class ControladorPuerta : GatilladorDeItem {
	public bool llave = false;
	public bool puertaAbierta = false;
	public Sentido sentido;
	public bool unSentido = false;
	public GameObject objetoNoPasar;

	protected override void ItemAcciona ()
	{
		llave = false;
		AbrirPuerta();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" && !llave){
			AbrirPuerta();

		}
		
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){

			if (sentido.ToString() == "Horizontal"){
				NotificationCenter.DefaultCenter().PostNotification(this,"CambiarSentido",Mathf.Sign(other.transform.position.x-transform.position.x));
			}
			else{
				NotificationCenter.DefaultCenter().PostNotification(this,"CambiarSentido",Mathf.Sign(other.transform.position.y - transform.position.y));
			}
			CerrarPuerta();		
		}
		
	}

	public void AbrirPuerta(){
		if (unSentido){
			if (objetoNoPasar != null)objetoNoPasar.SetActive(false);
		}
		gameObject.collider2D.isTrigger = true;
		puertaAbierta = true;
		NotificationCenter.DefaultCenter().PostNotification(this,"CambiarEstadoCamara" + sentido.ToString(),true);
	}
	public void CerrarPuerta(){
		gameObject.collider2D.isTrigger = false;
		puertaAbierta = false;
		NotificationCenter.DefaultCenter().PostNotification(this,"CambiarEstadoCamara" + sentido.ToString(),false);
		if (unSentido){
			if (objetoNoPasar != null)objetoNoPasar.SetActive(true);
		}
		EstadoJuego.estadoJuego.RespaldarPosicionJugador();
	}
}
public enum Sentido{
	Horizontal,Vertical
}