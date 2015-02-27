using UnityEngine;
using System.Collections;

public class ControladorInterruptor : AlteracionObjetoUtility {
	public float tiempoDeRestablecerSprite = 0.5f;

	protected override void ItemAcciona ()
	{
		if (!IsInvoking()){
			this.ActivarObjeto(!objeto.activeSelf);
			this.CambiarSprite();
			Invoke("CambiarSprite",tiempoDeRestablecerSprite);
		}

	}
}
