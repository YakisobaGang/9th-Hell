using ProjectD.Interfaces;
using ProjectD.ScriptableObjects;
using UnityEngine;

namespace ProjectD.Abilitys
{
    public abstract class AbilityBase : MonoBehaviour, ICommand
    {
        [SerializeField] protected AbilityData abilityDataData;
        protected Transform target;

        protected virtual void Ability() { }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
        
        public void Execute()
        {
            Ability();
        }
    }
}