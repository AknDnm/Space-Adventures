using System.Collections;
using UnityEngine;

namespace Space_Adventures.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float fadeOutTime = 0.5f;
        [SerializeField] private float fadeInTime = 0.5f;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut()
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / fadeOutTime;
                yield return null;
            }
        }

        public IEnumerator FadeIn()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeInTime;
                yield return null;
            }
        }
    }
}
