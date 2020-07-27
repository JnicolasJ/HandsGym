using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using TMPro;

public class AleatorioManager : MonoBehaviour
{
	// 1. Declaracion de las variables
	Thread receiveThread;
	UdpClient client;
	int port;

	public GameObject PlayerSerie1;
	public TextMeshProUGUI txtNombreEjercicio1;
	public TextMeshProUGUI txtNombreEjercicio2;
	public TextMeshProUGUI txtNombreEjercicio3;

	public Color color_terminado;
	AudioSource sonido_salto;
	bool inicio;
	bool saltar;
	bool patadaDerecha;
	bool patadaIzquierda;
	bool girar;
	bool abdominal;
	bool yoga;
	bool correr;
	bool serie_completa;
	int puntos_penalizacion;
	bool castigo;
	Dictionary<string, int> serie1 = new Dictionary<string, int>();

	// 2. Inicializar variable
	void Start()
	{
		port = 5065; //1 
		inicio = false;
		saltar = false; //2
		patadaDerecha = false;
		patadaIzquierda = false;
		girar = false;
		abdominal = false;
		yoga = false;
		correr = false;
		puntos_penalizacion = 0;
		serie_completa = false;
		castigo = false;
		Agregar_ejercicios_tutorial();
		//InitUDP();
	}

	private void Agregar_ejercicios_tutorial()
	{
		serie1.Add("Mano cerrada", 3);
		serie1.Add("Extension Pulgar", 3);
		serie1.Add("Extension MAM", 3);

		txtNombreEjercicio1.SetText("Mano cerrada");
		txtNombreEjercicio2.SetText("Extension Pulgar");
		txtNombreEjercicio3.SetText("Extension MAM");
	}
	private void InitUDP()
	{
		print("UDP Initialized");

		receiveThread = new Thread(new ThreadStart(EjecutarSerie));
		receiveThread.IsBackground = true;
		receiveThread.Start();

	}

	int i = 1;
	private void EjecutarSerie()
	{
		client = new UdpClient(port);
		foreach (KeyValuePair<string, int> item in serie1)
		{
			int intentos = 0;
			bool ejecutar_ejercicio = true;
			while (ejecutar_ejercicio)
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
				byte[] data = client.Receive(ref anyIP);
				string ejercicio_rehabilitacion = Encoding.UTF8.GetString(data);
				print("esperando ejercicio " + item.Key + "- recibiendo " + ejercicio_rehabilitacion);

				if (ejercicio_rehabilitacion.Equals(item.Key))
				{
					++intentos;
					print("intentos " + intentos + " de serie " + item.Value);
					RealizarEjercicio(ejercicio_rehabilitacion);

				}
				else
				{
					++puntos_penalizacion;
					print("puntos negativos " + puntos_penalizacion);
					castigo = true;
				}
				if (intentos == item.Value)
				{
					print("cambiando a otro");
					ejecutar_ejercicio = false;
				}


			}
		}
		serie_completa = true;
	}

	public void RealizarEjercicio(string e)
	{
		if (e.Equals("Mano cerrada"))
		{
			saltar = true;
		}

		if (e.Equals("Mano derecha"))
		{
			patadaDerecha = true;
		}

		if (e.Equals("Extension Pulgar"))
		{
			patadaIzquierda = true;
		}

		if (e.Equals("SpiderMan"))
		{
			girar = true;
		}

		if (e.Equals("Extension MAM"))
		{
			abdominal = true;
		}

		if (e.Equals("Mano izquierda"))
		{
			yoga = true;
		}

		if (e.Equals("Extension Pinza"))
		{
			correr = true;
		}

	}

	public void Inicio()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Inicio");
	}

	public void Saltar()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Saltar");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("InicioS");
	}

	public void PatadaDerecha()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("PatadaDerecha");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("InicioS");
	}

	public void PatadaIzquierda()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("PatadaIzquierda");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("InicioS");
	}

	public void Girar()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Girar");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("InicioS");
	}

	public void Abdominal()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Abdominal");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("InicioS");
	}

	public void Yoga()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Yoga");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("InicioS");
	}

	public void Correr()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Correr");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("InicioS");
	}

	public void Penalizacion()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Penalizacion");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Reintentar");
	}
	public void Celebracion()
	{
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Finalizacion");
		PlayerSerie1.GetComponent<Animator>().SetTrigger("Reinicio");
	}

	// 6. Verificar la variable jump para que el jugador salte y actualizrla  para el siguiente salto
	void Update()
	{
		if (saltar == true)
		{
			Saltar();
			saltar = false;
		}
		if (patadaDerecha == true)
		{
			PatadaDerecha();
			patadaDerecha = false;
		}

		if (patadaIzquierda == true)
		{
			PatadaIzquierda();
			patadaIzquierda = false;
		}

		if (girar == true)
		{
			Girar();
			girar = false;
		}

		if (abdominal == true)
		{
			Abdominal();
			abdominal = false;
		}

		if (yoga == true)
		{
			Yoga();
			yoga = false;
		}

		if (correr == true)
		{
			Correr();
			correr = false;
		}


		if (castigo == true)
		{
			Penalizacion();
			castigo = false;
		}

		if (serie_completa == true)
		{
			Celebracion();
			serie_completa = false;
		}

		if (inicio == true)
		{
			Inicio();
		}
	}
	public void ReturnMenuMain()
	{
		//client.Close();
		receiveThread.Interrupt();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
	}
}
