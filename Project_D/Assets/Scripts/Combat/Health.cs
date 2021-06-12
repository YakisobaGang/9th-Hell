using System;
using System.Collections;
using ProjectD.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD.Combat
{
    public class Health : MonoBehaviour, IDamageable, ICanHeal
    {
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        [SerializeField] private Animator anim;
        public UnityEvent OnDeath;
        public AudioSource hit;
        private static readonly int IsRecivingDamage = Animator.StringToHash("IsRecivingDamage");
        public Action<float, float> DoDamangeTick;

        private void OnEnable()
        {
            DoDamangeTick += HandleDamageTick;
        }

        private void OnDisable()
        {
            DoDamangeTick -= HandleDamageTick;
        }

        private void HandleDamageTick(float timeToStart, float timeToStop)
        {
            Invoke(nameof(StartDamageTick), timeToStart);
            
            Invoke(nameof(StopDamageTick), timeToStop);
        }

        private void OnValidate()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        private void Update()
        {
            if (health <= 0)
                gameObject.SetActive(false);
        }

        public void Heal(int healAmount = 1)
        {
            health += healAmount;

            if (health > maxHealth || health < 0)
                health = Mathf.Clamp(health, 0, maxHealth);

            OnHealthChange?.Invoke(CurrentHealthPercentage());
        }

        public bool TakeDamage(int damage = 1)
        {
            hit.Play();
            health -= damage;

            OnHealthChange?.Invoke(CurrentHealthPercentage());
            
            if (health >= 0) return false;

            OnDeath?.Invoke();
            return true;
        }

        public event Action<float> OnHealthChange;

        public float CurrentHealthPercentage()
        {
            return (float)health / maxHealth;
        }

        private void StopDamageTick()
        {
            anim.SetBool(IsRecivingDamage, false);
        
        } 
        private void StartDamageTick()
        {
            anim.SetBool(IsRecivingDamage, true);
        }
    }
}