using ProjectD.Combat;
using UnityEngine;

namespace ProjectD.Abilitys
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create New Ability", order = 0)]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityTypes type;
        [SerializeField] private string abilityName;
        [SerializeField] private GameObject onHitAbilityFX;
        [SerializeField] private GameObject castAbilityFx;
        [SerializeField] private int damage;
        public GameObject GetOnHitFX => onHitAbilityFX;
        public GameObject GetCastFX => castAbilityFx;

        public AbilityTypes GetAbilityType()
        {
            return type;
        }

        public string GetName()
        {
            return name;
        }

        public bool CastAbility(GameObject target, float damageMultiplayer = 1)
        {
            return target.GetComponent<Unit>().TakeDamage((int)(damage * damageMultiplayer));
        }
    }
}