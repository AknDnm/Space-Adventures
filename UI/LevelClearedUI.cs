using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Space_Adventures.Core;

namespace Space_Adventures.UI
{
    public class LevelClearedUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> stars = null; //0)Right 1)Middle 2)Left
        [SerializeField] private Text scoreText = null;
        [SerializeField] private GameObject continueButton = null;
        [SerializeField] private float delay = 0.4f;

        private void OnEnable()
        {
            SpaceAdventuresEvents.processLevelClearedUI.AddListener(ProcessLevelClearedUI);
        }

        public void ProcessLevelClearedUI(int rating, int score)
        {
            StartCoroutine(AnimateUI(rating, score));
        }

        IEnumerator AnimateUI(int rating, int score)
        {
            for (int starCount = 0; starCount < rating - 2; starCount++) //rating - 2 because our star ratings at the list starts from 2.
            {
                yield return new WaitForSeconds(delay);
                stars[starCount].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(delay);
            scoreText.gameObject.SetActive(true);
            scoreText.text = score.ToString();
            yield return new WaitForSeconds(delay);
            continueButton.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            SpaceAdventuresEvents.processLevelClearedUI.RemoveListener(ProcessLevelClearedUI);
        }
    }
}
