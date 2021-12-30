using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private string nickName;
    public static Action GetPhotonFriends = delegate {};

    #region Unity Method

    private void Awake()
    {
        nickName = PlayerPrefs.GetString("PlayerName");
        UIInvite.OnRoomInviteAccept += HandleRoomConnectAccept;
    }

    private void OnDestroy()
    {
        UIInvite.OnRoomInviteAccept -= HandleRoomConnectAccept;
    }

    private void Start()
    {
        ConnectToPhoton(nickName);
    }

    
    #endregion
    #region Private Methods
    private void ConnectToPhoton(string name)
    {
        Debug.LogWarning($"Connect to Photon as {name}");
        PhotonNetwork.AuthValues = new AuthenticationValues(name);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void CreatePhotonRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }

    private void HandleRoomConnectAccept(string roomName)
    {
        PlayerPrefs.SetString("PHOTONROOM", roomName);
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            if (PhotonNetwork.InLobby)
            {
                JoinPlayerRoom();
            }
        }
    }

    private void JoinPlayerRoom()
    {
        string room = PlayerPrefs.GetString("PHOTONROOM");
        PlayerPrefs.SetString("PHOTONROOM", "");
        PhotonNetwork.JoinRoom(room);
    }
    #endregion
    #region Public Methods
    public void OnCreateRoomClicked(string roomName)
    {
        CreatePhotonRoom(roomName);
    }
    #endregion
    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.LogWarning("You have connected to Photon Master Server");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("You have connected to Photon Lobby");
        GetPhotonFriends?.Invoke();
        string room = PlayerPrefs.GetString("PHOTONROOM");
        if (!string.IsNullOrEmpty(room))
        {
            JoinPlayerRoom();
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.LogWarning($"You have created a Photon Room named {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.LogWarning($"You have joined the Photon Room {PhotonNetwork.CurrentRoom.Name}");
    }
    public override void OnLeftRoom()
    {
        Debug.LogWarning("You have left a Photon Room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning($"You failed to join a Photon Room {message}");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogWarning($"Another player has joined the room {newPlayer.UserId}");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogWarning($"Player has left the room {otherPlayer.UserId}");
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        Debug.LogWarning($"New Master Client is {newMasterClient.UserId}");
    }
    #endregion
}
