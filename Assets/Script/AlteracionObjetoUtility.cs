using UnityEngine;
using System.Collections;

public abstract class AlteracionObjetoUtility : GatilladorDeItem
{
	public GameObject objeto;
	public Sprite [] listaDeSprite;
	private int indiceSprite = 0;

	public void CrearObjeto(Vector3 pocision){
		
		Instantiate(objeto,pocision,Quaternion.identity);
	}
	public void CrearObjeto(){
		CrearObjeto(transform.position);
	}
	public void CrearObjeto(float tiempo){
		Invoke("CrearObjeto",tiempo);
	}
	public void ActivarObjeto(bool seActiva){
		objeto.SetActive(seActiva);
	}
	public void CambiarSprite(){
		if (++indiceSprite >= listaDeSprite.Length)indiceSprite=0;
		CambiarSprite(indiceSprite);
	}
	public void CambiarSprite(int indice){
		if (indice >= listaDeSprite.Length)Debug.LogError("el indice excede al listado de sprite, objeto: " + gameObject.name);
		GetComponent<SpriteRenderer>().sprite = listaDeSprite[indice];
	}
}

