using UnityEngine;
using System.Collections;

public class ControladorBarraVida : MonoBehaviour {
	private RectTransform rectangulaBarra;

	// Use this for initialization
	void Start () {
		rectangulaBarra = GetComponent<RectTransform>();
		//Debug.Log("tamanio de rectangulo");
	}
	
	// Update is called once per frame
	void Update () {
		rectangulaBarra.sizeDelta = new Vector2((float)EstadoJuego.estadoJuego.hpActual,rectangulaBarra.rect.height);
	}
}
