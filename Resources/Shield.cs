using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Space_Adventures.Resources
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float shieldMaxHealth = 50f;
        [SerializeField] private float shieldMaxTime = 15f;
        [SerializeField] private bool isActive = false;
        [SerializeField] private UnityEvent onEnable = null;
        [SerializeField] private UnityEvent onDisable = null;

        private float shieldHealth = 0f;
        private float shieldTimer = 0f;

        private Animator animator;
        private new CircleCollider2D collider;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            collider = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            DisableShield();
        }

        public void SetShield()
        {
            shieldHealth = shieldMaxHealth;
            shieldTimer = shieldMaxTime;

            StopAllCoroutines();
            ActivateShield();
            StartCoroutine(ShieldTimer(shieldTimer));
        }

        private void ActivateShield()
        {
            isActive = true;
            animator.enabled = true;
            collider.enabled = true;
            spriteRenderer.enabled = true;
            onEnable.Invoke();
        }

        public void TakeDamage(float damage)
        {
            shieldHealth = Mathf.Max(shieldHealth - damage, 0);

            if (shieldHealth <= 0)
            {
                onDisable.Invoke();
                DisableShield();
            }
        }

        public void DisableShield()
        {
            isActive = false;
            StopAllCoroutines();
            animator.enabled = false;
            collider.enabled = false;
            spriteRenderer.enabled = false;
            
        }

        IEnumerator ShieldTimer(float time)
        {
            yield return new WaitForSeconds(time);
            DisableShield();
            OnDisableEvent();
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void OnDisableEvent()
        {
            onDisable.Invoke();
        }
    }
}
