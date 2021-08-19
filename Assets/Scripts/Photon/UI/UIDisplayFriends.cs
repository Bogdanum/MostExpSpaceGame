using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class UIDisplayFriends : MonoBehaviour
{
    [SerializeField] private Transform friendsContainer;
    [SerializeField] private UIFriend uiFriendPrefab;

    private void Awake()  
    {
        PhotonFriendController.OnDisplayFriends += HandleDisplayFriends;
    }

    private void OnDestroy()
    {
        PhotonFriendController.OnDisplayFriends -= HandleDisplayFriends;
    }

    private void HandleDisplayFriends(List<FriendInfo> friends)
    {
        foreach (Transform child in friendsContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (FriendInfo friend in friends)
        {
            UIFriend uIFriend = Instantiate(uiFriendPrefab, friendsContainer);
            uIFriend.Initialize(friend);
        }
    }
}
