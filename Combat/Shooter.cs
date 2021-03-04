using System;
using System.Collections.Generic;
using Space_Adventures.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Space_Adventures.Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private List<ProjectileTypes> projectiles = null;
        [SerializeField] private List<EffectTypes> fireEffects = null;
        [SerializeField] private List<Transform> guns = null;
        [SerializeField] private UnityEvent[] onFire = null;

        private ObjectPooler objectPooler = null;

        private void Start()
        {
            objectPooler = FindObjectOfType<ObjectPooler>();
        }

        public void Fire(int gunNumber)
        {
            LaunchProjectile(gunNumber);
        }

        protected void LaunchProjectile(int gunNumber)
        {
            onFire[gunNumber].Invoke();
            GetFireEffect(gunNumber);
            GetProjectile(gunNumber);
        }

        private void GetFireEffect(int gunNumber)
        {
            GameObject fireEffect = objectPooler.GetEffect(fireEffects[gunNumber]);
            
            if(fireEffect != null)
            {
                fireEffect.SetActive(true);
                fireEffect.transform.position = guns[gunNumber].transform.position;
            }
        }

        private void GetProjectile(int gunNumber)
        {
            GameObject projectile = objectPooler.GetProjectile(projectiles[gunNumber]);
            if (projectile != null)
            {
                projectile.SetActive(true);
                projectile.transform.position = guns[gunNumber].transform.position;
            }
        }

        public int GetGunsCount()
        {
            return guns.Count;
        }
    }
}
