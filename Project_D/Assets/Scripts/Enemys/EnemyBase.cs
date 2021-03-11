using ProjectD.Commands;
using ProjectD.Interfaces;
using ProjectD.ScriptableObjects;
using UnityEngine;

namespace ProjectD.Enemys
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField] protected EnemyDataSo enemyData;
        public CommandHandler commandHandler = new CommandHandler();

        public virtual void TakeDamage(int damage = 1)
        {
            enemyData.SetHealth(damage);
        }
    }
}