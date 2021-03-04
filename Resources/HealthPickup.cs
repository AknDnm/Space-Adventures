using UnityEngine;

namespace Space_Adventures.Resources
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private float health = 20f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.IncreaseHealth(health);
                Destroy(gameObject);
            }
        }
    }
}
