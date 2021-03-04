using Space_Adventures.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_Adventures.UI
{
    public class LevelUIManager : MonoBehaviour
    {
        [SerializeField] private List<Canvas> canvasGroups = null; // 0 -> HUD, 1 -> Pause, 2 -> Options, 3 -> Gameover, 4 -> LevelCleared, 5 - > Help

        private void OnEnable()
        {
            SpaceAdventuresEvents.activateGameOverUI.AddListener(ActivateGameOver);
            SpaceAdventuresEvents.activateLevelClearedUI.AddListener(ActivateLevelCleared);
        }

        private void Start()
        {
            ActivateHUD();
        }

        private void ActivateUI(int index)
        {
            for(int i = 0; i < canvasGroups.Count; i++)
            {
                if (index == i) { canvasGroups[i].gameObject.SetActive(true); }
                else { canvasGroups[i].gameObject.SetActive(false); }
            }
        }

        public void ActivateHUD()
        {
            ActivateUI(0);
            Time.timeScale = 1f;
        }

        public void ActivatePauseMenu()
        {
            ActivateUI(1);
            Time.timeScale = 0f;
        }

        public void ActivateOptions()
        {
            ActivateUI(2);
        }

        public void ActivateHelp()
        {
            ActivateUI(5);
        }

        public void ActivateGameOver()
        {
            StartCoroutine(ActivationGameOverUI());
        }

        public void DisablePauseMenu()
        {
            canvasGroups[1].gameObject.SetActive(false);
        }

        public void DisableLevelCleared()
        {
            canvasGroups[4].gameObject.SetActive(false);
        }

        public void DisableGameOver()
        {
            canvasGroups[3].gameObject.SetActive(false);
        }

        public void ActivateLevelCleared(int rating, int score)
        {
            StartCoroutine(ActivationLevelClearedUI(rating, score));
        }

        //Button Event
        public void RocketButtonPressed()
        {
            SpaceAdventuresEvents.rocketButtonPressed.Invoke();
        }

        //Button Event
        public void ShieldButtonPressed()
        {
            SpaceAdventuresEvents.shieldButtonPressed.Invoke();
        }

        private void OnDisable()
        {
            SpaceAdventuresEvents.activateGameOverUI.RemoveListener(ActivateGameOver);
            SpaceAdventuresEvents.activateLevelClearedUI.RemoveListener(ActivateLevelCleared);
        }

        private IEnumerator ActivationGameOverUI()
        {
            yield return new WaitForSeconds(1f);
            ActivateUI(3);
        }

        private IEnumerator ActivationLevelClearedUI(int rating, int score)
        {
            yield return new WaitForSeconds(1f);
            ActivateUI(4);
            SpaceAdventuresEvents.processLevelClearedUI.Invoke(rating, score);
        }
    }
}
