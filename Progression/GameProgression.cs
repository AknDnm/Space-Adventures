using System.Collections.Generic;
using UnityEngine;
using Space_Adventures.Saving;
using System.Collections;

namespace Space_Adventures.Progression
{
    public class GameProgression : MonoBehaviour, ISaveable
    {
        [SerializeField] private RewardSystem rewardSystem = null;
        [SerializeField] private int totalListCount = 10;

        private static List<int> levelList;

        private SavingWrapper savingWrapper;
        

        private void Awake()
        {
            savingWrapper = FindObjectOfType<SavingWrapper>();
        }

        private void OnEnable()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            savingWrapper.Load();
            if (levelList == null) { CreateList(); }
        }

        private void CreateList()
        {
            // Create a level list that contains all levels condition
            levelList = new List<int>();
            for(int index = 0; index <totalListCount; index++)
            {
                // At first will set them all to zero
                levelList.Add(0);
            }
        }

        public void SetTheLevelRating(int rating, int levelIndex) 
        {
            int oldLevelRating = levelList[levelIndex];

            if(oldLevelRating < rating)
            {
                levelList[levelIndex] = rating;
            }

            int nextLevelIndex = levelIndex + 1;
            if(nextLevelIndex < levelList.Count)
            {
                if(levelList[nextLevelIndex] < 1) 
                levelList[nextLevelIndex] = 1; // We just set the active mode for the next level if it wasn't played before.
            }
            savingWrapper.Save();
        }

        public int GetTheLevelRating(int level)
        {
            return levelList[level];
        }

        public object CaptureState()
        {
            return levelList;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;
            levelList = (List<int>)state;
        }

        public RewardSystem GetRewardSystem()
        {
            return rewardSystem;
        }
    }
}