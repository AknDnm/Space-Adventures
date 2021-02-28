using System.Collections.Generic;
using UnityEngine;

namespace Space_Adventures.Progression
{
    [CreateAssetMenu(fileName = "Reward System", menuName = "Space Adventures/Reward System", order = 0)]
    public class RewardSystem : ScriptableObject
    {
        [SerializeField] private List<LevelStats> levels = null;

        [System.Serializable]
        public class LevelStats
        {
            public int oneStarPoint;
            public int twoStarPoint;
            public int threeStarPoint;
        }

        public int GetOneStarPoint(int level)
        {
            return levels[level - 1].oneStarPoint;
        }

        public int GetTwoStarPoint(int level)
        {
            return levels[level - 1].twoStarPoint;
        }

        public int GetThreeStarPoint(int level)
        {
            return levels[level - 1].threeStarPoint;
        }

        public int GetLevelCount()
        {
            return levels.Count;
        }
    }
}

