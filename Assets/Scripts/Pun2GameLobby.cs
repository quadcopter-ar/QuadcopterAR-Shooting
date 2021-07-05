using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
// reference: https://sharpcoderblog.com/blog/make-a-multiplayer-game-in-unity-3d-using-pun-2
public class Pun2GameLobby : MonoBehaviourPunCallbacks
{

    private string gameVersion = "0.9";
    private int MaxRoomPlayers = 2;
    //Our player name
    [SerializeField]
    public string playerName = "Player 1";

    public List<RoomInfo> existingRooms = new List<RoomInfo>();
    public string roomName = "Room 1";
    public Vector2 roomListScroll = Vector2.zero;
    public bool joiningRoom = false;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + cause.ToString() + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("OnConnectedToMaster");
        //After we connected to Master server, join the Lobby
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Debug.Log("Room List is updated");
        //After this callback, update the room list
        existingRooms = roomList;
    }

    void OnGUI()
    {
        GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), LobbyWindow, "Game Rooms");
    }

    void LobbyWindow(int index)
    {
        //Connection Status and Room creation Button
        GUILayout.BeginHorizontal();

        GUILayout.Label("Pun 2 lobby Status: " + PhotonNetwork.NetworkClientState);

        if (joiningRoom || !PhotonNetwork.IsConnected || PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
        {
            GUI.enabled = false;
        }

        GUILayout.FlexibleSpace();

        //Room name text field
        roomName = GUILayout.TextField(roomName, GUILayout.Width(Screen.width/2));

        if (GUILayout.Button("Create Room", GUILayout.Width(125)))
        {
            if (roomName != "")
            {
                joiningRoom = true;

                RoomOptions roomOptions = new RoomOptions();
                roomOptions.IsOpen = true;
                roomOptions.IsVisible = true;
                roomOptions.MaxPlayers = (byte)this.MaxRoomPlayers; //Set any number

                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            }
        }

        GUILayout.EndHorizontal();

        roomListScroll = GUILayout.BeginScrollView(roomListScroll, true, true);

        for (int i = 0; i < existingRooms.Count; i++)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label(existingRooms[i].Name, GUILayout.Width(400));
            GUILayout.Label(existingRooms[i].PlayerCount + "/" + existingRooms[i].MaxPlayers);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Join"))
            {
                joiningRoom = true;
                PhotonNetwork.NickName = playerName;
                PhotonNetwork.JoinRoom(existingRooms[i].Name);
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        GUI.enabled = (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby || PhotonNetwork.NetworkClientState == ClientState.Disconnected) && !joiningRoom;
        if (GUILayout.Button("Refresh", GUILayout.Width(100)))
        {
            if (PhotonNetwork.IsConnected)
            {
                //Re-join Lobby to get the latest Room list
                PhotonNetwork.JoinLobby(TypedLobby.Default);
            }
            else
            {
                //We are not connected, estabilish a new connection
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        GUILayout.EndHorizontal();

        if (joiningRoom)
        {
            GUI.enabled = true;
            GUI.Label(new Rect(900 / 2 - 50, 400 / 2 - 10, 100, 20), "Connecting...");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Debug.Log("OnCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
        joiningRoom = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //Debug.Log("OnJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //Debug.Log("OnJoinRandomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnCreatedRoom()
    {
        //Debug.Log("OnCreatedRoom");
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.LoadLevel("test");
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("OnJoinedRoom");
    }
}