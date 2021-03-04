using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Space_Adventures.Progression;

namespace Space_Adventures.Control
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> waveConfigs = null;
        [SerializeField] private int startingWave = 0;
        [SerializeField] private float waitTimeForFirstWave = 3f;

        private int aliveEnemyCount = 0;
        private bool spawningFinished = false;
        private bool firstTimeWait = true;

        private void Start()
        {
            StartCoroutine(SpawnAllWaves());
        }

        private void Update()
        {
            CheckGameState();
        }

        private void CheckGameState()
        {
            if(spawningFinished && aliveEnemyCount == 0)
            {
                FindObjectOfType<CurrentLevelAdmin>().GameHasFinished();
                Destroy(gameObject);
            }
        }

        private IEnumerator SpawnAllWaves()
        {
            // It will give some time at the beginning of the level
            if (firstTimeWait)
            {
                yield return new WaitForSeconds(waitTimeForFirstWave);
                firstTimeWait = false;
            }

            for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
            {
                var currentWave = waveConfigs[waveIndex];

                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

                // If continuous spawning isn't set as true then it will wait until all enemies are dead
                if (!waveConfigs[waveIndex].GetContinuosSpawningCondition()) 
                yield return new WaitUntil(() => aliveEnemyCount <= 0); 

                // Delay between waves
                yield return new WaitForSeconds(waveConfigs[waveIndex].GetDelayForTheNextGroup());
            }
            spawningFinished = true;
        }

        private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
        {
            for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
            {
                var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().SetWaveConfig(waveConfig);

                yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
            }
        }

        public void EnemySpawned()
        {
            aliveEnemyCount++;
        }

        public void EnemyDied()
        {
            aliveEnemyCount--;
        }
    }
}
