using UnityEngine;
using Space_Adventures.SceneManagement;
using Space_Adventures.Core;

namespace Space_Adventures.Progression
{
    public class CurrentLevelAdmin : MonoBehaviour
    {
        private RewardSystem rewardSystem = null;
        private bool gameStopped = false;
        private bool gameFinished = false;
        private float gameTimer = 0f;
        private float totalReceivedDamage = 1f;   // It is set to 1 to prevent diving by 0
        private int level;
        private bool gameOver = false;

        private void Awake()
        {
            SpaceAdventuresEvents.playerDied.AddListener(PlayerDied);
            SpaceAdventuresEvents.damageTaken.AddListener(IncrementReceivedDamage);
        }

        private void Start()
        {
            rewardSystem = FindObjectOfType<GameProgression>().GetRewardSystem();
            SetLevelIndex();
        }

        private void Update()
        {
            if (!gameFinished && !gameStopped)
            {
                gameTimer += Time.deltaTime;
            }
        }

        private void ProcessThisLevelRating()
        {
            if (gameOver) return;

            float timeRewardPoints = (1 / gameTimer);
            float damageRewardPoints = (1 / totalReceivedDamage);
            int totalPoints = (int)((timeRewardPoints + damageRewardPoints) * 100000);

            int rating = GetThisLevelRating(totalPoints);
            FindObjectOfType<GameProgression>().SetTheLevelRating(rating, level - 1);
            SpaceAdventuresEvents.activateLevelClearedUI.Invoke(rating, totalPoints);
        }

        private void SetLevelIndex()
        {
            int index = FindObjectOfType<SceneLoader>().GetCurrentSceneIndex();
            level = index - 1;
        }

        public void PlayerDied()
        {
            if (!gameFinished)
            {
                gameOver = true;
                InvokeTheEndLevelActions();
                SpaceAdventuresEvents.activateGameOverUI.Invoke();
            }
        }

        public void IncrementReceivedDamage(float damage)
        {
            totalReceivedDamage += damage;
        }

        public void GameIsStopped(bool condition)
        {
            gameStopped = condition;
        }

        public void GameHasFinished()
        {
            gameFinished = true;
            InvokeTheEndLevelActions();
            ProcessThisLevelRating();
        }

        public int GetOneStarPoint()
        {
            return rewardSystem.GetOneStarPoint(level);
        }

        public int GetTwoStarPoint()
        {
            return rewardSystem.GetTwoStarPoint(level);
        }

        public int GetThreeStarPoint()
        {
            return rewardSystem.GetThreeStarPoint(level);
        }

        private int GetThisLevelRating(int point)
        {
            if(point < GetOneStarPoint())
            {
                return 2;
            }
            else if (point < GetTwoStarPoint())
            {
                return 3;
            }
            else if (point < GetThreeStarPoint())
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }

        // When we complete the level or die, it will call all InvokeTheEndGameAction() functions
        private void InvokeTheEndLevelActions()
        {
            foreach (ActionForTheEndofLevel component in FindObjectsOfType<ActionForTheEndofLevel>())
            {
                component.InvokeTheEndGameAction();
            }
        }

        private void OnDisable()
        {
            SpaceAdventuresEvents.playerDied.RemoveListener(PlayerDied);
            SpaceAdventuresEvents.damageTaken.RemoveListener(IncrementReceivedDamage);
        }
    }
}
