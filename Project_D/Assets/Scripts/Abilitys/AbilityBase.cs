using System.Collections;
using ProjectD.Combat;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Abilitys
{
    public abstract class AbilityBase : MonoBehaviour, ICastAbility
    {
        [SerializeField] private AbilityData abilityData;
        [SerializeField] private Transform casterTransform = null;
        protected Unit target;
        public void SetTarget(Unit newTarget) => target = newTarget;
        public AbilityTypes abilityType => abilityData.GetAbilityType();
        public string abilityName => abilityData.GetName();

        public virtual bool CastAbility()
        {
            return false;
        }

        protected IEnumerator PlayCastFX()
        {
            if (abilityData.GetCastFX is null || casterTransform is null)
            {
                yield break;
            }

            var fx = Instantiate(abilityData.GetCastFX, casterTransform);

            yield return new WaitUntil(() => fx.gameObject.GetComponent<ParticleSystem>().isEmitting == false);

            Destroy(fx, 0.3f);
        }
    }
}