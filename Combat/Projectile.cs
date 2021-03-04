using Space_Adventures.Core;
using Space_Adventures.Progression;
using Space_Adventures.Resources;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Space_Adventures.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float damage = 20;
        [SerializeField] private EffectTypes hitEffectType;
        [SerializeField] private UnityEvent onHit = null;
        [SerializeField] private bool playerProjectile = false;
        [SerializeField] private float moveSpeed = 5f; 

        private ObjectPooler objectPooler;

        // According to the game difficulty level it will provide extra damage for the enemies' projectiles.
        private float extraDamage;

        private bool firstTime = true; // To detect the first OnEnable() function.
        private float setMoveSpeed;

        private void Awake()
        {
            objectPooler = FindObjectOfType<ObjectPooler>();
        }

        private void OnEnable()
        {
            if (!firstTime) { moveSpeed = setMoveSpeed; }

            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }

        private void Start()
        {
            if (playerProjectile) 
            { 
                extraDamage = 0f; // If this projectile belongs to player don't give any extra damage.
            } 
            else 
            {
                moveSpeed = -moveSpeed; // If this projectile belongs to an enemy, change the direction.
                extraDamage = (FindObjectOfType<GameDifficulty>().GetGameDifficultyFactor() * 5); 
            }
            firstTime = false;
            setMoveSpeed = moveSpeed;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Shredder>()) return;

            GetComponent<SpriteRenderer>().enabled = false; // To make projectile disappear immediately after the collision.
            GetComponent<Collider2D>().enabled = false;

            moveSpeed = 0f; // Stop the projectile if it hits a character.

            onHit.Invoke();

            GiveDamage(other);

            HitEffect();

            StartCoroutine(Disable()); // Give some delay to provide a time to play the sound effect.
        }

        private void GiveDamage(Collider2D other)
        {
            Health health = other.GetComponent<Health>();
            Shield shield = other.GetComponent<Shield>();

            if (shield && shield.IsActive()) { shield.TakeDamage(damage + extraDamage); }
            else if (health) { health.TakeDamage(damage + extraDamage); }
        }

        private void HitEffect()
        {
            GameObject hitEffect = objectPooler.GetEffect(hitEffectType);
            if (hitEffect != null)
            {
                hitEffect.gameObject.SetActive(true);
                hitEffect.transform.position = this.gameObject.transform.position;
            }
        }

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(3f);
            this.gameObject.SetActive(false);
        }
    }
}
