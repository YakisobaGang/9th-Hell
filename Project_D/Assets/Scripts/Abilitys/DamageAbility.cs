using UnityEngine;

namespace ProjectD.Abilitys
{
    public class DamageAbility : AbilityBase
    {
        [SerializeField] [Range(1, 9999)] private int damageAmount;

        public override bool CastAbility()
        {
            castingVFX.SpawnVFX(casterTransform.position, Quaternion.identity);
            impactVfx.SpawnVFX(target.transform.position, Quaternion.identity);
            
            castingVFX.PlayVFX();
            var isDead = DoDamage();
            impactVfx.PlayVFX();

            return isDead;
        }

        private bool DoDamage()
        {
            return target.health.TakeDamage(damageAmount);
        }
    }
}