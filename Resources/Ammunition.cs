using UnityEngine;
using UnityEngine.Events;
using Space_Adventures.Core;

namespace Space_Adventures.Resources
{
    public class Ammunition : MonoBehaviour
    {
        [SerializeField] private Shield shield = null;
        [SerializeField] private int rocketCount = 0;
        [SerializeField] private int shieldCount = 0;
        [SerializeField] private float rocketCoolDownDuration = 10f;
        [SerializeField] private float shieldCoolDownDuration = 20f;
        [SerializeField] private UnityEvent pickupEvent = null;

        private float rocketCoolDownTimer = 0f;
        private float shieldCoolDownTimer = 0f;

        private void Start()
        {
            SpaceAdventuresEvents.updateRocketCount.Invoke(rocketCount);
            SpaceAdventuresEvents.updateShieldCount.Invoke(shieldCount);
        }

        private void Update()
        {
            if(rocketCoolDownTimer >= 0f) { rocketCoolDownTimer -= Time.deltaTime; }
            if (shieldCoolDownTimer >= 0f) { shieldCoolDownTimer -= Time.deltaTime; }
        }

        public void IncrementRocketCount()
        {
            pickupEvent.Invoke();
            rocketCount++;
            SpaceAdventuresEvents.updateRocketCount.Invoke(rocketCount);
        }

        public void IncrementShieldCount()
        {
            pickupEvent.Invoke();
            shieldCount++;
            SpaceAdventuresEvents.updateShieldCount.Invoke(shieldCount);
        }

        public void RocketHasBeenLaunched()
        {
            rocketCount--; 
            rocketCoolDownTimer = rocketCoolDownDuration;
            SpaceAdventuresEvents.setRocketButton.Invoke(rocketCount, rocketCoolDownDuration);
        }

        public void ActivateShield()
        {
            shield.SetShield();
            shieldCount--;
            shieldCoolDownTimer = shieldCoolDownDuration;
            SpaceAdventuresEvents.setShieldButton.Invoke(shieldCount, shieldCoolDownDuration);
        }

        public bool RocketIsReady()
        {
            return (rocketCoolDownTimer <= 0f && rocketCount > 0);
        }

        public bool ShieldIsReady()
        {
            return (shieldCoolDownTimer <= 0f && shieldCount > 0);
        }
    }
}

