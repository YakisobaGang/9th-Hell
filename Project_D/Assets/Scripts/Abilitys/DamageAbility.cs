using UnityEngine;

namespace ProjectD.Abilitys
{
    public class DamageAbility : AbilityBase
    {
        [SerializeField] [Range(1, 9999)] private int damageAmount;

        [SerializeField] private float timeToStartDamageTick = 1f;
        [SerializeField] private float timeToStopDamageTick = 1f;


        public override bool CastAbility()
        {
            if (hasCastingVFX)
                castingVFX.SpawnVFX(casterTransform.position, Quaternion.identity);

            if (hasImpactVFX)
                impactVfx.SpawnVFX(target.transform.position, Quaternion.identity);

            if (hasCastingVFX)
                castingVFX.PlayVFX();

            if (playImpactThroughScript)
            {
                impactVfx.PlayVFX();
                target.health.DoDamangeTick?.Invoke(timeToStartDamageTick, timeToStopDamageTick);
            }

            var isDead = DoDamage();

            return isDead;
        }

        private bool DoDamage()
        {
            return target.health.TakeDamage(damageAmount);
        }
    }
}