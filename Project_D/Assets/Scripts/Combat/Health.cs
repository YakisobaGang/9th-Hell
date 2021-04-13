using System;
using ProjectD.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD.Combat
{
    public class Health : MonoBehaviour, IDamageable, ICanHeal
    {
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        public UnityEvent OnDeath;

        private void OnValidate()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        private void Update()
        {
            if (health <= 0)
                Destroy(gameObject);
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
    }
}