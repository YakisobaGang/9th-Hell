using UnityEngine;

namespace ProjectD.Abilitys
{
    public class DamageAbility : AbilityBase
    {
        [SerializeField] [Range(1, 9999)] private int damageAmount;


        public override bool CastAbility()
        {
            if (hasCastingVFX)
                castingVFX.SpawnVFX(casterTransform.position, Quaternion.identity);

            if (hasImpactVFX)
                impactVfx.SpawnVFX(target.transform.position, Quaternion.identity);

            if (hasCastingVFX)
                castingVFX.PlayVFX();

            if (playImpactThroughScript)
                impactVfx.PlayVFX();

            var isDead = DoDamage();

            return isDead;
        }

        private bool DoDamage()
        {
            return target.health.TakeDamage(damageAmount);
        }
    }
}