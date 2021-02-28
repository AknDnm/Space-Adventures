using UnityEngine;

namespace Space_Adventures.Resources
{
    public class RocketPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Ammunition playerAmmunition = other.GetComponent<Ammunition>();
            if (playerAmmunition)
            {
                playerAmmunition.IncrementRocketCount();
                Destroy(gameObject);
            }
        }
    }
}
