using System.Collections.Generic;
using UnityEngine;

namespace Space_Adventures.Control
{
    [CreateAssetMenu(menuName = "Enemy Wave Configuration")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private GameObject enemyPrefab = null;
        [SerializeField] private GameObject pathPrefab = null;
        [SerializeField] private float timeBetweenSpawns = 0.5f;
        [SerializeField] private float spawnRandomFactor = 0.3f;
        [SerializeField] private int numberOfEnemies = 5;
        [SerializeField] private int oneTimeWaypointsCount = 2;   // These waypoints are used for the first loop of the movement only and are not repeated.
        [SerializeField] private bool continuousSpawning = true;
        [SerializeField] private float delayForTheNextGroup = 1f;

        public GameObject GetEnemyPrefab() { return enemyPrefab; }

        public List<Transform> GetWaypoints()
        {
            var waveWaypoints = new List<Transform>();
            foreach (Transform child in pathPrefab.transform)
            {
            waveWaypoints.Add(child);
            }
            
            return waveWaypoints;
        }

        public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

        public float GetSpawnRandomFactor() { return spawnRandomFactor; }

        public int GetNumberOfEnemies() { return numberOfEnemies; }

        public int GetOneTimeWaypointsCount() { return oneTimeWaypointsCount; }

        public bool GetContinuosSpawningCondition() { return continuousSpawning; }

        public float GetDelayForTheNextGroup() { return delayForTheNextGroup; }

    }
}
