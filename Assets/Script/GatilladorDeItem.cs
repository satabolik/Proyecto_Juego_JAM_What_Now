using UnityEngine;
using System.Collections;

public  abstract class GatilladorDeItem : MonoBehaviour {
	public string itemRequerido = "";

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Item"){
			if (other.gameObject.GetComponent<Item>().nombre == itemRequerido){
				ItemAcciona();
			}
			
		}
	}
	protected abstract void ItemAcciona();
}
