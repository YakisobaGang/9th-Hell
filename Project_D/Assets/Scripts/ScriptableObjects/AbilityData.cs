using ProjectD.Abilitys;
using UnityEngine;

namespace ProjectD.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create New Ability", order = 0)]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityTypes type;
        [SerializeField] private string abilityName;
        [SerializeField] private GameObject onHitAbilityFX;
        [SerializeField] private GameObject castAbilityFx;
        [SerializeField] private int damage;

        public AbilityTypes GetAbilityType() => type;
        public string GetName() => name;
        public GameObject GetOnHitFX => onHitAbilityFX;
        public GameObject GetCastFX => castAbilityFx;
        public int GetDamageValue => damage;

        /// <summary>
        /// se o valor de dano for negativo, a função vai usar o valor de dentro da classe.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="_damage"></param>
        /// <returns></returns>
        public bool CastAbility(Fighter target, int _damage = -1) => target.Damage(_damage <= -1 ? this.damage : _damage);
    }
}