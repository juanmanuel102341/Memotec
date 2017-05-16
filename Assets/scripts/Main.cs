using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public List<Material>listaSimbolos=new List<Material>();
	private GameObject []arrayCartas;

	private Carta carta1;
	private Carta carta2;

	public static int cantClicks=0;
	private float time;
	private int correcto=0;
	private bool manoCorrecta=false;
	private bool activeTime=false;
	public float tiempoCartaVuelta;
	private string materialCartaNombre;
	// Use this for initialization
	void Start () {
		if(arrayCartas==null){
		arrayCartas=GameObject.FindGameObjectsWithTag("carta");
			print("cartas nros "+arrayCartas.Length/2);
		}
		//materialCartaNombre=arrayCartas[0].GetComponent<Material>().name;
		AsignacionSimbolos();
		carta1=null;
		carta2=null;
	
	//no funciona cuando es correcto posteriormente n se dan vuelta
	}
	
	// Update is called once per frame
	void Update () {
		print("clicks "+cantClicks);
		if(activeTime){
		time+=Time.deltaTime;
		}
		if(cantClicks==1&&carta1==null){
			manoCorrecta=false;//reseteo tb mano correcta x si es true en la anterior
			activeTime=true;
			carta1=GetCartaActive();
			carta1.setBool();//reseteo booleano 
			print("main click");

			DeactiveCollidersCartas();
		}else if(cantClicks==2&&carta2==null){
			print("main click 2");

			carta2=GetCartaActive();
			carta2.setBool();
			print("carta click 2"+carta2);

			if(ComparacionSimbolos(carta1,carta2)){
				print("correcto");
				correcto++;
				carta1.Vuelta_simbolo();
				manoCorrecta=true;
				cantClicks=0;//reseteo clicks
				carta1=null;//nulliamos 
				carta2=null;
				//correcto
				print("correcto "+correcto);
				ChequeWin();
			}else{
				//incorrecto
				DeactiveCollidersCartas();//desactivo paraq usuario n haga click en otras cartas durante el tiempo en q se dan vuelta

				activeTime=true;//te das vuelta si sos incorrecta sn no 
				manoCorrecta=false;
				print("incorrecto");
			}

		}

		if(time>tiempoCartaVuelta&&!manoCorrecta)
		{	print("tiempo cumplido");
			switch(cantClicks){
			case 1:
					carta1.Vuelta_carta();
					break;
			case 2:
				print("carta vuelta");
				carta2.Vuelta_carta();
				cantClicks=0;
				carta1=null;
				carta2=null;
				break;
			}
			ActiveCollidersCartas();
			activeTime=false;
			time=0;
			}			
	}
	

	Carta GetCartaActive(){
		Carta c=null;
		for(int i=0;i<arrayCartas.Length;i++){
			c=arrayCartas[i].GetComponent<Carta>();
			if(c.Active()){
				return c;
			}
		}
		return c;
	}

	void AsignacionSimbolos(){
		Carta c;
		for(int i=0;i<arrayCartas.Length;i++){
		
			switch(i){
			case 0:
			case 1:
				c=arrayCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[0]);
				print("nombre simbolo"+listaSimbolos[0].name);
				c.simbolo=listaSimbolos[0].name;
				break;
			case 2:
			case 3:
				 c=arrayCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[1]);
				c.simbolo=listaSimbolos[1].name;
				break;
			case 4:
			case 5:
				c=arrayCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[2]);
				c.simbolo=listaSimbolos[2].name;
				break;
			case 6:
			case 7:
				c=arrayCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[3]);
				c.simbolo=listaSimbolos[3].name;
				break;
			case 8:
			case 9:
				c=arrayCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[4]);
				c.simbolo=listaSimbolos[4].name;
				break;

			}

		}		

	}
	bool ComparacionSimbolos(Carta c1,Carta c2){
		if(c1!=null&&c2!=null){
			if(c1.GetSimbol()==c2.GetSimbol()){
			return true;	
		}
		}
		return false;
	}
	void DeactiveCollidersCartas(){
		Carta c=null;
		for(int i=0;i<arrayCartas.Length;i++){
			
			c=arrayCartas[i].GetComponent<Carta>();
		
			c.DeactiveCollider();
		}
	}
	void ActiveCollidersCartas(){
		Carta c=null;
		print("e");
		for(int i=0;i<arrayCartas.Length;i++){
			c=arrayCartas[i].GetComponent<Carta>();
			if(c.GetMaterial()==c.GetMaterialCarta()){
//				print("collider carta");
			//activamos colliders de cartas "q no salieron"
				c.ActiveCollider();
			}
		}
	}
	bool ChequeWin(){
		if(correcto==arrayCartas.Length/2){
			print ("ganaste");
			return true;

		}
		return false;
	}

}
