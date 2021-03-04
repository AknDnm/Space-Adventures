using System.Collections.Generic;
using UnityEngine;

namespace Space_Adventures.Resources
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pickups = null;
        [SerializeField] private List<Transform> spawnPoints = null;
        [SerializeField] private float spawnTime = 15f;
        [SerializeField] private float minSpawnTime = 1f;
        [SerializeField] private float maxSpawnTime = 4f;

        private int previousPickup = 10;
        private int spawnedPickupCounts = 0;

        private void Update()
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0)
            {
                SpawnPickup();
            }
        }

        private void SpawnPickup()
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
            // Spawn health generation pickup once in every 5 time 
            if (spawnedPickupCounts == 4)
            {
                Instantiate(pickups[pickups.Count - 1], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                spawnedPickupCounts = 0;
            }
            else
            {
                int randomPickup = Random.Range(0, pickups.Count - 2);

                // If this current random pickup that generated is same with the previous one, generate different one.
                if (randomPickup == previousPickup) { randomPickup = Mathf.Clamp(randomPickup + 1, 0, pickups.Count - 2); }

                Instantiate(pickups[randomPickup], spawnPoints[randomSpawnPoint].position, Quaternion.identity);

                previousPickup = randomPickup;
                spawnedPickupCounts++;
            }
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
