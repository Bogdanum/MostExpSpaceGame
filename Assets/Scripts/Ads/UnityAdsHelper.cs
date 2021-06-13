using UnityEngine;
using System;
using UnityEngine.Advertisements;
using Helpers;

public class UnityAdsHelper : MonoBehaviour
{
    private string gameId;

    [Header("Game IDs")]
    [SerializeField] private string googlePlayAppId;
    [SerializeField] private string appstoreAppId;

    private void Awake()
    {
#if UNITY_ANDROID
        gameId = googlePlayAppId;
#elif UNITY_IOS
        gameId = appstoreAppId;
#endif

        Advertisement.Initialize(gameId);
    }

    public void ShowVideoAd()
    {
        if (UnityAdsVideoHelper.isReady())
        {
            UnityAdsVideoHelper.Show();
        }
    }

}
