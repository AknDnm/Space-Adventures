using System.Collections.Generic;
using UnityEngine;

namespace Space_Adventures.Core
{
    [System.Serializable]
    public class PoolEffects
    {
        public EffectTypes effectType;
        public GameObject prefab;
        public int amount;
    }

    [System.Serializable]
    public class PoolProjectiles
    {
        public ProjectileTypes projectileType;
        public GameObject prefab;
        public int amount;
    }

    // Object Pooling Design Pattern
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private List<PoolEffects> poolEffects;
        [SerializeField] private List<PoolProjectiles> poolProjectiles;

        private Dictionary<ProjectileTypes, List<GameObject>> projectilesTable;
        private Dictionary<EffectTypes, List<GameObject>> effectsTable;

        // Parent gameobjects to instantiate items underneath 
        private GameObject poolProjectilesContainer;
        private GameObject poolEffectsContainer;

        private void Awake()
        {
            poolProjectilesContainer = new GameObject("Pool - Projectiles");
            poolEffectsContainer = new GameObject("Pool - Effects");

            poolProjectilesContainer.transform.SetParent(this.transform);
            poolEffectsContainer.transform.SetParent(this.transform);
        }

        private void Start()
        {
            InstantiateAllEffects();
            InstantiateAllProjectiles();
        }

        private void InstantiateAllProjectiles()
        {
            projectilesTable = new Dictionary<ProjectileTypes, List<GameObject>>();
            foreach (PoolProjectiles poolProjectile in poolProjectiles)
            {
                List<GameObject> cacheList = new List<GameObject>();
                for (int i = 0; i < poolProjectile.amount; i++)
                {
                    GameObject obj = Instantiate(poolProjectile.prefab);
                    obj.transform.SetParent(poolProjectilesContainer.transform);
                    obj.SetActive(false);
                    cacheList.Add(obj);
                }
                projectilesTable[poolProjectile.projectileType] = cacheList;
            }
        }

        private void InstantiateAllEffects()
        {
            effectsTable = new Dictionary<EffectTypes, List<GameObject>>();
            foreach(PoolEffects poolEffect in poolEffects)
            {
                List<GameObject> cacheList = new List<GameObject>();
                for(int i = 0; i < poolEffect.amount; i++)
                {
                    GameObject obj = Instantiate(poolEffect.prefab);
                    obj.transform.SetParent(poolEffectsContainer.transform);
                    cacheList.Add(obj);
                    obj.SetActive(false);
                }
                effectsTable[poolEffect.effectType] = cacheList;
            }
        }

        public GameObject GetProjectile(ProjectileTypes projectileType)
        {
            foreach(GameObject projectile in projectilesTable[projectileType])
            {
                if (!projectile.activeInHierarchy)
                {
                    return projectile;
                }
            }

            foreach(PoolProjectiles poolProjectile in poolProjectiles)
            {
                if(poolProjectile.projectileType == projectileType)
                {
                    GameObject projectile = Instantiate(poolProjectile.prefab);
                    projectilesTable[projectileType].Add(projectile);
                    projectile.transform.SetParent(poolProjectilesContainer.transform);
                    projectile.SetActive(false);
                    return projectile;
                }
            }
            return null;
        }

        public GameObject GetEffect(EffectTypes effectType)
        {
            foreach (GameObject effect in effectsTable[effectType])
            {
                if (!effect.activeInHierarchy)
                {
                    return effect;
                }
            }

            foreach (PoolEffects poolEffect in poolEffects)
            {
                if (poolEffect.effectType == effectType)
                {
                    GameObject effect = Instantiate(poolEffect.prefab);
                    effectsTable[effectType].Add(effect);
                    effect.transform.SetParent(poolEffectsContainer.transform);
                    effect.SetActive(false);
                    return effect;
                }
            }
            return null;
        }
    }
}
