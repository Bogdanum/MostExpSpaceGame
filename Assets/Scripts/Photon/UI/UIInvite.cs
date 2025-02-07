﻿using UnityEngine;
using System;
using UnityEngine.UI;

public class UIInvite : MonoBehaviour
{
    [SerializeField] private string _friendName;
    [SerializeField] private string _roomName;
    [SerializeField] private Text _friendNameText;

    public static Action<UIInvite> OnInviteAccept = delegate { };
    public static Action<string> OnRoomInviteAccept = delegate { };
    public static Action<UIInvite> OnInviteDecline = delegate { };

    public void Initialize(string friendName, string roomName)
    {
        _friendName = friendName;
        _roomName = roomName;

        _friendNameText.text = _friendName;
    }

    public void AcceptInvite()
    {
        OnInviteAccept?.Invoke(this);
        OnRoomInviteAccept?.Invoke(_roomName);
    }

    public void DeclineInvite()
    {
        OnInviteDecline?.Invoke(this);
    }
}
