using UnityEngine;
using System.Collections;

public class ColliderQueActivaObjeto : AlteracionObjetoUtility {
	public bool cambiarSpriteInstantaneamente = false;
	public bool esUnPrefab = false;
	public bool eliminarCollider = false;
	protected override void ItemAcciona ()
	{
		if (objeto != null){
			if (esUnPrefab){
				CrearObjeto(transform.position - (new Vector3(0f,0.6f,0f)));

			}
			else{
				objeto.SetActive(true);
			}
		}
		if (cambiarSpriteInstantaneamente){
			CambiarSprite();
		}
		if (eliminarCollider){
			collider2D.enabled = false;
			GetComponent<SpriteRenderer>().sortingOrder = -1;
		}
	}

}
