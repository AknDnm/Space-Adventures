using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Space_Adventures.Core;

namespace Space_Adventures.Resources
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float health = 100f;
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] private GameObject body = null;
        [SerializeField] private UnityEvent onDie = null;

        protected bool isDead = false;
        private bool isTriggered = false;
        protected float receivedDamage;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void TakeDamage(float damage)
        {
            receivedDamage = damage;
            health = Mathf.Max(health - damage, 0);

            if(health == 0 && !isDead)
            {
                isDead = true;
                Die();
            }
            else if(!isDead)
            {
                GetHit();
            }
        }

        public virtual void GetHit()
        {
            if (!isTriggered) 
            { 
                animator.SetTrigger("GetHit");
                isTriggered = true;
                // To prevent weird animation behaviour we gave a mini delay and reset trigger
                StartCoroutine(AnimationDelay());
            }
        }

        public virtual void Die()
        {
            GetComponent<ActionScheduler>().Disable();
            onDie.Invoke();
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Die");
        }

        public void Destroy()
        {
            body.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 1f);
        }

        private IEnumerator AnimationDelay()
        {
            yield return new WaitForSeconds(0.2f);
            isTriggered = false;
        }
    }
}
