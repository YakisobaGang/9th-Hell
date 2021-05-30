﻿using System.Collections;
using ProjectD.Combat;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Abilitys
{
    public abstract class AbilityBase : MonoBehaviour, ICastAbility
    {
        [SerializeField] private AbilityData abilityData;
        [SerializeField] protected AbilityVFXController castingVFX;
        [SerializeField] protected AbilityVFXController impactVfx;
        [SerializeField] protected Transform casterTransform = null;
        protected Unit target;
        public AbilityTypes abilityType => abilityData.GetAbilityType();
        public string abilityName => abilityData.GetName();

        public abstract bool CastAbility();

        public void SetTarget(Unit newTarget)
        {
            target = newTarget;
        }

        public void SetCasterTransform(Transform _transform)
        {
            casterTransform = _transform;
        }

        public Transform GetCasterTransform() => casterTransform;
    }
}