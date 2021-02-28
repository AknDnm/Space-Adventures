using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Space_Adventures.Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private List<Projectile>  projectilePrefabs = null;
        [SerializeField] private List<Transform> guns = null;
        [SerializeField] private UnityEvent[] onFire = null;

        public void Fire(int gunNumber)
        {
            LaunchProjectile(gunNumber);
        }

        protected void LaunchProjectile(int gunNumber)
        {
            var fireEffect = projectilePrefabs[gunNumber].GetFireEffect();
            onFire[gunNumber].Invoke();
            if (fireEffect != null)
            {
                var instantFireEffect = Instantiate(fireEffect, guns[gunNumber].position, Quaternion.identity);
                instantFireEffect.transform.parent = gameObject.transform;
            }
            Instantiate(projectilePrefabs[gunNumber], guns[gunNumber].position, Quaternion.identity);
        }

        public int GetGunsCount()
        {
            return guns.Count;
        }
    }
}
