using System;
using System.Collections.Generic;
using ProjectD.Abilitys;
using UnityEngine;

namespace ProjectD.Combat
{
    [RequireComponent(typeof(Health))]
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private string unitName;
        [SerializeField] private AbilityBase[] abilitys;
        [SerializeField] private int baseDamage = 10;
        public Health health;
        private readonly Queue<int> abilityIndex = new Queue<int>();
        private readonly Queue<GameObject> targets = new Queue<GameObject>();

        public AbilityBase[] GetAllAbilitys => abilitys;
        public string GetUnitName => unitName;
        public int BaseDamage => baseDamage;

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

            var target = targets.Dequeue();

            if (target is null)
                return;

            try
            {
                if (!target.TryGetComponent<Unit>(out var targetInfo))
                    return;

                abilitys[index].SetTarget(targetInfo);
                abilitys[index].CastAbility();
            }
            catch (MissingReferenceException err)
            {
                print(err);
                return;
            }
        }
    }
}