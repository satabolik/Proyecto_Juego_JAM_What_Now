using UnityEngine;
using System.Collections;

public class Botiquin : MonoBehaviour {
	public int cantidadQueCura = 10;
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			EstadoJuego.estadoJuego.CurarHP(cantidadQueCura);
			Destroy(gameObject);
			
		}
	}
}
