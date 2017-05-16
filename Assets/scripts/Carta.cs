
using UnityEngine;

public class Carta : MonoBehaviour {
	public string simbolo;
	private bool active=false;
	private MeshRenderer mesh;
	private Material materialSimbolo;
	private Material materialCarta;
	private string currentMaterial;
	private bool activeCarta=false;
	// Use this for initialization
	void Start () {
		mesh=GetComponent<MeshRenderer>();
		materialCarta=mesh.material;
		currentMaterial=materialCarta.name;
		print("nombre material "+currentMaterial);
	}
	
	void OnMouseDown(){
		
		print("click");
		Main.cantClicks+=1;
		active=true;
		mesh.material=materialSimbolo;
		simbolo=materialSimbolo.name;
		currentMaterial=materialSimbolo.name;
		print("nombre material "+currentMaterial);
		}
	public void SetSimbolo(Material _mat){
		materialSimbolo=_mat;
	}
	public bool Active(){
		return active;
	}
	public void setBool(){
		active=false;
	}
	public void DeactiveCollider(){
		
		BoxCollider aux=this.GetComponent<BoxCollider>();
		aux.enabled=false;
	}
	public void ActiveCollider(){

		BoxCollider aux=this.GetComponent<BoxCollider>();
		aux.enabled=true;
	}
	public void Vuelta_carta(){
		mesh.material=materialCarta;
		currentMaterial=materialCarta.name;
		print("nombre material "+currentMaterial);
	}
	public void Vuelta_simbolo(){
		mesh.material=materialSimbolo;
		currentMaterial=materialSimbolo.name;
		print("nombre material "+currentMaterial);
	}
	public string GetMaterial(){
		return currentMaterial; 
	}
	public string GetMaterialCarta(){
		return materialCarta.name;
	}
	public string GetSimbol(){
		return simbolo;
	}


}
