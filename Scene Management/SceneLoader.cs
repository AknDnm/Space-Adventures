using Space_Adventures.Saving;
using Space_Adventures.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Space_Adventures.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Slider slider = null;
        [SerializeField] private Text percentageText = null;
        [SerializeField] private bool automaticLoad = false;
        [SerializeField] private int lastSceneIndex = 8;

        private IEnumerator Start()
        {
            if (automaticLoad) { yield return (StartCoroutine(LoadAsynchronously())); }
            else { yield return null; }
        }

        private IEnumerator Transition(int sceneIndex)
        {
            Time.timeScale = 1f;

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut();

            yield return SceneManager.LoadSceneAsync(sceneIndex);
            FindObjectOfType<SavingWrapper>().Load();


            yield return fader.FadeIn();

        }

        private IEnumerator LoadAsynchronously()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);

                if (slider && percentageText)
                {
                    slider.value = progress;
                    percentageText.text = progress * 100 + "%";
                }

                yield return null;
            }
        }

        public void LoadNextScene()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(sceneIndex - 1 == lastSceneIndex)
            {
                StartCoroutine(Transition(1));
                return;
            }
            StartCoroutine(Transition(sceneIndex));
        }

        public void LoadMainMenu()
        {
            FindObjectOfType<LevelUIManager>().DisablePauseMenu();
            StartCoroutine(Transition(1));
        }

        public void LoadSceneWithTransition(int sceneIndex)
        {
            StartCoroutine(Transition(sceneIndex));
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadThisSceneAgain()
        {
            int sceneIndex = GetCurrentSceneIndex();
            StartCoroutine(Transition(sceneIndex));
        }

        public int GetCurrentSceneIndex()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            return sceneIndex;
        }
    }
}
