using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CajaFuerte : MonoBehaviour {
	/// <summary>
	/// textos de digitos ingresados.
	/// </summary>
	public Text [] digito = new Text[3];
	/// <summary>
	/// Clave de la caja fuerte
	/// </summary>
	public string clave = "123";
	/// <summary>
	/// clave ingresada para comparar con la clave.
	/// </summary>
	private string claveIngresada="";
	/// <summary>
	/// indica la posicion actual que se esta ingresando.
	/// </summary>
	private int posDigitada = 0;
	/// <summary>
	/// cantidad de digitos que compone la caja fuerte.
	/// </summary>
	private const int NUMERO_DE_DIGITOS = 7;
	/// <summary>
	/// posicion actual de la rueda de la caja fuerte;
	/// </summary>
	private int posActualRueda = 0;
	/// <summary>
	/// booleano que determine que al mover la rueda habilite un tiempo de pausa entre movimientos.
	/// </summary>
	private bool controlActivo = true;
	/// <summary>
	/// tiempo que se espera al volver a girar la rueda.
	/// </summary>
	public float MovimientoRueda = 0.25f;
	/// <summary>
	/// renderer de puerta cerrada
	/// </summary>
	public SpriteRenderer rendererPuertaCerrada;
	/// <summary>
	/// renderer de puerta abirta al abrir caja fuerte se visualisa esta.
	/// </summary>
	public SpriteRenderer rendererPuertaAbierta;
	/// <summary>
	/// tiempo que se demora cada rotacion de la rueda.
	/// </summary>
	public float TiempoPorMovimiento = 0.1f;

	public GameObject contenidoDeLaCajaFuerteQueSeActiva;

	private bool seAbrio = false;
	// Use this for initialization
	
	void Start () {
		if (digito[0]== null || digito[1]== null || digito[2]== null){
			Debug.LogError("DEBE INGRESAR TODOS LOS TEXTOS DE DIGITOS");
		}
		digito[0].text="";
		digito[1].text="";
		digito[2].text="";
	}
	
	// Update is called once per frame
	void Update () {
		if (controlActivo){
			if(Input.GetAxis("Horizontal") > 0f){
				GirarHaciaLaDerecha();
				//controlActivo = false;
			}
			if(Input.GetAxis("Horizontal") < 0f){
				GirarHaciaLaIzquierda();
				//controlActivo = false;
			}
			if (Input.GetButtonDown("Fire1")){
				IngresarDigito();
			}
			if (Input.GetButtonDown("Fire2")){
				//aqui se debe reiniciar estado y desactivarse.
				ReiniciarValores();
			}
		}

	}

	public void GirarHaciaLaDerecha(){
		posActualRueda = (posActualRueda < NUMERO_DE_DIGITOS)?posActualRueda+1:0;
		StartCoroutine(RotarRueda(45f,MovimientoRueda,TiempoPorMovimiento,0f));
	}
	public void GirarHaciaLaIzquierda(){
		posActualRueda = (posActualRueda > 0)?posActualRueda-1:NUMERO_DE_DIGITOS;
		StartCoroutine(RotarRueda(-45f,MovimientoRueda,TiempoPorMovimiento,0f));
	}

	public void IngresarDigito(){
		if (posDigitada < digito.Length){
			digito[posDigitada].text = posActualRueda.ToString();
			claveIngresada += posActualRueda.ToString();
			posDigitada++;
		}
		if (posDigitada >= digito.Length){
			ComprobarClave();
		}
	}
	public void ComprobarClave(){
		if (IsInvoking("ReiniciarValores"))return;
		if (claveIngresada == clave){
			///aqui debe ejecutar la accion de exito.
			Debug.Log("Se abre la caja fuerte!!!");
			rendererPuertaCerrada.enabled = false;
			rendererPuertaAbierta.enabled = true;
			renderer.enabled = false;
			contenidoDeLaCajaFuerteQueSeActiva.SetActive(true);
			seAbrio = true;
		}
		else{
			///en caso de no exito.
			Debug.Log("NO se abrio");
		}
		Invoke("ReiniciarValores",1.5f);

	}
	public void ReiniciarValores(){
		if (seAbrio){
			GameObject.Find("caja_fuerte").GetComponent<ColliderQueActivaObjeto>().CambiarSprite();
			Destroy(transform.parent.gameObject);
		}
		claveIngresada="";
		posDigitada=0;
		posActualRueda=0;
		transform.rotation = Quaternion.identity;
		digito[0].text="";
		digito[1].text="";
		digito[2].text="";
		controlActivo = true;
		transform.parent.gameObject.SetActive(false);
		rendererPuertaCerrada.enabled = true;
		rendererPuertaAbierta.enabled = false;
		renderer.enabled = true;
	}
	public IEnumerator RotarRueda(float direccion, float inc,float tiempo,float gradoInicio){
		controlActivo = false;
		yield return new WaitForSeconds(tiempo);
		if(Mathf.Abs(gradoInicio) < Mathf.Abs(direccion)){
			gradoInicio += (inc * Mathf.Sign(direccion));
			transform.Rotate(0f,0f,(inc * Mathf.Sign(direccion)));
			//Debug.Log("rotacion : " + transform.rotation.z + "grado: " + gradoInicio);
			StartCoroutine(RotarRueda(direccion,inc,tiempo,gradoInicio));
		}
		else{
			transform.Rotate(0f,0f,direccion-(gradoInicio));
			controlActivo = true;
		}


	}

	void OnEnable(){
		EstadoJuego.estadoJuego.personajeSeMueve = false;
		transform.parent.position = Camera.main.transform.position;
	}
	void OnDisable(){
		EstadoJuego.estadoJuego.personajeSeMueve = true;
	}
}
