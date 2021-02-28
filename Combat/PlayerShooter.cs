using System.Collections;
using UnityEngine;

namespace Space_Adventures.Combat
{
    public class PlayerShooter : Shooter
    {
        [SerializeField] private float fireRateForGun = 0.1f;

        public IEnumerator FireContinuosly(int gunNumber)
        {
            while (true)
            {
                LaunchProjectile(gunNumber);

                yield return new WaitForSeconds(fireRateForGun);
            }
        }
    }
}
