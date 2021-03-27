using System;
using System.Collections.Generic;
using ProjectD.Abilitys;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Combat
{
    public class Unit : MonoBehaviour, IDamageable, ICanHeal
    {
        [SerializeField] private string unitName;
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        [SerializeField] private AbilityBase[] abilitys;
        [SerializeField] private int baseDamage = 10;
        private readonly Queue<GameObject> targets = new Queue<GameObject>();
        private readonly Queue<int> abilityIndex = new Queue<int>();

        public AbilityBase[] GetAllAbilitys => abilitys;
        public string GetUnitName => unitName;
        public int BaseDamage => baseDamage;

        private void OnValidate()
        {
            currentHealth = maxHealth;
        }

        public void Heal(int healAmount = 1)
        {
            currentHealth += healAmount;

            if (currentHealth > maxHealth || currentHealth < 0)
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }

            OnHealthChange?.Invoke((float)currentHealth / maxHealth);
        }

        public bool TakeDamage(int damage = 1)
        {
            currentHealth -= damage;

            OnHealthChange?.Invoke((float)currentHealth / maxHealth);

            return currentHealth == 0 ? true : false;
        }

        public event Action<float> OnHealthChange;

        public void AddTarget(GameObject newTarget)
        {
            targets.Enqueue(newTarget);
        }

        public void SetChooseAbility(int index)
        {
            if (abilityIndex.Count == 3)
                return;
            abilityIndex.Enqueue(index);
        }

        public void UsingAbility()
        {
            if (abilityIndex.Count == 0 || targets.Count == 0) return;

            var index = abilityIndex.Dequeue();

            if (abilitys[index].abilityType == AbilityTypes.Heal)
            {
                abilitys[index].SetTarget(gameObject.GetComponent<Unit>());
                abilitys[index].CastAbility();
                return;
            }

            abilitys[index].SetTarget(targets.Dequeue().GetComponent<Unit>());
            abilitys[index].CastAbility();
        }
    }
}