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
        private int? abilityIndex;

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
            abilityIndex = index;
        }

        public bool UsingAbility()
        {
            if (targets is null || abilityIndex is null)
                return false;

            if (abilitys[abilityIndex.Value].abilityType == AbilityTypes.Heal)
            {
                abilitys[abilityIndex.Value].SetTarget(gameObject.GetComponent<Unit>());
                targets.Dequeue();
                return abilitys[abilityIndex.Value].CastAbility();
            }

            abilitys[abilityIndex.Value].SetTarget(targets.Dequeue().GetComponent<Unit>());
            return abilitys[abilityIndex.Value].CastAbility();
        }
    }
}