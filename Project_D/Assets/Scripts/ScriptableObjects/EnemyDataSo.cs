using ProjectD.Abilitys;
using UnityEngine;

namespace ProjectD.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Create New Enemy", menuName = "Create New Enemy", order = 0)]
    public class EnemyDataSo : ScriptableObject
    {
        [SerializeField] private Sprite enemySprite;
        [SerializeField] private float health;
        [SerializeField] private string enemyName;
        [SerializeField] private AbilityBase[] abilitys = new AbilityBase[0];

        public string GetName() => enemyName;
        public Sprite GetSprite() => enemySprite;
        public AbilityBase[] GetAllAbilitys() => abilitys;
        public AbilityBase GetAbility(int index) => abilitys[index];

        public float GetHealth() => health;
        /// <summary>
        /// Quando chamar essa função ela ira diminur o valor da vida atual pelo novo
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public float SetHealth(float damage) => health -= damage;
    }
}