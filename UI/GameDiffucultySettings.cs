using Space_Adventures.Progression;
using UnityEngine;
using UnityEngine.UI;

namespace Space_Adventures.UI {
    public class GameDiffucultySettings : MonoBehaviour
    {
        [SerializeField] private Slider slider = null;

        private float difficultyLevel;

        private void Start()
        {
            FirstSetupForSlider();
        }

        public void SetGameDifficulty(float level)
        {
            difficultyLevel = level;
            FindObjectOfType<GameDifficulty>().SetGameDifficulty(difficultyLevel);            
        }

        public void FirstSetupForSlider()
        {
            difficultyLevel = FindObjectOfType<GameDifficulty>().GetGameDifficultyFactor();
            slider.value = difficultyLevel;
        }
    }
}
