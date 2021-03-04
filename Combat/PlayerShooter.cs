using System.Collections;
using UnityEngine;

namespace Space_Adventures.Combat
{
    public class PlayerShooter : Shooter
    {
        [SerializeField] private float fireRateForGun = 0.1f;

        private bool firstTime = true;

        public IEnumerator FireContinuosly(int gunNumber)
        {
            if(firstTime) 
            { 
                yield return new WaitForSeconds(1f);
                firstTime = false;
            }

            while (true)
            {
                LaunchProjectile(gunNumber);

                yield return new WaitForSeconds(fireRateForGun);
            }
        }
    }
}
