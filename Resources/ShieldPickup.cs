using UnityEngine;

namespace Space_Adventures.Resources
{
    public class ShieldPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                other.GetComponent<Ammunition>().IncrementShieldCount();
                Destroy(gameObject);
            }
        }
    }
}
