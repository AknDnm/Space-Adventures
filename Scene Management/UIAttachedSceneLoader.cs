using UnityEngine;
using UnityEngine.SceneManagement;

namespace Space_Adventures.SceneManagement
{
    public class UIAttachedSceneLoader : MonoBehaviour
    {
        // Button Event
        public void LoadSceneWithTransition(int sceneIndex)
        {
            FindObjectOfType<SceneLoader>().LoadSceneWithTransition(sceneIndex);
        }

        // Button Event
        public void LoadThisSceneAgain()
        {
            FindObjectOfType<SceneLoader>().LoadThisSceneAgain();
        }

        // Button Event
        public void LoadMainMenu()
        {
            FindObjectOfType<SceneLoader>().LoadMainMenu();
        }

        // Button Event
        public void CallInterstitialAd(bool itsGameOver)
        {
            FindObjectOfType<InterstitialAd>().ShowAd(itsGameOver);
        }

        // Button Event
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
