using System.Collections;
using ProjectD.Combat;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Abilitys
{
    public abstract class AbilityBase : MonoBehaviour, ICastAbility
    {
        [SerializeField] private AbilityData abilityData;
        [SerializeField] protected Transform casterTransform;
        [SerializeField] protected AbilityVFXController castingVFX;
        [SerializeField] protected AbilityVFXController impactVfx;
        protected Unit target;
        public AbilityTypes abilityType => abilityData.GetAbilityType();
        public string abilityName => abilityData.GetName();

        public abstract bool CastAbility();


        public void SetTarget(Unit newTarget)
        {
            target = newTarget;
        }
    }
}