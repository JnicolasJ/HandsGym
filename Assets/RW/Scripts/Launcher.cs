
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Experimental.UIElements;

namespace Photon.Pun.Demo.PunBasics
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject controlPanel;

        [SerializeField]
        private Text feedbackText;

        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        bool isConnecting;

        string gameVersion = "1";

        [Space(10)]
        [Header("Custom Variables")]
        public InputField playerNameField;
        public InputField roomNameField;
        public Text textListLobby;

        [Space(5)]
        public Text playerStatus;
        public Text connectionStatus;

        [Space(5)]
        public GameObject roomJoinUI;
        public GameObject buttonLoadArena;
        public GameObject buttonJoinRoom;

        string playerName = "";
        string roomName = "";
        private ConexionDBManager m_conexionManeger = null;

        // Start Method
        void Start()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Connecting to Photon Network");
            playerNameField.text = JugadorSingleton.Instance.m_usuario;
            roomJoinUI.SetActive(false);
            buttonLoadArena.SetActive(false);
            ConnectToPhoton();
        }

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            m_conexionManeger = GameObject.FindObjectOfType<ConexionDBManager>();

        }

        public void SetPlayerName(string name)
        {
            playerName = name;
        }

        public void SetRoomName(string name)
        {
            roomName = name;
        }
        
        void ConnectToPhoton()
        {
            connectionStatus.text = "Conectando...";
            PhotonNetwork.GameVersion = gameVersion; //1
            PhotonNetwork.ConnectUsingSettings(); //2
        }

        public void JoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName; //1
                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + roomNameField.text);
                RoomOptions roomOptions = new RoomOptions(); //2
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default); //3
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby); //4
            }
        }
        //
        public void CrearSala()
        {
            if (PhotonNetwork.IsConnected)
            {
                RoomOptions roomOptions = new RoomOptions(); //2
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default); //3
                if (player_instanciate.type == "INSTRUCTOR")
                {
                    PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby);
                }
                else
                {
                    PhotonNetwork.JoinRoom(roomName,null);
                }
            }
        }
        /*********************************************************/

        public void LoadArena()
        {
            // 5
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PhotonNetwork.LoadLevel("Demo");
            }
            else
            {
                textListLobby.text = "<br> Sala: " + roomName + "<br>" +
               "En el lobby: " + PhotonNetwork.CurrentRoom.PlayerCount;
                playerStatus.text = "Minimo " + maxPlayersPerRoom.ToString() + " Players para cargar el Gym!";
            }
        }

        // Photon Methods
        public override void OnConnected()
        {
            // 1
            base.OnConnected();
            // 2
            connectionStatus.text = "Connected to Photon!";
            connectionStatus.color = Color.green;
            roomJoinUI.SetActive(true);
            buttonLoadArena.SetActive(false);
          
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            // 3
            isConnecting = false;
            controlPanel.SetActive(true);
            Debug.LogError("Desconectado. Revisa tu conexion a internet.");
        }

        public override void OnJoinedRoom()
        {
            // 4
            if (PhotonNetwork.IsMasterClient)
            {
                buttonLoadArena.SetActive(true);
                buttonJoinRoom.SetActive(false);
                playerStatus.text = "Eres el lider del Lobby";
            }
            else
            {
                playerStatus.text = "Conectando al Lobby";
            }
            m_conexionManeger.UpdateSesiones(JugadorSingleton.Instance.m_usuario);
        }
    }
}

//funcion temporal para informe

public static class player_instanciate
{
    public static string type;
}
