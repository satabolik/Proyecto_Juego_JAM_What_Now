using UnityEngine;
using System.Collections;

public class ControladorPersonaje : MonoBehaviour {
	public float velocidad = 0.5f;
	public float velocidadDeGiro = 0.1f;
	public GameObject personajeRenderer;
	public GameObject itemDeComienzo;
	private Vector3 pocisionActual;
	private Vector3 pocisionAnterior;
	private Animator anim;
	public bool mover = true;

	// Use this for initialization
	void Start () {
		if (itemDeComienzo != null){
			Instantiate(itemDeComienzo,transform.position,Quaternion.identity);
		}
		anim = personajeRenderer.GetComponent<Animator>();
		NotificationCenter.DefaultCenter().AddObserver(this,"HabilitarMovimiento");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (EstadoJuego.estadoJuego.personajeSeMueve){
			rigidbody2D.MovePosition(new Vector2(rigidbody2D.position.x + (velocidad * Input.GetAxis("Horizontal")),rigidbody2D.position.y + (velocidad * Input.GetAxis("Vertical"))));
			if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0){
				personajeRenderer.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,(Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg)*-1));
			}

			anim.speed = (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")))/2f;
		}
		pocisionActual = transform.position;
		anim.SetBool("moviendose",(pocisionActual != pocisionAnterior));
		pocisionAnterior = pocisionActual;
	}

	public void HabilitarMovimiento(Notification notificacion){
		this.mover = (bool)notificacion.data;
	}

}
