using Space_Adventures.Core;
using Space_Adventures.Progression;
using Space_Adventures.Resources;
using UnityEngine;
using UnityEngine.Events;

namespace Space_Adventures.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float damage = 20;
        [SerializeField] private GameObject fireEffect = null;
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] private UnityEvent onHit = null;
        [SerializeField] private bool playerProjectile = false;

        // According to the game difficulty level it will provide extra damage for the enemies' projectiles
        private float extraDamage;

        private void Start()
        {
            if (playerProjectile) { extraDamage = 0f; } // If this projectile belongs to player don't give any extra damage
            else { extraDamage = (FindObjectOfType<GameDifficulty>().GetGameDifficultyFactor() * 5); }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if it hits the game borders
            if (other.GetComponent<Shredder>()) return;  

            
            GetComponent<ActionScheduler>().Disable();  // To stop the projectile immediately after the collision
            GetComponent<SpriteRenderer>().enabled = false; // To make projectile disappear immediately after the collision
            GetComponent<Collider2D>().enabled = false; 

            onHit.Invoke();

            Health health = other.GetComponent<Health>();
            Shield shield = other.GetComponent<Shield>();

            if (shield && shield.IsActive()) { shield.TakeDamage(damage + extraDamage);  } 
            else if(health) { health.TakeDamage(damage + extraDamage); }

            if (hitEffect != null) { Instantiate(hitEffect, transform.position, Quaternion.identity); }

            Destroy(gameObject, 5f); // Giving 5 second delay to play the explosion sound effect
        }

        public GameObject GetFireEffect()
        {
            return fireEffect;
        }
    }
}
