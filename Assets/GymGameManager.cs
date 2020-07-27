using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using UnityEngine.UI;

namespace Photon.Pun.Demo.PunBasics
{
    public class GymGameManager : MonoBehaviourPunCallbacks
    {
        public GameObject winnerUI;

        public GameObject player1SpawnPosition;
        public GameObject player2SpawnPosition;
        public GameObject player3SpawnPosition;
        public GameObject player0SpawnPosition;

        private GameObject player_instructor = null;
        private GameObject player_rehabilitacion = null;




        // Start Method
        void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Launcher");
                return;
            }

            if (PlayerManager.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient) 
                {
                   Debug.Log("Instancia a player_instructor");
                   player_instructor = PhotonNetwork.Instantiate(JugadorSingleton.Instance.m_avatar, new Vector3((float)JugadorSingleton.Instance.m_x, (float)JugadorSingleton.Instance.m_y, (float)JugadorSingleton.Instance.m_z), player0SpawnPosition.transform.rotation, 0);
                   var go =  PhotonNetwork.Instantiate("TextUsuarioFlot", new Vector3((float)JugadorSingleton.Instance.m_x, (float)JugadorSingleton.Instance.m_y, (float)JugadorSingleton.Instance.m_z), player0SpawnPosition.transform.rotation, 0);
                   go.GetComponent<TextMesh>().text = JugadorSingleton.Instance.m_usuario;
                 
                }
                else 
                {  
                   player_rehabilitacion = PhotonNetwork.Instantiate(JugadorSingleton.Instance.m_avatar, new Vector3((float)JugadorSingleton.Instance.m_x, (float)JugadorSingleton.Instance.m_y, (float)JugadorSingleton.Instance.m_z), player1SpawnPosition.transform.rotation, 0);
                   var go = PhotonNetwork.Instantiate("TextUsuarioFlot", new Vector3((float)JugadorSingleton.Instance.m_x, (float)JugadorSingleton.Instance.m_y, (float)JugadorSingleton.Instance.m_z), player1SpawnPosition.transform.rotation, 0);
                   go.GetComponent<TextMesh>().text = JugadorSingleton.Instance.m_usuario;
                }
            }
        }

        // Update Method
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //1
            {
                Application.Quit();
            }
        }

        // Photon Methods
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Launcher");
            }
        }

        // Helper Methods
        public void QuitRoom()
        {
            Application.Quit();
        }
    }
}


