using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public string nombre = "";
	public string descripcion = "";
	public float tiempoDeActivo = 1.5f;
	public bool recogido = false;
	public bool deTipoSwitch = false;
	public bool renderisar = true;
	private BoxCollider2D colliderItem;

	// Use this for initialization
	void Awake(){
		colliderItem = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Usar(){

		if (deTipoSwitch){
			if (transform.childCount > 0){
				transform.GetChild(0).gameObject.SetActive(!renderer.enabled);
			}
			colliderItem.enabled = !colliderItem.enabled;
			if (renderisar){
				renderer.enabled = !renderer.enabled;
			}


		}
		else{
			CancelInvoke("DesactivarItem");
			colliderItem.enabled = true;
			if (renderisar){
				renderer.enabled = true;
			}
			Invoke("DesactivarItem",tiempoDeActivo);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !recogido){
			recogido = true;

			transform.parent = other.gameObject.transform.GetChild(1).GetChild(0);
			if (transform.childCount > 0){
				transform.GetChild(0).gameObject.SetActive(!renderer.enabled);
			}
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			DesactivarItem();
			EstadoJuego.estadoJuego.AgregarItem(gameObject);

		}
		
	}

	public void DesactivarItem(){
		colliderItem.enabled = false;
		renderer.enabled = false;
	}
}
