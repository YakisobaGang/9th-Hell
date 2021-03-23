using UnityEngine;

namespace ProjectD.Abilitys
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create New Ability", order = 0)]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityTypes type;
        [SerializeField] private string abilityName;
        [SerializeField] private GameObject onHitAbilityFX = null;
        [SerializeField] private GameObject castAbilityFx = null;
        [SerializeField, Range(0.5f, 4f)] private float castTime = 1f;
        public GameObject GetOnHitFX => onHitAbilityFX;
        public GameObject GetCastFX => castAbilityFx;
        public float CastTime => castTime;
        public AbilityTypes GetAbilityType() => type;
        public string GetName() => abilityName;
    }
}