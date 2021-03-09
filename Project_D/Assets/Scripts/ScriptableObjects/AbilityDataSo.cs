using ProjectD.Abilitys;
using UnityEngine;

namespace ProjectD.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create New Ability", order = 0)]
    public class AbilityDataSo : ScriptableObject
    {
        [SerializeField] private AbilityTypes type;
        [SerializeField] private string abilityName;
        [SerializeField, Tooltip("Quantidade de dano ou de curra da abilidade")] private float value;
        [SerializeField] private GameObject abilityFX;

        public AbilityTypes GetAbilityType() => type;
        public string GetName()              => name;
        public float GetValue()              => value;
        public GameObject GetFX()            => abilityFX;
    }
}