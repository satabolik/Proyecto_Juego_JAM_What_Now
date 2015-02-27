using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EstadoJuego : MonoBehaviour {
	public int vidaActual = 3;
	public int vidaTotal = 3;
	public int hpActual = 100;
	public int hpMaximo = 100;
	public List<GameObject> inventarioItems = new List<GameObject>();
	public static EstadoJuego estadoJuego = null;
	public Image ImageHudItemActual;
	public bool personajeSeMueve = true;
	private GameObject itemActual;
	private int indiceDeInventarioActual = 0;
	[HideInInspector]public ColorControl colorControl;
    private Vector3 posicionRespaldo;
    private Quaternion rotacionRespaldo;
	private Vector3 posicionCamara;
	// Use this for initialization
	void Awake () {

		if (estadoJuego == null){
			estadoJuego = this;
			DontDestroyOnLoad(gameObject);
			colorControl = GetComponent<ColorControl>();
		}
		else{
			estadoJuego.ImageHudItemActual = ImageHudItemActual;
			Destroy(gameObject);
		}

	}
	void Start(){
		PantallaFade.myFade.ActivarFadeDescenso(0.35f);
		RespaldarPosicionJugador();
	}
	
	// Update is called once per frame
	void Update () {
		if (personajeSeMueve){
			if (Input.GetButtonDown("Fire1")){
				UsarItemActual();
			}
			if (Input.GetButtonDown("Fire2")){
				CambiarSiguienteItem();
			}
			if (Input.GetButtonDown("Fire3")){
				CambiarAnteriorItem();
			}
		}

	}
	public void AgregarItem(GameObject objetoItem){
		inventarioItems.Add(objetoItem);
		if (inventarioItems.Count == 1){
			EquiparItem(indiceDeInventarioActual);
		}
	}
	public void CambiarSiguienteItem(){
		if (inventarioItems.Count == 0)return;
		inventarioItems[indiceDeInventarioActual].SetActive(false);
		if (indiceDeInventarioActual < (inventarioItems.Count-1)){
			indiceDeInventarioActual++;
		}
		else{
			indiceDeInventarioActual = 0;
		}
		inventarioItems[indiceDeInventarioActual].SetActive(true);
		EquiparItem(indiceDeInventarioActual);
	}
	public void CambiarAnteriorItem(){
		if (inventarioItems.Count == 0)return;
		inventarioItems[indiceDeInventarioActual].SetActive(false);
		if (indiceDeInventarioActual > 0){
			indiceDeInventarioActual--;
		}
		else{
			indiceDeInventarioActual = (inventarioItems.Count-1);
		}
		inventarioItems[indiceDeInventarioActual].SetActive(true);
		EquiparItem(indiceDeInventarioActual);
	}
	public void EquiparItem(int indice){
		itemActual = inventarioItems[indice];
		if (ImageHudItemActual != null){
			ImageHudItemActual.sprite = itemActual.GetComponent<SpriteRenderer>().sprite;
		}
	}
	public void UsarItemActual(){
		if (inventarioItems.Count == 0)return;
		itemActual.GetComponent<Item>().Usar();
	}
	public void DanioHP(int cantidad){
		hpActual=(hpActual<cantidad)?0:(hpActual-cantidad);
		if (hpActual==0){
			MuertePersonaje();
		}
		else{
			colorControl.CambiarColor(GameObject.Find("RenderPersonaje"),Color.red,0.25f);
		}

	}
	public void CurarHP(int cantidad){
	hpActual=(hpActual+cantidad>hpMaximo)?hpMaximo:(hpActual+cantidad);
		colorControl.CambiarColor(GameObject.Find("RenderPersonaje"),Color.green,0.15f);
	}
	public void ReiniciarJuego(){
		//hpActual = hpMaximo;
		indiceDeInventarioActual = 0;
		inventarioItems.Clear();
		//personajeSeMueve = true;
		vidaActual = vidaTotal;
	}
	public void MuertePersonaje(){
		PantallaFade.myFade.ActivarFadeDescenso(0.5f,Color.red);
		if (vidaActual > 0){
			vidaActual--;
			AjustarPosicionJugador();
		}
		else{
			Application.LoadLevel(Application.loadedLevel);
			ReiniciarJuego();
		}
		hpActual = hpMaximo;
		personajeSeMueve = true;
		Debug.Log("Muere el personaje, Vidas restantes: " + vidaActual);
	}
	public void RespaldarPosicionJugador(){
		posicionRespaldo = GameObject.FindGameObjectWithTag("Player").transform.position;
		rotacionRespaldo = GameObject.Find("RenderPersonaje").transform.rotation;
		Invoke("RespaldarCamara",0.15f);

	}
	private void RespaldarCamara(){
		if (personajeSeMueve){
			posicionCamara = Camera.main.transform.position;
			Debug.Log("se guarda camara");
		}
		else{
			Invoke("RespaldarCamara",0.15f);
		}

	}
	public void AjustarPosicionJugador(){
		Camera.main.transform.position = posicionCamara;
		Camera.main.GetComponent<SeguirPersonajeCamara>().posicionDestino = posicionCamara;
		GameObject.FindGameObjectWithTag("Player").transform.position = posicionRespaldo;
		GameObject.Find("RenderPersonaje").transform.rotation = rotacionRespaldo;
		GameObject.Find("RenderPersonaje").GetComponent<Animator>().SetBool("caerse",false);

	}


}
