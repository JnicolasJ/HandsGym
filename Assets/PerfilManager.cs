using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PerfilManager : MonoBehaviour
{

	public GameObject botonSiguiente = null;
	public GameObject botonAnterior = null;
	public GameObject btnMenu = null;
	public GameObject avatar1 = null;
	public GameObject avatar2 = null;
	public GameObject avatar3 = null;
	public GameObject avatar4 = null;
	public GameObject avatar5 = null;
	public GameObject avatar6 = null;
	private int numero_avatar = 1;
	private string nombre_avatar_pref = "";
	private int numero__total_avatars = 6;
	GameObject avatar_seleccionado = null;
	[SerializeField] private Text nombre_avatar = null;
	public Text nombre_usuario = null;
	public Text puntaje_usuario = null;
	public Text ranking_usuario = null;
	public Text sesiones_usuario = null;
	public Text penalizaciones_usuario = null;

	//GameObject play2 = null;
	private ConexionDBManager m_conexionManeger = null;


	private void Awake()
	{
		m_conexionManeger = GameObject.FindObjectOfType<ConexionDBManager>();
	}

	public void updateAvatar()
	{
		m_conexionManeger.UpdateAvatar(nombre_usuario.text, nombre_avatar_pref);
		
	}
	public void ActivarAnterior()
	{
		numero_avatar = numero_avatar - 1;
		if (numero_avatar >=1)
		{
			Destroy(avatar_seleccionado, 0);
			SetAvatarSeleccionado(numero_avatar);
			updateAvatar();
			botonAnterior.SetActive(true);
			botonSiguiente.SetActive(true);

		}
		else
		{
			botonSiguiente.SetActive(true);
			botonAnterior.SetActive(false);
		}
	}
	
	
	public void ActivarSiguiente()
	{
		numero_avatar = numero_avatar + 1;
		if (numero_avatar < numero__total_avatars)
		{
			Destroy(avatar_seleccionado, 0);
			SetAvatarSeleccionado(numero_avatar);
			updateAvatar();
			botonAnterior.SetActive(true);
			botonSiguiente.SetActive(true);
		}
		else
		{
			botonSiguiente.SetActive(false);
			botonAnterior.SetActive(true);
		}
	}
	
	private void SetAvatarSeleccionado(int numero_avatar)
	{
		if (numero_avatar == 1)
		{
			avatar_seleccionado = Instantiate(avatar1, new Vector3(0f, 0.5f, -7.5f), Quaternion.Euler(0f, -180f, 0f)) as GameObject; 
			nombre_avatar.text = "FG3D_DH_Ethnic_African";
			nombre_avatar_pref = "FG3D_DH_Ethnic_African";
		}
		if (numero_avatar == 2)
		{
			avatar_seleccionado = Instantiate(avatar2, new Vector3(0f, 0.5f, -7.5f), Quaternion.Euler(0f, -180f, 0f)) as GameObject;
			nombre_avatar.text = "FG3D_DH_Ethnic_Asian_East";
			nombre_avatar_pref = "FG3D_DH_Ethnic_Asian_East";

		}
		if (numero_avatar == 3)
		{
			avatar_seleccionado = Instantiate(avatar3, new Vector3(0f, 0.5f, - 7.5f), Quaternion.Euler(0f, -180f, 0f)) as GameObject;
			nombre_avatar.text = "FG3D_DH_Ethnic_Australian";
			nombre_avatar_pref = "FG3D_DH_Ethnic_Australian";

		}
		if (numero_avatar == 4)
		{
			avatar_seleccionado = Instantiate(avatar4, new Vector3(0f, 0.5f, -7.5f), Quaternion.Euler(0f, -180f, 0f)) as GameObject;
			nombre_avatar.text = "FG3D_DH_Ethnic_Eskimo";
			nombre_avatar_pref = "FG3D_DH_Ethnic_Eskimo";

		}
		if (numero_avatar == 5)
		{
			avatar_seleccionado = Instantiate(avatar5, new Vector3(0f, 0.5f, -7.5f), Quaternion.Euler(0f, -180f, 0f)) as GameObject;
			nombre_avatar.text = "FG3D_DH_Ethnic_European_East";
			nombre_avatar_pref = "FG3D_DH_Ethnic_European_East";

		}
		if (numero_avatar == 6)
		{
			avatar_seleccionado = Instantiate(avatar6, new Vector3(0f, 0.5f, -7.5f), Quaternion.Euler(0f, -180f, 0f)) as GameObject;
			nombre_avatar.text = "FG3D_DH_Ethnic_Polynesian";
			nombre_avatar_pref = "FG3D_DH_Ethnic_Polynesian";

		}
	}
	// Use this for initialization
	void Start()
	{
		nombre_usuario.text = JugadorSingleton.Instance.m_usuario;
		puntaje_usuario.text = JugadorSingleton.Instance.m_puntaje.ToString();
		ranking_usuario.text = JugadorSingleton.Instance.m_ranking.ToString();
		sesiones_usuario.text = JugadorSingleton.Instance.m_sesiones.ToString();
		penalizaciones_usuario.text = JugadorSingleton.Instance.m_penalizaciones.ToString();
		botonSiguiente.SetActive(true);
		botonAnterior.SetActive(false);
		numero_avatar = 1;
		SetAvatarSeleccionado(numero_avatar);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ShowMenuPrincipal()
	{
		SceneManager.LoadScene("Menu");

	}
}
