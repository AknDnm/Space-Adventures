using UnityEngine;
using Space_Adventures.Core;
using UnityEngine.Events;

namespace Space_Adventures.Resources
{
    public class PlayerHealth : Health
    {
        [SerializeField] private UnityEvent onRegenerateHealth = null;

        private void Start()
        {
            SpaceAdventuresEvents.setPlayerHealthBar.Invoke(maxHealth);
        }

        public override void GetHit()
        {
            SpaceAdventuresEvents.updatePlayerHealthBar.Invoke(health);
            SpaceAdventuresEvents.damageTaken.Invoke(receivedDamage);
            base.GetHit();
        }

        public override void Die()
        {
            SpaceAdventuresEvents.updatePlayerHealthBar.Invoke(health);
            base.Die();
            SpaceAdventuresEvents.playerDied.Invoke();
        }

        public void IncreaseHealth(float extraHealth)
        {
            if (!isDead)
            {
                onRegenerateHealth.Invoke();
                health = Mathf.Min(health + extraHealth, maxHealth);
                SpaceAdventuresEvents.updatePlayerHealthBar.Invoke(health);
            }
        }
    }
}
