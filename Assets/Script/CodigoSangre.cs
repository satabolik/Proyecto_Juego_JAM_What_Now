using UnityEngine;
using System.Collections;

public class CodigoSangre : MonoBehaviour {
	public Texture[] numerosTexturas;
	public GameObject referenciaCajaFuerte;
	public GameObject [] numerosSangre;
	private string claveGenerada = "";
	private int digitoRandom = 0;
	// Use this for initialization
	void Start () {
		digitoRandom = Random.Range(0,7);
		numerosSangre[0].renderer.material.mainTexture = numerosTexturas[digitoRandom];
		claveGenerada += digitoRandom.ToString();

		digitoRandom = Random.Range(0,7);
		numerosSangre[1].renderer.material.mainTexture = numerosTexturas[digitoRandom];
		claveGenerada += digitoRandom.ToString();

		digitoRandom = Random.Range(0,7);
		numerosSangre[2].renderer.material.mainTexture = numerosTexturas[digitoRandom];
		claveGenerada += digitoRandom.ToString();

		referenciaCajaFuerte.GetComponent<CajaFuerte>().clave = claveGenerada;
		referenciaCajaFuerte.transform.parent.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
