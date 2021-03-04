using UnityEngine;
using Space_Adventures.Combat;
using Space_Adventures.Core;
using Space_Adventures.Progression;

namespace Space_Adventures.Control
{
    public class EnemyAI : MonoBehaviour, IAction, ILastAction
    {
        [SerializeField] private float shootTimer = 1f;
        [SerializeField] private float minTimeBetweenShots = 0.2f;
        [SerializeField] private float maxtimeBetweenShots = 3f;
        [SerializeField] private float minYPosition = -7f;
        [SerializeField] private float maxYPosition = 7f;

        private Shooter shooter;
        private float gameDifficultyFactor = 1f;

        private void Awake()
        {
            shooter = GetComponent<Shooter>();
        }

        private void Start()
        {
            shootTimer = Random.Range(minTimeBetweenShots, maxtimeBetweenShots);
            gameDifficultyFactor = FindObjectOfType<GameDifficulty>().GetGameDifficultyFactor();
        }

        private void Update()
        {
            CountDownAndShoot();
        }

        private void CountDownAndShoot()
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                for (int gun = 0; gun < shooter.GetGunsCount(); gun++)
                {
                //Prevents shooting outside of the view of the camera
                if(transform.position.y > minYPosition && transform.position.y < maxYPosition) { shooter.Fire(gun); }
                }

                shootTimer = Random.Range(minTimeBetweenShots - (gameDifficultyFactor/2.5f), maxtimeBetweenShots - (gameDifficultyFactor/2.5f));
            }
        }

        public void Cancel()
        {
            Destroy(this);
        }

        public void InvokeLastAction()
        {
            Destroy(this);
        }
    }
}
