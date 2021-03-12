using System;
using UnityEngine;

namespace ProjectD.ScriptableObjects
{
    [CreateAssetMenu]
    public class Fighter : ScriptableObject
    {
        [SerializeField] private string fighterName;
        [SerializeField] private int level;
        [SerializeField] private Color color;
        [SerializeField] private int currentHealth;
        [SerializeField] private int totalHealth;
        [SerializeField] private int attack;
        [SerializeField] private int healing;

        public string FighterName => fighterName;
        public int Level => level;
        public Color Color => color;
        public int CurrentHealth => currentHealth;
        public int TotalHealth => totalHealth;
        public int Attack => attack;
        public int Healing => healing;

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