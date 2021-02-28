using System.Collections.Generic;
using UnityEngine;
using Space_Adventures.Resources;
using Space_Adventures.Core;

namespace Space_Adventures.Control
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyController : MonoBehaviour, IAction
    {
        [SerializeField] private float moveSpeed = 5f;

        private WaveConfig waveConfig;
        private List<Transform> waypoints;
        private EnemySpawner enemySpawner;
        private int waypointIndex = 0;
        private Vector3 aspectRatioFactor;

        private void Awake()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        void Start()
        {
            enemySpawner.EnemySpawned();
            waypoints = waveConfig.GetWaypoints();
            float xAxisAspectRatioFactor = FindObjectOfType<AspectRatioControl>().GetAspectRatioFactor();
            aspectRatioFactor = new Vector3(xAxisAspectRatioFactor, 0f, 0f);
            transform.position = (waypoints[waypointIndex].transform.position - aspectRatioFactor);  //This enemy is going to be born at the first waypoint
        }

        void Update()
        {
            Move();
        }

        public void SetWaveConfig(WaveConfig waveConfig)
        {
            this.waveConfig = waveConfig;
        }

        private void Move()
        {
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = (waypoints[waypointIndex].transform.position - aspectRatioFactor);
                var movementThisFrame = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

                // If it reached the current target position move to next one
                if (transform.position == targetPosition) { waypointIndex++; }
                // If it reached the last waypoint, skip one time waypoints and set waypoint index.
                if(waypointIndex >= waypoints.Count) { waypointIndex = waveConfig.GetOneTimeWaypointsCount(); }
            }
        }

        public void Cancel()
        {
            Destroy(this);
        }

        public void OnDestroy()
        {
            enemySpawner.EnemyDied();
        }
    }
}

