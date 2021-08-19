using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

public class PhotonChatController : MonoBehaviour, IChatClientListener
{
    [SerializeField] private string nickName;
    private ChatClient chatClient;

    public static Action<string, string> OnRoomInvite = delegate { };

#region Monobeheviour Methods
    private void Awake()
    {
        nickName = PlayerPrefs.GetString("PlayerName");
        UIFriend.OnInviteFriend += HandleFriendInvite;
    }

    private void OnDestroy()
    {
        UIFriend.OnInviteFriend -= HandleFriendInvite;
    }

    private void Start()
    {
        chatClient = new ChatClient(this);
        ConnectToPhotonChat();
    }

    private void Update()
    {
        chatClient.Service();
    }

    #endregion Monobeheviour Methods

    #region Private Methods

        private void ConnectToPhotonChat()
        {
            Debug.LogWarning("Connecting to Photon Chat");
            chatClient.AuthValues = new Photon.Chat.AuthenticationValues(nickName);
            ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
            chatClient.ConnectUsingSettings(chatSettings);
        }

    #endregion Private Methods

    #region Public Methods

        public void HandleFriendInvite(string recipient)
        {
            chatClient.SendPrivateMessage(recipient, PhotonNetwork.CurrentRoom.Name);
        }

    #endregion Public Methods

    #region Photon Chat Calbacks


    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnDisconnected()
    {
        Debug.LogWarning("You have disconnected from the Photon Chat");
    }

    public void OnConnected()
    {
        Debug.LogWarning("You have connected to the Photon Chat");
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        if (!string.IsNullOrEmpty(message.ToString()))
        {
            // Chanel Name format [Sender : Recipient]
            string[] splitNames = channelName.Split(new char[] { ':' });
            string senderName = splitNames[0];
            if (!sender.Equals(senderName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.LogWarning($"{sender} : {message}");
                OnRoomInvite?.Invoke(sender, message.ToString());
            }
        }
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

#endregion Photon Chat Calbacks
}
