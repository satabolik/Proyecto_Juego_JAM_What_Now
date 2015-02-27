using UnityEngine;
using System.Collections;

public class ControladorAgua : AlteracionObjetoUtility {

	protected override void ItemAcciona ()
	{
		ActivarObjeto(true);
		gameObject.SetActive(false);
	}
}
