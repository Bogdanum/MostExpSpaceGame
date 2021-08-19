using Photon.Pun;
using Photon.Realtime;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class PhotonFriendController : MonoBehaviourPunCallbacks
{
    public static Action<List<PhotonFriendInfo>> OnDisplayFriends = delegate {};

    private void Awake()
    {
        PlayFabFriendsController.OnFriendListUpdated += HandleFriendsUpdated;
    }

    private void OnDestroy()
    {
        PlayFabFriendsController.OnFriendListUpdated -= HandleFriendsUpdated;
    }

    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
        if (friends.Count != 0)
        {
            string[] friendsDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendsDisplayNames);
        }
        else
        {
            List<PhotonFriendInfo> friendList = new List<PhotonFriendInfo>();
            OnDisplayFriends?.Invoke(friendList);
        }
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    {
        OnDisplayFriends?.Invoke(friendList);
    }
}

