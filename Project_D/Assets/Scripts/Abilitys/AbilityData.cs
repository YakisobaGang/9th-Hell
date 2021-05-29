using UnityEngine;

namespace ProjectD.Abilitys
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create New Ability", order = 0)]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityTypes type;
        [SerializeField] private string abilityName;

        public AbilityTypes GetAbilityType()
        {
            return type;
        }

        public string GetName()
        {
            return abilityName;
        }
    }
}