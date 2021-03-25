using UnityEngine;

namespace ProjectD.Abilitys
{
    public class DamageAbility : AbilityBase
    {
        [SerializeField] [Range(1, 9999)] private int damageAmount;

        public override bool CastAbility()
        {
            return DoDamage();
        }

        private bool DoDamage()
        {
            return target.TakeDamage(damageAmount);
        }
    }
}