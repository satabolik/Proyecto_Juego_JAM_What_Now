using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualizadorVida : MonoBehaviour {
	private Text uiTextoVida;
	void Start(){
		uiTextoVida = GetComponent<Text>();

	}
	void Update () {
		uiTextoVida.text = "Vidas Restantes: " + EstadoJuego.estadoJuego.vidaActual.ToString();
	}
}
