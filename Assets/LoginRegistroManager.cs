using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginRegistroManager : MonoBehaviour
{
    [Header("Registro")]
    [SerializeField] private GameObject m_registroUI = null;
    [SerializeField] private GameObject m_loginUI = null;
    [SerializeField] private InputField m_userNameInput = null;
    [SerializeField] private InputField m_userEmailInput = null;
    [SerializeField] private InputField m_userCodigo = null;
    [SerializeField] private InputField m_userPassInput = null;
    [SerializeField] private InputField m_userPassInput2 = null;
    [SerializeField] private Text m_txtError = null;

    [Header("Login")]
    [SerializeField] private InputField m_userLogin = null;
    [SerializeField] private InputField m_passLogin = null;
    [SerializeField] private Text m_txtMensajeLogin = null;

    private ConexionDBManager m_conexionManeger = null;
    private void Awake()
    {
        m_conexionManeger = GameObject.FindObjectOfType<ConexionDBManager>();
    }

    public void submitRegistro()
    {
        if(m_userNameInput.text == "" || m_userNameInput.text=="" || m_userPassInput.text =="" || m_userPassInput2.text == "")
        {
            m_txtError.text = "Por favor llena todos los campos";
            m_txtError.color = Color.red;
            return;
        }
        if (m_userPassInput.text == m_userPassInput2.text)
        {
           
            m_txtError.text = "Procesando.....";
            m_txtError.color = Color.blue;
            string codigo_instructor = null;
            if(m_userCodigo.text == "")
            {
                codigo_instructor = "";
            }
            m_conexionManeger.CrearUser(m_userNameInput.text, m_userEmailInput.text, m_userPassInput.text, m_userCodigo.text, delegate (Response response)
            {
                m_txtError.text = response.mensaje;
                if (response.done == true)
                {
                    m_txtError.color = Color.blue;
                    m_userNameInput.text = "";
                    m_userEmailInput.text = "";
                    m_userCodigo.text = "";
                    m_userPassInput.text = "";
                    m_userPassInput2.text = "";
                }
                else
                {
                    m_txtError.color = Color.red;
                }
                
               
            });
           
        }
        else
        {
            m_txtError.text = "Las contraseñas no son iguales!";
            m_txtError.color = Color.red;
        }
    }

    public void SubmitLogin()
    {
        if (m_userLogin.text == "" || m_passLogin.text == "")
        {
            m_txtMensajeLogin.text = "Por favor llena todos los campos";
            m_txtMensajeLogin.color = Color.red;
            return;
        }
        m_txtMensajeLogin.text = "Procesando.....";
        m_txtMensajeLogin.color = Color.blue;
        m_conexionManeger.CheckUser(m_userLogin.text, m_passLogin.text, delegate (Response response)
        {
            m_txtMensajeLogin.text = response.mensaje;
            if (response.done == true)
            {
                JugadorSingleton.Instance.m_usuario = response.db_usuario;
                JugadorSingleton.Instance.m_email = response.db_email;
                JugadorSingleton.Instance.m_ranking = response.db_ranking;
                JugadorSingleton.Instance.m_puntaje = response.db_puntaje;
                JugadorSingleton.Instance.m_penalizaciones = response.db_penalizacion;
                JugadorSingleton.Instance.m_sesiones = response.db_sessiones;
                JugadorSingleton.Instance.m_avatar = response.db_avatar;
                JugadorSingleton.Instance.m_x = response.db_x;
                JugadorSingleton.Instance.m_y = response.db_y;
                JugadorSingleton.Instance.m_z = response.db_z;
                JugadorSingleton.Instance.m_cod_instructor = response.db_cod_instructor;

                SceneManager.LoadScene("Perfil");
            }
            else
            {
                m_txtMensajeLogin.color = Color.red;
            }
        });
    }

    public void ShowLogin()
    {
        m_registroUI.SetActive(false);
        m_loginUI.SetActive(true);
    }

    public void ShowRegistro()
    {

        m_registroUI.SetActive(true);
        m_loginUI.SetActive(false);
    }

    

  
}
