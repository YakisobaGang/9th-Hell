using System;
using ProjectD.Abilitys;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Combat
{
    public class Unit : MonoBehaviour, IDamageable, ICanHeal
    {
        public event Action<int> OnHealthChange;
        [SerializeField] private string unitName;
        [SerializeField] private int health = 1000;
        [SerializeField] private AbilityData[] abilitys;
        [SerializeField] private int baseDamage = 10;
        private int? abilityIndex;
        private GameObject target;

        public string GetUnitName => unitName;
        public int BaseDamage => baseDamage;

        public void Heal(int healAmount = 1)
        {
            health += healAmount;
            OnHealthChange?.Invoke(health);
        }

        public bool TakeDamage(int damage = 1)
        {
            health -= damage;

            OnHealthChange?.Invoke(health);

            return health == 0 ? true : false;
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }

        public void SetChooseAbility(int index)
        {
            abilityIndex = index;
        }

        public bool UsingAbility()
        {
            if (target is null || abilityIndex is null)
                return false;

            return abilitys[abilityIndex.Value].CastAbility(target);
        }
    }
}