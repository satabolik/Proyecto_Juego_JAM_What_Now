using UnityEngine;
using System.Collections;

public class ControladorTrampaAplastadora : MonoBehaviour {
	public float velocidad = 1f;
	[Range(0,10)]
	public int intervalosDeGolpes = 0;
	public float tiempoInactivo = 1.5f;
	public float desfaceInicio = 0f;
	private Animator anim;
	private float incAnimacion = 0f;
	private static bool activado = true;
	private static bool restableceTrampas = false;
	private static int cantidadTrampas = 0;
	private static int cantidadTotalTrampas = 0;
	private bool restaurarEstadoObjetoTrampa = true;

	void Start(){
		activado = true;
		restableceTrampas = false;
		cantidadTotalTrampas++;
		anim = GetComponent<Animator>();
		anim.speed = velocidad;
		Invoke("Restablecer",desfaceInicio);
	}

	void FixedUpdate(){
		if (!activado){
			anim.SetBool("aplastar",false);
			return;
		}
		if (restableceTrampas && restaurarEstadoObjetoTrampa){
			restaurarEstadoObjetoTrampa = false;
			VerificarTrampas();
		}
		if (anim.GetBool("aplastar")){

			if (intervalosDeGolpes == 0)return;
			int ciclo = Mathf.FloorToInt(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
			if (intervalosDeGolpes-1 == ciclo){
				anim.SetBool("aplastar",false);
				Invoke("Restablecer",tiempoInactivo);
			}

		}

	}
	void VerificarTrampas(){
		cantidadTrampas++;
		if (cantidadTrampas>cantidadTotalTrampas){	
				restableceTrampas = false;
				restaurarEstadoObjetoTrampa = true;
				Invoke("Restablecer",desfaceInicio);
		}
		else{
			Invoke("VerificarTrampas",0.1f /(float)cantidadTrampas);
		}


	}
	void Restablecer(){
		incAnimacion = 0f;
		anim.SetBool("aplastar",true);
	}
	public static void SwitchTrampas(bool activar){
		activado = activar;
		cantidadTrampas=0;
		if (activado)restableceTrampas=true;
	}
}
