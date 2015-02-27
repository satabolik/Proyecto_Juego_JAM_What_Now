using UnityEngine;
using System.Collections;

public class ColorControl:MonoBehaviour{

	public void CambiarColor(GameObject objeto, Color color, float tiempoDeDuracion){
		StopAllCoroutines();
		objeto.GetComponent<SpriteRenderer>().color = new Color(color.r,color.g,color.b);
		StartCoroutine(RestablecerColor(objeto,tiempoDeDuracion,0.01f));

	}
	private IEnumerator RestablecerColor(GameObject objeto, float tiempo, float num){
		yield return new WaitForSeconds(tiempo);
		if (objeto == null)yield break;
		Color colorActual = objeto.GetComponent<SpriteRenderer>().color;
		objeto.GetComponent<SpriteRenderer>().color = Color.Lerp(colorActual,Color.white,num);
		if (num < 1f){
			StartCoroutine(RestablecerColor(objeto,tiempo/2f,num+0.01f));
		}
	}
}
