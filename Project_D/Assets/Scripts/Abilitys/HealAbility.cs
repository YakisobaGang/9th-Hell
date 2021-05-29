using UnityEngine;

namespace ProjectD.Abilitys
{
    public class HealAbility : AbilityBase
    {
        [SerializeField] [Range(1, 9999)] private int healAmount;

        public override bool CastAbility()
        {
            castingVFX.SpawnVFX(target.transform.position, Quaternion.identity);
            castingVFX.PlayVFX();
            return Heal();
        }

        private bool Heal()
        {
            target.health.Heal(healAmount);

            return false;
        }
    }
}