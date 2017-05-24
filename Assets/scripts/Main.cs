using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public List<Material>listaSimbolos=new List<Material>();


	private GameObject[] arrayCartas;
	private List<GameObject>ListaCartas=new List<GameObject>();

	public static bool mainClick=false;
	private Carta carta1;
	private Carta carta2;

	public static int cantClicks=0;
	private float time;
	private int correcto=0;
	private bool manoCorrecta=false;
	private bool activeTime=false;
	private bool win=false;

	private bool gameOver=false;
	public float tiempoCartaVuelta;
	private string materialCartaNombre;
	public float tiempoDerrota;
	private float tiempoGame;
	private Renderer materialFondo;
	private Renderer materialBoton;
	private GameObject botonReplay;

	public AudioSource soundCorrecta;
	public AudioSource soundIncorrecta;
	public AudioSource Ambient;
	//public AudioSource source;
	// Use this for initialization
	void Start () {

		GameObject objFondo=GameObject.FindGameObjectWithTag("fondo");
		materialFondo=objFondo.GetComponent<Renderer>();
		GameObject objBoton=GameObject.FindGameObjectWithTag("boton");
		botonReplay=objBoton;
		materialBoton=objBoton.GetComponent<Renderer>();
		materialBoton.material.color=Color.black;
		materialFondo.material.color=Color.black;
		botonReplay.GetComponent<BoxCollider>().enabled=false;	


		arrayCartas=GameObject.FindGameObjectsWithTag("carta");
		for(int i=0;i<arrayCartas.Length;i++){

			ListaCartas.Add(arrayCartas[i]);
		}
			//materialCartaNombre=arrayCartas[0].GetComponent<Material>().name;
		AsignacionSimbolos();
		carta1=null;
		carta2=null;
		Ambient.Play();
		gameOver=false;

	//no funciona cuando es correcto posteriormente n se dan vuelta
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameOver){
		tiempoGame+=Time.deltaTime;
		print("tiempo game "+tiempoGame);
			print("game over "+gameOver);
			print("ganaste "+correcto);
			print("cantidad cartas"+ListaCartas.Count);
		if(activeTime){
			//*******timer****
		time+=Time.deltaTime;
		}

		if(mainClick){
			print("click main "+cantClicks);

			switch(cantClicks)
			{
			case 0:
				//1er click
						if(carta1==null){
						carta1=GetCartaActive();
						carta1.setBool();//reseteo booleano
						carta1.Vuelta_simbolo();
						manoCorrecta=false;//reseteo tb mano correcta x si es true en la anterior
						activeTime=true;//timer ativo

					//	print("main click");
				
						DeactiveCollidersCartas();
							}
			
				break;
			case 1:
				if(carta2==null){	
				carta2=GetCartaActive();//tomamos la 2da carta para compararla con la anterior
				carta2.setBool();//seteamos bool
				}
				if(carta2!=carta1){
					//print("main click 2");
					carta2.setBool();//nuevamente seteamos bool
					//print("carta click 2"+carta2);
					carta2.Vuelta_simbolo();
					if(ComparacionSimbolos(carta1,carta2)){
							soundCorrecta.Play();

							correcto++;
						print("correcto "+correcto);
						print("obj "+ListaCartas.Count/2);
								manoCorrecta=true;
							carta1.Vuelta_simbolo();
						carta1.GetComponent<BoxCollider>().enabled=false;
						carta2.GetComponent<BoxCollider>().enabled=false;
							carta1.SetSalio(true);
							carta2.SetSalio(true);
							cantClicks=0;//reseteo clicks
								carta1=null;//nulliamos 
								carta2=null;
								//correcto
							if(ChequeWin()){
								print("ganasteee "+correcto);
								gameOver=true;
								win=true;
							}
								}else{
							soundIncorrecta.Play();
									//incorrecto
									DeactiveCollidersCartas();//desactivo paraq usuario n haga click en otras cartas durante el tiempo en q se dan vuelta

									activeTime=true;//te das vuelta si sos incorrecta sn no 
									manoCorrecta=false;
									print("incorrecto");
								}
				}else{
					print("misma carta");
					carta2=null;
				}	

				break;
				
			}
			mainClick=false;
		}

		if(time>tiempoCartaVuelta&&!manoCorrecta)
		{	
			switch(cantClicks){
			case 0:
				print("tiempo cumplido primera carta");
				cantClicks+=1;
					carta1.Vuelta_carta();
					break;
			case 1:
				print("tiempo cumplido 2da carta");
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
			if(ChequeLoose()){
				gameOver=true;
				win=false;
			}
		}else{

			if(!botonReplay.GetComponent<BoxCollider>().enabled){
				botonReplay.GetComponent<BoxCollider>().enabled=true;
				materialBoton.material.color=Color.blue;
				print("boton activo");
				Ambient.Stop();//seteamos musica de fondo
			}
			if(win){
				print ("ganaste!!!!!!!!!!!!!!!!!!!!!");
				materialFondo.material.color=Color.green;
			}else{
				materialFondo.material.color=Color.red;
				print("perdiste!!!!!!!!!!!!!!!!!!1");
			}
			if(BotonReplayActive()){
				

				Reset();
				DeleteListas();
				Start();
			}
		}
	}
	

	Carta GetCartaActive(){
		Carta c=null;
		for(int i=0;i<ListaCartas.Count;i++){
			c=ListaCartas[i].GetComponent<Carta>();
			if(c.Active()){
				return c;
			}
		}
		return c;
	}


	void AsignacionSimbolos(){
		Carta c;
		for(int i=0;i<ListaCartas.Count;i++){
		
			switch(i){
			case 0:
			case 1:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[0]);
				print("nombre simbolo"+listaSimbolos[0].name);
				c.simbolo=listaSimbolos[0].name;
				break;
			case 2:
			case 3:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[1]);
				c.simbolo=listaSimbolos[1].name;
				break;
			case 4:
			case 5:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[2]);
				c.simbolo=listaSimbolos[2].name;
				break;
			case 6:
			case 7:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[3]);
				c.simbolo=listaSimbolos[3].name;
				break;
			case 8:
			case 9:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[4]);
				c.simbolo=listaSimbolos[4].name;
				break;
			case 10:
			case 11:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[5]);
				c.simbolo=listaSimbolos[5].name;
				break;
			case 12:
			case 13:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[6]);
				c.simbolo=listaSimbolos[6].name;
				break;
			case 14:
			case 15:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[7]);
				c.simbolo=listaSimbolos[7].name;
				break;
			case 16:
			case 17:
				c=ListaCartas[i].GetComponent<Carta>();
				c.SetSimbolo(listaSimbolos[8]);
				c.simbolo=listaSimbolos[8].name;
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
		for(int i=0;i<ListaCartas.Count;i++){
			
			c=ListaCartas[i].GetComponent<Carta>();
		
			c.DeactiveCollider();
		}
	}
	void ActiveCollidersCartas(){
		Carta c=null;
		print("e");
		for(int i=0;i<ListaCartas.Count;i++){
			c=ListaCartas[i].GetComponent<Carta>();
			if(!c.GetSalio()){
			c.ActiveCollider();//
			}
		}
	}
	bool ChequeWin(){
		if(correcto==ListaCartas.Count/2){

			return true;

		}
		return false;
	}
	bool ChequeLoose(){
		if(tiempoGame>=tiempoDerrota){
			
			return true;
		
		}
		return false;

	}
	void Reset(){
		for(int i=0;i<ListaCartas.Count;i++){
			Carta aux =ListaCartas[i].GetComponent<Carta>();
			aux.SetSalio(false);
			aux.Vuelta_carta();
			aux.GetComponent<BoxCollider>().enabled=true;
			aux.setBool();
		}
		botonReplay.GetComponent<BoxCollider>().enabled=false;
		botonReplay.GetComponent<Boton>().SetActiveBoton(false);
		correcto=0;

		win=false;
		manoCorrecta=false;
		tiempoGame=0;
		cantClicks=0;
		time=0;
	}
	bool BotonReplayActive(){
		Boton aux=botonReplay.GetComponent<Boton>();
		return aux.GetActiveBoton();
	}

	void DeleteListas(){
		print("camtidad CARTAS antes" +ListaCartas.Count);

			ListaCartas.RemoveRange(0,ListaCartas.Count);

		print("camtidad CARTAS despues" +ListaCartas.Count);
	}
	void DeleteArrays(){
		for(int i=0;i<arrayCartas.Length;i++){
			Destroy( arrayCartas[i].gameObject);
		}
	}


}
