using System;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class UIFriend : MonoBehaviour
{
    [SerializeField] private Text friendNameText;
    [SerializeField] private FriendInfo friend;
    [SerializeField] private Image onlineImage;
    [SerializeField] private Sprite onlineSprite;
    [SerializeField] private Sprite offlineSprite;


    public static Action<string> OnRemoveFriend = delegate {};
    public static Action<string> OnInviteFriend = delegate { };

    public void Initialize(FriendInfo friend)
    {
        Debug.Log($"{friend.UserId} is online: {friend.IsOnline} ; in room: {friend.IsInRoom} ; room name: {friend.Room}");
        this.friend = friend;

        SetupUI();
    }

    private void SetupUI()
    {
        friendNameText.text = friend.UserId;

        if (friend.IsOnline)
        {
            onlineImage.sprite = onlineSprite;
        }
        else
        {
            onlineImage.sprite = offlineSprite;
        }
    }

    public void RemoveFriend()
    {
        OnRemoveFriend?.Invoke(friend.UserId);
    }

    public void InviteFriend()
    {
        Debug.LogWarning($"Clicked to invite friend {friend.UserId}");
        OnInviteFriend?.Invoke(friend.UserId);
    }
}
