using System;
using UnityEngine.Advertisements;

namespace Helpers
{
    public static class UnityAdsRewardedVideoHelper
    {
        public static bool isReady() => Advertisement.IsReady(placementId: "rewardedVideo");

       
        public static void Show(Action<ShowResult> callback)
        {
            ShowOptions options = new ShowOptions
            {
                resultCallback = callback
            };
            Advertisement.Show(placementId: "rewardedVideo", options);
        }
    }
}