using ProjectD.Abilitys;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Combat
{
    public class Unit : MonoBehaviour, IDamageable, ICanHeal
    {
        [SerializeField] private string unitName;
        [SerializeField] private int health = 1000;
        [SerializeField] private AbilityData[] abilitys;
        private int? abilityIndex;

        private GameObject target;

        public string GetUnitName => unitName;

        public void Heal(int healAmount = 1)
        {
            health += healAmount;
        }

        public bool TakeDamage(int damage = 1)
        {
            health -= damage;

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