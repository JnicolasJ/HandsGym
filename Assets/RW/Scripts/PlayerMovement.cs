using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.UIElements;

public class PlayerMovement : MonoBehaviourPun
{
    public UnityEngine.UI.Image EjercicioL = null;
    private ConexionDBManager m_conexionManeger = null;

    void Start()
    {
       
    }

    void Awake()
    {
        m_conexionManeger = GameObject.FindObjectOfType<ConexionDBManager>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.Q))
            { 
                Saltar();
                m_conexionManeger.UpdatePuntaje(JugadorSingleton.Instance.m_usuario);
            }
            if (Input.GetKey(KeyCode.W))
            {
                Patear();
                m_conexionManeger.UpdatePuntaje(JugadorSingleton.Instance.m_usuario);
            }

            if (Input.GetKey(KeyCode.P))
            {
                Penalizacion();
                m_conexionManeger.UpdatePenalizacion(JugadorSingleton.Instance.m_usuario);

            }

        }
    }

    public void Saltar()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Saltar");
    }

    public void Patear()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Patada");
    }
    public void Penalizacion()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Penalizacion");
    }



    /*
    private string movementAxisName;
    private string turnAxisName;
    private string playerName = "";

    private Rigidbody rb;

    private float movementInputValue;
    private float turnInputValue;
    private float originalPitch;

    PhotonView photonView;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        playerName = photonView.Owner.NickName;
        gameObject.name = playerName;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Saltar();

            }
        }
    }

    private void FixedUpdate()
    {
       // Saltar();
    }

  

   
    */

}
