using System;
using System.Collections.Generic;
using ProjectD.Abilitys;
using UnityEngine;

namespace ProjectD.ScriptableObjects
{
    [CreateAssetMenu]
    public class Fighter : ScriptableObject
    {
        [SerializeField] private string fighterName;
        [SerializeField] private int currentHealth;
        [SerializeField] private int totalHealth;
        [SerializeField] private int attack;
        [SerializeField] private int healing;
        [SerializeField] private List<AbilityData> abilitys;

        public string FighterName => fighterName;
        public int CurrentHealth => currentHealth;
        public int TotalHealth => totalHealth;
        public int Attack => attack;
        public int Healing => healing;

        public AbilityData GetAbility(int index) => abilitys[index];

        private void Awake()
        {
            currentHealth = totalHealth;
        }

        /// <summary>
        /// O valor se deixar o valor do dano como -1 a função vai usar o dano purro da abilidade ex:
        /// uma abilidade foi crianda com 10 de dano, se você não mudar o valor, da função UsingAbility
        /// ela vai dar 10 de dano.
        /// Essa função retorna verdadeiro se o alvo morreu depois de receber dano
        /// </summary>
        /// <param name="index"></param>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool UsingAbility(int index, Fighter target, int damage = -1)
        {
            return abilitys[index].CastAbility(target, abilitys[index].GetDamageValue);
        }

        public bool Damage(int amount)
        {
            currentHealth = Math.Max(0, currentHealth - amount);
            return currentHealth == 0;
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
        }

        private void OnValidate()
        {
            currentHealth = Math.Min(currentHealth, totalHealth);
        }
    }
}