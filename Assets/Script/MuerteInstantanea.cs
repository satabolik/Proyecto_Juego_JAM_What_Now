using UnityEngine;
using System.Collections;

public class MuerteInstantanea : MonoBehaviour {
	public string nombreAnimEjecutar = "";
	public float tiempoAlMatar = 2.5f;
	private Animator animPersonaje;
	void Start(){
		animPersonaje = GameObject.Find("RenderPersonaje").GetComponent<Animator>();

	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			EstadoJuego.estadoJuego.personajeSeMueve = false;
			if (nombreAnimEjecutar != ""){
				animPersonaje.speed = 1f;
				animPersonaje.SetBool(nombreAnimEjecutar,true);
			}
			Invoke("IniciarMuerte",tiempoAlMatar);
		}
	}
	// Update is called once per frame
	private void IniciarMuerte(){
		EstadoJuego.estadoJuego.MuertePersonaje();
	}
}
