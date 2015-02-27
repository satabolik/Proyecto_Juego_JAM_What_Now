using UnityEngine;
using System.Collections;

public class FinPartida : MonoBehaviour {
	public GameObject pantallaFin;
	public GameObject HudOcultar;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			pantallaFin.SetActive(true);
			EstadoJuego.estadoJuego.personajeSeMueve = false;
			HudOcultar.SetActive(false);
		}
	}

}
