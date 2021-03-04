using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Space_Adventures.SceneManagement
{
    public class BannerAd : MonoBehaviour
    {
        [SerializeField] private bool testMode = true;
        private readonly string playStoreID = "3958701";
        private readonly string  placementID = "banner";
        

        private IEnumerator Start()
        {
            Advertisement.Initialize(playStoreID, testMode);

            while (!Advertisement.IsReady(placementID))
                yield return null;

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_RIGHT);
            Advertisement.Banner.Show(placementID);
        }

        private void OnDisable()
        {
            Advertisement.Banner.Hide();
        }
    }
}
