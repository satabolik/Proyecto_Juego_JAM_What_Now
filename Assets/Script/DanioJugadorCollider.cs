using UnityEngine;
using System.Collections;

public class DanioJugadorCollider : MonoBehaviour {
	public int cantidadDanio = 1;
	public float intervaloEntreDanio = 0f;
	private bool tocando = false;

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player" && (!tocando) && EstadoJuego.estadoJuego.hpActual > 0){
			tocando = true;
			EstadoJuego.estadoJuego.DanioHP(cantidadDanio);
			Invoke("RestaurarColision",intervaloEntreDanio);
		}
	}

	public void RestaurarColision(){
		tocando = false;
	}
}
