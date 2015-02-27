using UnityEngine;
using System.Collections;

public class DetectorDeAcercamientoEnemigo : GatilladorDeItem {
	public int vida = 100;
	public int danioRecibido = 25;
	public float radio = 0.5f;
	public LayerMask capaAFiltrar;
	public LayerMask capaAFiltrar2;
	public float velocidad = 0.5f;
	private Vector3 pocisionInicial;
	private float inc = 0f;
	private bool impactado = false;
	public float tiempoDeRestablecerImpacto = 1f;
	public Color colorAlSerDaniado;
	private ColorControl colorControl;
	// Use this for initialization
	void Start () {
		colorControl = GetComponent<ColorControl>();
		pocisionInicial = transform.position;
	}

	void FixedUpdate(){
		if (EstadoJuego.estadoJuego.personajeSeMueve && !impactado){
			RaycastHit2D ray = Physics2D.CircleCast(new Vector2(transform.position.x,transform.position.y),radio,(Vector2)transform.position,0.5f,capaAFiltrar.value);
			if (ray.transform != null){
				Debug.DrawLine(transform.position,ray.transform.position);
				RaycastHit2D raylineal = Physics2D.Linecast(transform.position,ray.transform.position,capaAFiltrar2);
				if (ray.transform.gameObject.tag == "Player" && raylineal.transform == null){

					transform.position = Vector2.Lerp(transform.position,ray.transform.position,velocidad+inc);
					inc += Time.deltaTime/500f;
				}
				else{
					inc = 0f;
				}
			}
			else{
				transform.position = Vector2.Lerp(transform.position,pocisionInicial,(velocidad/2));

			}
		}

	}
	public void QuitarVida(){
		vida -= danioRecibido;
		if (vida <= 0){
			Destroy(gameObject);
		}
		impactado = true;
		colorControl.CambiarColor(gameObject,colorAlSerDaniado,0.3f);
		Invoke("RestablecerEstado",tiempoDeRestablecerImpacto);
	}

	private void RestablecerEstado(){
		impactado = false;
	}
	
	protected override void ItemAcciona ()
	{
		QuitarVida();
	}
}
