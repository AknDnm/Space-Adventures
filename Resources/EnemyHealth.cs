using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_Adventures.Resources
{
    public class EnemyHealth : Health
    {
        [SerializeField] private EnemyHealthBar healthBar = null;

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            healthBar.UpdateHealthBar(health / maxHealth);
        }
    }
}
