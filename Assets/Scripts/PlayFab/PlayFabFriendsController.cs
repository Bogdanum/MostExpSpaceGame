using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;

public class PlayFabFriendsController : MonoBehaviour
{
    public static Action<List<FriendInfo>> OnFriendListUpdated = delegate {};
    private List<FriendInfo> friends;

    private void Awake()
    {
        friends = new List<FriendInfo>();
        PhotonConnector.GetPhotonFriends += HandleGetFriends;
        UIAddFriend.OnAddFriend += HandleAddPlayfabFriend;
        UIFriend.OnRemoveFriend += HandleRemoveFriend;
    }

    private void OnDestroy()
    {
        PhotonConnector.GetPhotonFriends -= HandleGetFriends;
        UIAddFriend.OnAddFriend -= HandleAddPlayfabFriend;
        UIFriend.OnRemoveFriend -= HandleRemoveFriend;
    }

    private void Start()
    {
        
    }

    private void HandleRemoveFriend(string name)
    {
        string id = friends.FirstOrDefault(f => f.TitleDisplayName == name).FriendPlayFabId;
        var request = new RemoveFriendRequest { FriendPlayFabId = id };
        PlayFabClientAPI.RemoveFriend(request, OnFriendRemoveSuccess, OnFailure);
    }

    private void HandleAddPlayfabFriend(string name)
    {
        var request = new AddFriendRequest { FriendTitleDisplayName = name };
        PlayFabClientAPI.AddFriend(request, OnFriendAddedSuccess, OnFailure);
    }

    private void HandleGetFriends()
    {
        GetPlayFabFriends();
    }

    private void GetPlayFabFriends()
    {
        var request = new GetFriendsListRequest { IncludeSteamFriends = false, IncludeFacebookFriends = false, XboxToken = null };
        PlayFabClientAPI.GetFriendsList(request, OnFriendListSuccess, OnFailure);
    }

    private void OnFriendListSuccess(GetFriendsListResult result)
    {
        friends = result.Friends;
        OnFriendListUpdated?.Invoke(result.Friends);
    }


    private void OnFriendAddedSuccess(AddFriendResult result)
    {
        GetPlayFabFriends();
    }

    private void OnFriendRemoveSuccess(RemoveFriendResult result)
    {
        GetPlayFabFriends();
    }

    private void OnFailure(PlayFabError error)
    {
        Debug.LogWarning($"PlayFab Friend Error occured: {error.GenerateErrorReport()}");
    }

}

