using Space_Adventures.Saving;
using UnityEngine;

namespace Space_Adventures.Progression
{
    public class GameDifficulty : MonoBehaviour, ISaveable
    {
        private float gameDifficultyFactor = 1f;

        public void SetGameDifficulty(float difficultyLevel)
        {
            gameDifficultyFactor = difficultyLevel;
            FindObjectOfType<SavingWrapper>().Save();
        }

        public float GetGameDifficultyFactor()
        {
            return gameDifficultyFactor;
        }

        public object CaptureState()
        {
            return gameDifficultyFactor;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;

            gameDifficultyFactor = (float)state;
        }
    }
}
