using UnityEngine;
using System.Collections;

public class ControladorInterruptorDesactivarAplastadoras : AlteracionObjetoUtility {
	private bool activo = true;
	public float tiempoDeRestablecerSprite = 0.5f;

	protected override void ItemAcciona ()
	{
		if (!IsInvoking()){
			activo = !activo;
			ControladorTrampaAplastadora.SwitchTrampas(activo);
			this.CambiarSprite();
			Invoke("CambiarSprite",tiempoDeRestablecerSprite);
		}
	}

}
