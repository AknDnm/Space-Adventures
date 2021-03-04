using UnityEngine;

namespace Space_Adventures.Resources
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Transform foreground = null;

        public void UpdateHealthBar(float healthFraction)
        {
            foreground.localScale = new Vector3(healthFraction, 1f, 1f);
        }

        public void DestroyBar()
        {
            this.gameObject.SetActive(false);
        }
    }
}
