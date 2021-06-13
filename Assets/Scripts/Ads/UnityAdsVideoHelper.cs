using UnityEngine.Advertisements;

namespace Helpers
{
    public static class UnityAdsVideoHelper
    {
        public static bool isReady() => Advertisement.IsReady(placementId: "video");

        public static void Show()
        {
            Advertisement.Show(placementId: "video");
        }
    }
}
