using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Audio;

namespace Space_Adventures.SceneManagement
{
    public class InterstitialAd : MonoBehaviour, IUnityAdsListener
    {
        [SerializeField] private AudioMixer audiomixer = null;

        private readonly string playStoreID = "3958701";
        private bool reloadScene = false;

        public bool isTestAd;
        

        private void Start()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(playStoreID, isTestAd);
        }

        public void ShowAd(bool itsGameOver)
        {
            reloadScene = itsGameOver;
            if (Advertisement.IsReady("video")) { Advertisement.Show("video"); }
            else { HandleSceneLoading(); }
                
        }

        public void OnUnityAdsReady(string placementId)
        {
        }

        public void OnUnityAdsDidError(string message)
        {
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            audiomixer.SetFloat("Options Music Volume", -80f);
            audiomixer.SetFloat("Options Game Volume", -80f);
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            HandleSceneLoading();
        }

        private void HandleSceneLoading()
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            if (reloadScene) { sceneLoader.LoadThisSceneAgain(); }
            else { sceneLoader.LoadNextScene(); }
        }
    }
}
