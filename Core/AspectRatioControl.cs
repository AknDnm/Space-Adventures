using UnityEngine;

namespace Space_Adventures.Core
{
    // This class detects the aspect ratio of the phone. According to the aspect ratio
    // enemies come closer to the player to adapt to the screen size. This game is designed
    // for a screen that has at least 18:9 aspect ratio. If it is lower than that then the game
    // is stay playable but becomes more difficult as enemies are much closer to the player.
    public class AspectRatioControl : MonoBehaviour
    {
        [SerializeField] private float reducedAspectRatioFactor = 2f;

        private float aspectRatioFactor = 0f;
        private void OnEnable()
        {
            CheckAspectRatio();
        }

        private void CheckAspectRatio()
        {
            if(Camera.main.aspect < 1.9f) { aspectRatioFactor = reducedAspectRatioFactor; }
        }

        public float GetAspectRatioFactor()
        {
            return aspectRatioFactor;
        }
    }
}
