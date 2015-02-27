using UnityEngine;
using System.Collections;

public class SeguirPersonajeCamara : MonoBehaviour {
	public Transform pocisionPersonaje;
	public LimiteVision visionVertical;
	public LimiteVision visionHorizontal;
	public float velocidadDeDesface = 0.5f;
	private float copiaVelocidadDeDesface = 0f;
	public bool noSeguir = true;
	[HideInInspector]public Vector3 posicionDestino;
	private float sentido = 1f;
	private float incX = 0f;
	private float incY = 0f;
	public float tiempoReanudarCamara = 0.5f;
	public float velocidadCambioEscenario = 0.5f;
	public float divisionSentido = 1.5f;
	void Awake(){
		transform.position = pocisionPersonaje.position;
	}
	// Use this for initialization
	void Start () {
		posicionDestino = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		NotificationCenter.DefaultCenter().AddObserver(this,"CambiarEstadoCamaraHorizontal");
		NotificationCenter.DefaultCenter().AddObserver(this,"CambiarEstadoCamaraVertical");
		NotificationCenter.DefaultCenter().AddObserver(this,"CambiarSentido");
		copiaVelocidadDeDesface = velocidadDeDesface;

	}

	void CambiarEstadoCamaraVertical(Notification notificacion){
		noSeguir = (bool)notificacion.data;
		if (!noSeguir){
			CancelInvoke();
			velocidadDeDesface = velocidadCambioEscenario;
			incX = 0f;
			incY = (visionVertical.collider2D.bounds.size.y/divisionSentido) * sentido;
			visionVertical.IsIgnorar(true);
			EstadoJuego.estadoJuego.personajeSeMueve = false;
			Invoke("ReanudarCamara",tiempoReanudarCamara);
		}
		
	}
	void CambiarEstadoCamaraHorizontal(Notification notificacion){
		noSeguir = (bool)notificacion.data;
		if (!noSeguir){
			CancelInvoke();
			velocidadDeDesface = velocidadCambioEscenario;
			incY = 0f;
			incX = (visionHorizontal.collider2D.bounds.size.x/divisionSentido) * sentido;
			visionHorizontal.IsIgnorar(true);
			EstadoJuego.estadoJuego.personajeSeMueve = false;
			Invoke("ReanudarCamara",tiempoReanudarCamara);
		}
		
	}



	public void CambiarSentido(Notification notification){
		sentido = (float) notification.data;
	}
	private void ReanudarCamara(){
		velocidadDeDesface = copiaVelocidadDeDesface; 
		incX = 0f;
		incY = 0f;
		visionVertical.SetSeguir(!visionVertical.GetIgnorar());
		visionHorizontal.SetSeguir(!visionHorizontal.GetIgnorar());
		visionVertical.IsIgnorar(false);
		visionHorizontal.IsIgnorar(false);
		EstadoJuego.estadoJuego.personajeSeMueve = true;
	}
	// Update is called once per frame
	void Update () {

		if (!noSeguir){
			if (pocisionPersonaje != null && visionVertical != null && visionHorizontal != null){
				if (visionVertical.GetSeguir() ){
					posicionDestino.Set(posicionDestino.x,Mathf.Lerp(pocisionPersonaje.position.y+incY,transform.position.y,velocidadDeDesface),posicionDestino.z);

				}
				if (visionHorizontal.GetSeguir() ){
					posicionDestino.Set(Mathf.Lerp(pocisionPersonaje.position.x+incX,transform.position.x,velocidadDeDesface),posicionDestino.y,posicionDestino.z);
				}

				transform.position = posicionDestino;
			}

		}


	}
}
