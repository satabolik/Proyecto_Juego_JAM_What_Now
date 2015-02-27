using UnityEngine;
using System.Collections;

public class PantallaFade : MonoBehaviour {
	/*public float tiempoDeFade = 0.5f;
	public Color colorPorDefecto;*/
	[HideInInspector]
	public static PantallaFade myFade;
	[HideInInspector]
	public bool activo = false;
	private SpriteRenderer spRender;
	private const float INC_DE_ALFA = 0.025f;
	private float canalAlfa = 0f;
	// Use this for initialization
	void Awake(){
		if (myFade == null){
			myFade = this;
			DontDestroyOnLoad(gameObject);
		}
		else{
			Destroy(gameObject);
		}
		spRender = GetComponent<SpriteRenderer>();
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.transform.position;
	}

	public void ActivarFade(float tiempoDeFade,bool sinRegreso){
		ActivarFade(tiempoDeFade,sinRegreso,Color.black);
	}
	public void ActivarFade(float tiempoDeFade,bool sinRegreso, Color color){
		canalAlfa = 0f;
		ActivarFade(tiempoDeFade,sinRegreso,color,true);
		
	}
	private void ActivarFade(float tiempoDeFade,bool sinRegreso, Color color,bool aparece){
		activo = true;
		CambiarColorRenderer(color,canalAlfa);
		StartCoroutine(AjustarAlfa(aparece,(INC_DE_ALFA*tiempoDeFade),sinRegreso));
		
	}
	public void ActivarFadeDescenso(float tiempoDeFade,Color color){
		canalAlfa = 1f;
		ActivarFade(tiempoDeFade,false,color,false);
	}
	public void ActivarFadeDescenso(float tiempoDeFade){
		ActivarFadeDescenso(tiempoDeFade,Color.black);
	}
	private IEnumerator AjustarAlfa(bool sumar, float tiempo, bool sinRegreso){

		yield return new WaitForSeconds(tiempo);
		if (sumar && canalAlfa < 1f){
			canalAlfa +=  INC_DE_ALFA;
		}
		else if (!sumar && canalAlfa > 0f){
			canalAlfa -=  INC_DE_ALFA;
		}
		else{
			if (canalAlfa<1f)activo = false;
			yield break;
		}
		CambiarColorRenderer(canalAlfa);
		if (!sinRegreso && esAprox(canalAlfa,1f,INC_DE_ALFA) && sumar){
			sumar = false;
		}
		StartCoroutine(AjustarAlfa(sumar,tiempo,sinRegreso));
	}
	public void CambiarColorRenderer(Color color, float canalAlfa){
		spRender.color = new Color(color.r,color.g,color.b,canalAlfa);
	}
	public void CambiarColorRenderer(float canalAlfa){
		CambiarColorRenderer(spRender.color,canalAlfa);
	}
	private bool esAprox(float a, float b, float toletancia){
		return Mathf.Abs(a - b) < toletancia;
	}
}
