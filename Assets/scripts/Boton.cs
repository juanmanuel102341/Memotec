using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour {
	private bool activeBoton=false;
	private BoxCollider box;
	// Use this for initialization
	void Start () {
		box=GetComponent<BoxCollider>();
		activeBoton=false;
	}
	
	void OnMouseDown(){
		print("click boton");
		activeBoton=true;
	}
	public bool GetActiveBoton(){
		return activeBoton;
	}
	public void SetActiveBoton(bool boton){
		activeBoton=boton;
	}
	public void ActiveCollider(){
		box.enabled=true;

	}
	public void DeactiveCollider(){
		box.enabled=false;
	}
}
