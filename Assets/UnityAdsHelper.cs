using UnityEngine;
using System;
using UnityEngine.Advertisements;
using Helpers;

public class UnityAdsHelper: MonoBehaviour
{
    private string gameId;

    [Header("Game IDs")]
    [SerializeField] private string googlePlayAppId;
    [SerializeField] private string appstoreAppId;

    private void Awake()
    {
#if UNITY_ANDROID
        gameId = this.googlePlayAppId;
#elif UNITY_IOS
        gameId = this.appstoreAppId;
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

    public void ShowRewardedVideoAd(Action<ShowResult> callback)
    {
        if (UnityAdsRewardedVideoHelper.isReady())
        {
            UnityAdsRewardedVideoHelper.Show(callback);
        }
    }

    public void DemoRewardedVideoAd()
    {
        ShowRewardedVideoAd(result =>
        {
            switch (result)
            {
                case ShowResult.Failed:
                    Debug.Log(message: "Show Result => Field");
                    break;

                case ShowResult.Finished:
                    Debug.Log(message: "Show Result => Finished");
                    break;

                case ShowResult.Skipped:
                    Debug.Log(message: "Show Result => Skipped");
                    break;
            }
        });
    }
}
